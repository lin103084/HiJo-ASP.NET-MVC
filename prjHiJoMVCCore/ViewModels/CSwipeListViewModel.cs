using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.ViewModels
{

    public class CSwipeListViewModel
    {
        public IEnumerable<CSwipeWrap>? Id { get; set; }

        public int SwiperId { get; set; }

        public int TargetId { get; set; }

        public int? SwipesAction { get; set; }

        public DateTime? CreaTime { get; set; }

        public virtual Member Swiper { get; set; } = null!;

        public virtual Member Target { get; set; } = null!;
    }
    }
