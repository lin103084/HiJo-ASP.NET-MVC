using Microsoft.AspNetCore.Mvc.Rendering;
using prjHiJoMVCCore.ViewModels;

namespace prjHiJoMVCCore.Models
{
    public class CEventHelper
    {
        // 儲存圖片到指定資料夾並回傳檔名
        public static async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            string ext = Path.GetExtension(file.FileName);
            string fileName = Guid.NewGuid().ToString() + ext;
            string fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        // 重建活動分類與城市的下拉選單選項
        public static void RebuildDropdowns(CEventFormViewModel vm, PrjFriendShipContext db)
        {
            vm.EventTypeOptions = db.EventTypes.Select(t => new SelectListItem
            {
                Value = t.EventTypeId.ToString(),
                Text = t.EventType1
            }).ToList();

            vm.CityOptions = db.Cities.Select(c => new SelectListItem
            {
                Value = c.CityId.ToString(),
                Text = c.CityName
            }).ToList();
        }
    }
}

