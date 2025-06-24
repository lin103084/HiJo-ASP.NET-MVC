using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjHiJoMVCCore.Models;
using prjHiJoMVCCore.ViewModels;

namespace prjHiJoMVCCore.Controllers
{
    public class SwipesController : Controller
    {
        private readonly PrjFriendShipContext _db;

        public SwipesController(PrjFriendShipContext context)
        {
            _db = context;
        }

        // GET: Swipes
        public IActionResult List()
        {
            var result = (from s in _db.Swipes
                          join swiper in _db.Members on s.SwiperId equals swiper.Id
                          join target in _db.Members on s.TargetId equals target.Id
                          join status in _db.StatTypes on s.SwipesAction equals status.StatId
                          orderby s.Id descending
                          select new CSwipeDisplayViewModel
                          {
                              Id = s.Id,
                              SwiperName = swiper.UserName,
                              TargetName = target.UserName,
                              SwipesActionName = status.StatName,
                              CreaTime = s.CreaTime
                          }).ToList();

            return View(result);
            
        }
       

        // GET: Swipes/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Swipe s)
        {
            
            PrjFriendShipContext db = new PrjFriendShipContext();
            
                db.Swipes.Add(s);
                db.SaveChanges();
           
            return RedirectToAction("List");
        }

        // GET: Swipes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            PrjFriendShipContext db = new PrjFriendShipContext();
            Swipe x = db.Swipes.FirstOrDefault(t => t.Id == id);
            if (x == null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public IActionResult Edit(Swipe swipUi)
        {
            PrjFriendShipContext db = new PrjFriendShipContext();
            Swipe custDb = db.Swipes.FirstOrDefault(t => t.Id == swipUi.Id);
            if (custDb == null)
                return RedirectToAction("List");
            custDb.SwiperId = swipUi.SwiperId;
            custDb.TargetId = swipUi.TargetId;
            custDb.SwipesAction = swipUi.SwipesAction;
           
            db.SaveChanges();
            return RedirectToAction("List");
        }

        // GET: Swipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swipe = await _db.Swipes
                .Include(s => s.Swiper)
                .Include(s => s.Target)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swipe == null)
            {
                return NotFound();
            }

            return View(swipe);
        }

        // POST: Swipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swipe = await _db.Swipes.FindAsync(id);
            if (swipe != null)
            {
                _db.Swipes.Remove(swipe);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool SwipeExists(int id)
        {
            return _db.Swipes.Any(e => e.Id == id);
        }
    }
}
