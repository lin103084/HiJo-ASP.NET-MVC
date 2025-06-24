using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.ViewModels
{
    public class CMemberFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "請輸入使用者名稱")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "請輸入 Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string Email { get; set; }

        public string? Sex { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Birthday { get; set; }

        public int? CityId { get; set; }

        public int? DistrictId { get; set; }

        public string? Resume { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsVerified { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public string? AvatarPath { get; set; }

        public List<int> SelectedRoleIds { get; set; } = new();

        public List<Role> RoleOptions { get; set; } = new();

        public List<City> CityOptions { get; set; } = new();

        public List<District> DistrictOptions { get; set; } = new();
    }
}
