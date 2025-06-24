using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.ViewModels
{

    public class CSwipeDisplayViewModel
    {
        public int Id { get; set; }

        public string SwiperName { get; set; } = "";
        public string TargetName { get; set; } = "";
        public string SwipesActionName { get; set; } = "";
        public DateTime? CreaTime { get; set; }
    }
 }
