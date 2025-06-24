using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace prjHiJoMVCCore.ViewModels
{
    public class CSwipeCreateViewModel
    {
        public int Id { get; set; }
        [DisplayName("滑動者ID/姓名")]
        public int swiperID { get; set; }
        [DisplayName("被滑動者ID/姓名")]
        public int targetID { get; set; }
        [DisplayName("配對狀態")]
        public int swipesAction { get; set; }
        public DateTime? creaTime { get; set; }
        

        public List<SelectListItem> MemberOptions { get; set; } = new();
        public List<SelectListItem> ActionOptions { get; set; } = new();
    }
}
