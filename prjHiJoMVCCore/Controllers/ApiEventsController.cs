using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHiJoMVCCore.Models;
using prjHiJoMVCCore.ViewModels;

namespace prjHiJoMVCCore.Controllers
{
    public class ApiEventsController : Controller
    {
        private readonly PrjFriendShipContext _db;
        private readonly IWebHostEnvironment _env;

        public ApiEventsController(PrjFriendShipContext context, IWebHostEnvironment environment)
        {
            _db = context;
            _env = environment;
        }

        public async Task<IActionResult> Search(
    [FromQuery] string? txtKeyword,
    [FromQuery] int? selectedEventType,
    [FromQuery] int? selectedCity,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            var query = _db.Events.AsQueryable();

            if (!string.IsNullOrEmpty(txtKeyword))
                query = query.Where(e => e.Name.Contains(txtKeyword));

            if (selectedEventType.HasValue)
                query = query.Where(e => e.EventTypeId == selectedEventType.Value);

            if (selectedCity.HasValue)
                query = query.Where(e => e.CityId == selectedCity.Value);

            var totalCount = await query.CountAsync(); // ✅ 總筆數

            var events = await query
                .OrderBy(e => e.EventId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new CEventListWrap
                {
                    eventId = e.EventId,
                    eventName = e.Name,
                    eventTypeName = e.EventType.EventType1,
                    city = e.City.CityName
                })
                .ToListAsync();

            return Ok(new
            {
                totalCount,
                events
            });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var eventDb = await _db.Events
                .Include(e => e.EventPhotos)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (eventDb == null)
                return NotFound("找不到該活動");

            // 確保圖片資料夾存在
            string folderPath = Path.Combine(_env.WebRootPath, "images", "eventPhotos");

            // 開啟交易（資料庫部分）
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                // 先刪除圖片實體檔案（若失敗將拋出例外）
                foreach (var photo in eventDb.EventPhotos)
                {
                    string fullPath = Path.Combine(folderPath, photo.Photo);
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);
                }

                // 刪除資料庫中的照片記錄
                _db.EventPhotos.RemoveRange(eventDb.EventPhotos);

                // 刪除活動主資料
                _db.Events.Remove(eventDb);

                // 資料庫儲存並提交交易
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok("刪除成功");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "刪除失敗：" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _db.EventPhotos.FindAsync(id);
            if (photo == null)
                return NotFound();

            // 刪除檔案
            string path = Path.Combine(_env.WebRootPath, "images", "eventPhotos", photo.Photo);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _db.EventPhotos.Remove(photo);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExtraPhotos(int eventId, List<IFormFile> photos)
        {
            var folder = Path.Combine(_env.WebRootPath, "images", "eventPhotos");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            int sort = await _db.EventPhotos
                .Where(p => p.EventId == eventId && !p.IsCover)
                .MaxAsync(p => (int?)p.SortOrder) ?? 0;

            foreach (var photo in photos)
            {
                string fileName = await CEventHelper.SaveFileAsync(photo, folder);
                _db.EventPhotos.Add(new EventPhoto
                {
                    EventId = eventId,
                    IsCover = false,
                    Photo = fileName,
                    SortOrder = ++sort
                });
            }

            await _db.SaveChangesAsync();
            return Ok();
        }


    }
}
