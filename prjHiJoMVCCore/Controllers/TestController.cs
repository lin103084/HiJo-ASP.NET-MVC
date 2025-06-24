using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.Controllers
{
    public class TestController : Controller
    {
        private readonly PrjFriendShipContext _db;

        public TestController(PrjFriendShipContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Member> datas = from c in _db.Members
                                        select c;
            return View(datas.Take(10));
        }
    }
}
