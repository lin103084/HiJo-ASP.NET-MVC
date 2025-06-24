using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.ViewModels
{

    public class CSwipeWrap
    {
        private Swipe _swipe;

        public CSwipeWrap()
        {
            _swipe = new Swipe();
        }

        public Swipe swipe
        {
            get => _swipe;
            set => _swipe = value;
        }

        // ---------------------- 屬性包裝 ----------------------

        public int Id
        {
            get => _swipe.Id;
            set => _swipe.Id = value;
        }

        [Required(ErrorMessage = "請選擇滑動者")]
        [DisplayName("滑動者 ID")]
        public int SwiperId
        {
            get => _swipe.SwiperId;
            set => _swipe.SwiperId = value;
        }

        [Required(ErrorMessage = "請選擇目標對象")]
        [DisplayName("目標 ID")]
        public int TargetId
        {
            get => _swipe.TargetId;
            set => _swipe.TargetId = value;
        }

        [DisplayName("滑動動作")]
        public int? SwipesAction
        {
            get => _swipe.SwipesAction;
            set => _swipe.SwipesAction = value;
        }

        [DisplayName("建立時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? CreaTime
        {
            get => _swipe.CreaTime;
            set => _swipe.CreaTime = value;
        }

        // 可選：若你要顯示對應名稱可補上這類屬性
        public string? SwiperName => _swipe.Swiper?.UserName;
        public string? TargetName => _swipe.Target?.UserName;
    }
    }
