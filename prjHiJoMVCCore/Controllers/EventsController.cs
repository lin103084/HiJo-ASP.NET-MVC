using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjHiJoMVCCore.Models;
using prjHiJoMVCCore.ViewModels;

namespace prjHiJoMVCCore.Controllers
{
    public class EventsController : Controller
    {
        private readonly PrjFriendShipContext _db;
        private readonly IWebHostEnvironment _env;

        public EventsController(PrjFriendShipContext context, IWebHostEnvironment environment)
        {
            _db = context;
            _env = environment;
        }
        public IActionResult List()
        {
            var vm = new CEventQueryViewModel
            {
                txtKeyword = "",
                selectedEventType = null,
                selectedCity = null,
                eventTypeOptions = _db.EventTypes
            .Select(t => new SelectListItem { Value = t.EventTypeId.ToString(), Text = t.EventType1 })
            .ToList(),
                cityOptions = _db.Cities
            .Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName })
            .ToList(),
                resultList = _db.Events.Select(e => new CEventListWrap
                {
                    eventId = e.EventId,
                    eventName = e.Name,
                    eventTypeName = e.EventType.EventType1,
                    city = e.City.CityName
                }).ToList()
            };

            return View(vm);
        }
                
        public IActionResult Create()
        {
            var vm = new CEventFormViewModel
            {
                EventTypeOptions = _db.EventTypes.Select(t => new SelectListItem
                {
                    Value = t.EventTypeId.ToString(),
                    Text = t.EventType1
                }).ToList(),

                CityOptions = _db.Cities.Select(c => new SelectListItem
                {
                    Value = c.CityId.ToString(),
                    Text = c.CityName
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CEventFormViewModel eventUi)
        {
            if (eventUi.CoverPhoto == null)
            {
                ModelState.AddModelError("CoverPhoto", "請上傳封面圖片");
                CEventHelper.RebuildDropdowns(eventUi, _db);
                return View(eventUi);
            }


            if (!ModelState.IsValid)
            {
                CEventHelper.RebuildDropdowns(eventUi, _db);
                return View(eventUi);
            }

            // 確保圖片資料夾存在
            string folderPath = Path.Combine(_env.WebRootPath, "images", "eventPhotos");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1. 建立活動資料
                var eventDb = new Event
                {
                    Name = eventUi.Name,
                    Description = eventUi.Description,
                    CityId = eventUi.CityId,
                    EventTypeId = eventUi.EventTypeId,
                    Address = eventUi.Address,
                    Longitude = 0,
                    Latitude = 0
                };
                _db.Events.Add(eventDb);
                await _db.SaveChangesAsync(); // 拿到 EventId

                // 2. 儲存封面圖（固定 SortOrder = 0，IsCover = true）
                if (eventUi.CoverPhoto != null)
                {
                    string fileName = await CEventHelper.SaveFileAsync(eventUi.CoverPhoto, folderPath);
                    _db.EventPhotos.Add(new EventPhoto
                    {
                        EventId = eventDb.EventId,
                        IsCover = true,
                        Photo = fileName,
                        SortOrder = 0
                    });
                }

                // 3. 儲存其他圖片（SortOrder = 1, 2, ...，IsCover = false）
                if (eventUi.ExtraPhotos != null)
                {
                    int sortOrder = 1;
                    foreach (var photo in eventUi.ExtraPhotos)
                    {
                        if (photo != null)
                        {
                            string fileName = await CEventHelper.SaveFileAsync(photo, folderPath);
                            _db.EventPhotos.Add(new EventPhoto
                            {
                                EventId = eventDb.EventId,
                                IsCover = false,
                                Photo = fileName,
                                SortOrder = sortOrder++
                            });
                        }
                    }
                }

                await _db.SaveChangesAsync(); // 確保圖片也寫入成功
                await transaction.CommitAsync(); // 成功提交
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "新增活動時發生錯誤：" + ex.Message);

                // optional: 你也可以印出 inner exception
                if (ex.InnerException != null)
                    ModelState.AddModelError("", "詳細：" + ex.InnerException.Message);

                CEventHelper.RebuildDropdowns(eventUi, _db);
                return View(eventUi);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dbFind = await _db.Events
                        .Include(e => e.EventPhotos)
                        .FirstOrDefaultAsync(e => e.EventId == id);
            if (dbFind == null) 
                return NotFound();

            var vm = new CEventFormViewModel
            {
                EventId = dbFind.EventId,
                Name = dbFind.Name,
                Description = dbFind.Description,
                CityId = dbFind.CityId,
                EventTypeId = dbFind.EventTypeId,
                Address = dbFind.Address,
                ExistingPhotos = dbFind.EventPhotos?.ToList(),
                EventTypeOptions = _db.EventTypes
                    .Select(t => new SelectListItem { Value = t.EventTypeId.ToString(), Text = t.EventType1 })
                    .ToList(),
                CityOptions = _db.Cities
                    .Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CEventFormViewModel eventUi, List<int> RemainingPhotoIds)
        {
            if (!ModelState.IsValid)
            {
                CEventHelper.RebuildDropdowns(eventUi, _db);
                return View(eventUi);
            }

            var dbFind = await _db.Events
                .Include(e => e.EventPhotos)
                .FirstOrDefaultAsync(e => e.EventId == eventUi.EventId);
            if (dbFind == null) return NotFound();

            // 封面驗證（確保使用者未刪封面或已補上新封面）
            var oldCover = dbFind.EventPhotos.FirstOrDefault(p => p.IsCover);

            bool isCoverBeingRemoved = oldCover != null && eventUi.CoverPhotoDeleted && eventUi.CoverPhoto == null;
            bool hasCoverAfterEdit = (oldCover != null && !isCoverBeingRemoved) || eventUi.CoverPhoto != null;

            if (!hasCoverAfterEdit)
            {
                ModelState.AddModelError("CoverPhoto", "必須有封面圖片");

                eventUi.ExistingPhotos ??= new List<EventPhoto>();

                foreach (var photo in dbFind.EventPhotos)
                {
                    if (!eventUi.ExistingPhotos.Any(p => p.EventPhotoId == photo.EventPhotoId))
                        eventUi.ExistingPhotos.Add(photo);
                }

                CEventHelper.RebuildDropdowns(eventUi, _db);
                return View(eventUi);
            }


            string folderPath = Path.Combine(_env.WebRootPath, "images", "eventPhotos");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // 更新活動欄位
                dbFind.Name = eventUi.Name;
                dbFind.Description = eventUi.Description;
                dbFind.CityId = eventUi.CityId;
                dbFind.EventTypeId = eventUi.EventTypeId;
                dbFind.Address = eventUi.Address;
                dbFind.Latitude = 0;
                dbFind.Longitude = 0;

                // 替換封面圖片（若上傳新封面則刪除舊的）
                if (oldCover != null && eventUi.CoverPhoto != null)
                {
                    _db.EventPhotos.Remove(oldCover);
                    System.IO.File.Delete(Path.Combine(folderPath, oldCover.Photo));
                }

                if (eventUi.CoverPhoto != null)
                {
                    string fileName = await CEventHelper.SaveFileAsync(eventUi.CoverPhoto, folderPath);
                    _db.EventPhotos.Add(new EventPhoto
                    {
                        EventId = dbFind.EventId,
                        IsCover = true,
                        SortOrder = 0,
                        Photo = fileName
                    });
                }

                // 移除未保留的舊圖
                var existingExtraPhotos = dbFind.EventPhotos.Where(p => !p.IsCover).ToList();
                foreach (var photo in existingExtraPhotos)
                {
                    if (!RemainingPhotoIds.Contains(photo.EventPhotoId))
                    {
                        _db.EventPhotos.Remove(photo);
                        System.IO.File.Delete(Path.Combine(folderPath, photo.Photo));
                    }
                }

                // 加入新上傳的額外圖片
                int maxSortOrder = dbFind.EventPhotos
                    .Where(p => !p.IsCover)
                    .Select(p => p.SortOrder)
                    .DefaultIfEmpty(0)
                    .Max();

                if (eventUi.ExtraPhotos != null)
                {
                    foreach (var photo in eventUi.ExtraPhotos)
                    {
                        if (photo != null)
                        {
                            string fileName = await CEventHelper.SaveFileAsync(photo, folderPath);
                            _db.EventPhotos.Add(new EventPhoto
                            {
                                EventId = dbFind.EventId,
                                IsCover = false,
                                SortOrder = ++maxSortOrder,
                                Photo = fileName
                            });
                        }
                    }
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "更新活動失敗：" + ex.Message);
                if (ex.InnerException != null)
                    ModelState.AddModelError("", "詳細：" + ex.InnerException.Message);

                // 若出錯回傳畫面，維持使用者編輯狀態
                eventUi.ExistingPhotos = dbFind.EventPhotos.ToList();
                CEventHelper.RebuildDropdowns(eventUi, _db);
                return View(eventUi);
            }
        }

    }
}
