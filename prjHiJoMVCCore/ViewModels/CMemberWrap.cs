using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.ViewModels
{
    public class CMemberWrap
    {
        private Member _member;

        public CMemberWrap()
        {
            _member = new Member();
        }

        public Member member
        {
            get { return _member; }
            set { _member = value; }
        }

        // ---------------------- 基本屬性 ----------------------

        public int Id
        {
            get => _member.Id;
            set => _member.Id = value;
        }

        [Required(ErrorMessage = "請輸入使用者名稱")]
        [DisplayName("使用者名稱")]
        public string UserName
        {
            get => _member.UserName;
            set => _member.UserName = value;
        }

        [Required(ErrorMessage = "請輸入 Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        [DisplayName("電子郵件")]
        public string Email
        {
            get => _member.Email;
            set => _member.Email = value;
        }

        [Required(ErrorMessage = "請輸入密碼")]
        [DisplayName("密碼雜湊")]
        public string PassWordHash
        {
            get => _member.PassWordHash;
            set => _member.PassWordHash = value;
        }

        public string PassWordSalt
        {
            get => _member.PassWordSalt;
            set => _member.PassWordSalt = value;
        }

        [DisplayName("性別")]
        public string? Sex
        {
            get => _member.Sex;
            set => _member.Sex = value;
        }

        [DisplayName("生日")]
        [DataType(DataType.Date)]
        public DateOnly? Birthday
        {
            get => _member.Birthday;
            set => _member.Birthday = value;
        }

        [DisplayName("縣市")]
        public int? CityId
        {
            get => _member.CityId;
            set => _member.CityId = value;
        }

        [DisplayName("地區")]
        public int? DistrictId
        {
            get => _member.DistrictId;
            set => _member.DistrictId = value;
        }

        [DisplayName("個人簡介")]
        public string? Resume
        {
            get => _member.Resume;
            set => _member.Resume = value;
        }

        public bool? IsActive
        {
            get => _member.IsActive;
            set => _member.IsActive = value;
        }

        public bool? IsVerified
        {
            get => _member.IsVerified;
            set => _member.IsVerified = value;
        }

        public DateTime? LastOnline
        {
            get => _member.LastOnline;
            set => _member.LastOnline = value;
        }

        public string? AvatarPath
        {
            get => _member.AvatarPath;
            set => _member.AvatarPath = value;
        }

        // ---------------------- 處理檔案上傳 ----------------------

        [DisplayName("頭像圖片")]
        public IFormFile? AvatarFile { get; set; }

        // ---------------------- 顯示用屬性 ----------------------

        [DisplayName("城市名稱")]
        public string? CityName => _member.City?.CityName;

        [DisplayName("地區名稱")]
        public string? DistrictName => CityId.HasValue && _member.District?.ParentCityId == CityId ? _member.District?.DistrictName : null;

        [DisplayName("身分權限")]
        public string? RoleName { get; set; }
    }
}
