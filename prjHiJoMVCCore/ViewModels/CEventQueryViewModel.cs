using Microsoft.AspNetCore.Mvc.Rendering;

namespace prjHiJoMVCCore.ViewModels
{
    public class CEventQueryViewModel
    {
        // 為了查詢用
        public string? txtKeyword { get; set; }
        public int? selectedEventType { get; set; }
        public int? selectedCity { get; set; }

        // 下拉選單用
        public List<SelectListItem> eventTypeOptions { get; set; }
        public List<SelectListItem> cityOptions { get; set; }

        // 查詢結果
        public List<CEventListWrap> resultList { get; set; }
    }
}
