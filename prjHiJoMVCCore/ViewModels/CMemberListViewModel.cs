using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.ViewModels
{
    public class CMemberListViewModel
    {
        // 資料集
        public IEnumerable<CMemberWrap> Members { get; set; } = new List<CMemberWrap>();

        // 分頁與查詢參數
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }

        // 篩選條件
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public string? Sex { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public int? RoleId { get; set; } // 查詢條件
                                         // 下拉選單資料來源
        public List<Role> RoleOptions { get; set; } = new List<Role>(); // 下拉選單資料來源


        public List<City> CityOptions { get; set; } = new List<City>();
        public List<District> DistrictOptions { get; set; } = new List<District>();
    }

}

