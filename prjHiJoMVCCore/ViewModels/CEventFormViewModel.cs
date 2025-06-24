using Microsoft.AspNetCore.Mvc.Rendering;
using prjHiJoMVCCore.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjHiJoMVCCore.ViewModels
{
    public class CEventFormViewModel
    {
        public List<EventPhoto>? ExistingPhotos { get; set; }
        public int? EventId { get; set; }

        [Required(ErrorMessage = "活動名稱為必填")]
        [DisplayName("活動名稱")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "活動描述為必填")]
        [DisplayName("活動描述")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "活動分類為必選")]
        [DisplayName("活動分類")]
        public int EventTypeId { get; set; }

        [Required(ErrorMessage = "城市為必選")]
        [DisplayName("城市")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "活動地址為必填")]
        [DisplayName("活動地址")]
        public string? Address { get; set; }

        [DisplayName("封面圖片")]
        public IFormFile? CoverPhoto { get; set; }
        // 判斷使用者是否有刪除封面圖片的意圖
        public bool CoverPhotoDeleted { get; set; }


        [DisplayName("其他照片")]
        public List<IFormFile>? ExtraPhotos { get; set; }

        // 下拉選單選項來源
        public List<SelectListItem> EventTypeOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CityOptions { get; set; } = new List<SelectListItem>();
    }
}

