using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjHiJoMVCCore.Models;
using prjHiJoMVCCore.ViewModels;
using System.CodeDom;

namespace prjHiJoMVCCore.Controllers
{
    public class MemberController : Controller
    {
        private readonly PrjFriendShipContext _db;
        private readonly IWebHostEnvironment _env;

        public MemberController(PrjFriendShipContext context, IWebHostEnvironment environment)
        {
            _db = context;
            _env = environment;
        }




        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public IActionResult List(CMemberListViewModel vm)
        {
            var query = _db.Members.AsQueryable();

            if (!string.IsNullOrEmpty(vm.Keyword))
                query = query.Where(m =>
                    m.Id.ToString() == vm.Keyword ||
                    m.UserName.Contains(vm.Keyword) ||
                    m.Email.Contains(vm.Keyword));

            if (vm.CityId.HasValue)
                query = query.Where(m => m.CityId == vm.CityId);

            if (vm.DistrictId.HasValue)
                query = query.Where(m => m.DistrictId == vm.DistrictId);

            if (!string.IsNullOrEmpty(vm.Sex))
                query = query.Where(m => m.Sex == vm.Sex);

            if (vm.IsActive.HasValue)
                query = query.Where(m => m.IsActive == vm.IsActive);

            if (vm.IsVerified.HasValue)
                query = query.Where(m => m.IsVerified == vm.IsVerified);
            
            if (vm.RoleId.HasValue)
                query = query.Where(m => m.MemberRoles.Any(mr => mr.RoleId == vm.RoleId));

        

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / vm.PageSize);

            // 抓出 role 資訊並包進 CMemberWrap
            var members = query
                .Include(m => m.City)
                .Include(m => m.District)
                .Include(m => m.MemberRoles)
                    .ThenInclude(mr => mr.Role)
                .OrderByDescending(m => m.Id)
                .Skip((vm.Page - 1) * vm.PageSize)
                .Take(vm.PageSize)
                .ToList()
                .Select(m => new CMemberWrap
                {
                    member = m,
                    RoleName = m.MemberRoles.FirstOrDefault()?.Role?.Name ?? ""
                });

            vm.Members = members;
            vm.TotalPages = totalPages;
            vm.CityOptions = _db.Cities.ToList();
            vm.DistrictOptions = _db.Districts.ToList();
            vm.RoleOptions = _db.Roles.ToList(); // 提供角色選項

            return View(vm);
        }





        // ✅ Controller: MemberController.cs
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CMemberFormViewModel
            {
                RoleOptions = _db.Roles.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CMemberFormViewModel vm)
        {
            if (_db.Members.Any(m => m.Email.ToLower() == vm.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "此 Email 已被註冊");
            }

            if (!ModelState.IsValid)
            {
                vm.RoleOptions = _db.Roles.ToList();
                return View(vm);
            }

            string? avatarFileName = null;
            if (vm.AvatarFile != null && vm.AvatarFile.Length > 0)
            {
                string ext = Path.GetExtension(vm.AvatarFile.FileName);
                avatarFileName = Guid.NewGuid().ToString() + ext;
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/avatars", avatarFileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await vm.AvatarFile.CopyToAsync(stream);
                }
            }

            var member = new Member
            {
                UserName = vm.UserName,
                Email = vm.Email,
                PassWordHash = "defaultHash",
                PassWordSalt = "defaultSalt",
                IsActive = true,
                IsVerified = false,
                LastOnline = DateTime.Now,
                AvatarPath = avatarFileName != null ? $"/images/avatars/{avatarFileName}" : null
            };

            try
            {
                _db.Members.Add(member);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    ModelState.AddModelError("Email", "此 Email 已重複，請改用其他信箱");
                    vm.RoleOptions = _db.Roles.ToList();
                    return View(vm);
                }
                throw;
            }

            if (vm.SelectedRoleIds == null || !vm.SelectedRoleIds.Any())
            {
                var defaultRole = _db.Roles.FirstOrDefault(r => r.Name == "User");
                if (defaultRole != null)
                {
                    vm.SelectedRoleIds.Add(defaultRole.Id);
                }
            }

            foreach (var roleId in vm.SelectedRoleIds)
            {
                _db.MemberRoles.Add(new MemberRole
                {
                    MemberId = member.Id,
                    RoleId = roleId
                });
            }

            _db.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult CheckEmailExists(string email)
        {
            bool exists = _db.Members.Any(m => m.Email == email);
            return Json(new { exists });
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _db.Members
                .Include(m => m.MemberRoles)
                .ThenInclude(mr => mr.Role)
                .FirstOrDefault(m => m.Id == id);

            if (member == null) return NotFound();

            var vm = new CMemberFormViewModel
            {
                Id = member.Id,
                UserName = member.UserName,
                Email = member.Email,
                Sex = member.Sex,
                Birthday = member.Birthday,
                CityId = member.CityId,
                DistrictId = member.DistrictId,
                Resume = member.Resume,
                IsActive = member.IsActive,
                IsVerified = member.IsVerified,
                AvatarPath = member.AvatarPath,
                SelectedRoleIds = member.MemberRoles.Select(mr => mr.RoleId).ToList(),
                RoleOptions = _db.Roles.ToList(),
                CityOptions = _db.Cities.ToList(),
                DistrictOptions = _db.Districts.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CMemberFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.RoleOptions = _db.Roles.ToList();
                vm.CityOptions = _db.Cities.ToList();
                vm.DistrictOptions = _db.Districts.ToList();
                return View(vm);
            }

            var member = _db.Members.Include(m => m.MemberRoles).FirstOrDefault(m => m.Id == vm.Id);
            if (member == null) return NotFound();

            // 更新欄位
            member.UserName = vm.UserName;
            member.Sex = vm.Sex;
            member.Birthday = vm.Birthday;
            member.CityId = vm.CityId;
            member.DistrictId = vm.DistrictId;
            member.Resume = vm.Resume;
            member.IsActive = vm.IsActive;
            member.IsVerified = vm.IsVerified;

            // 處理頭貼
            if (vm.AvatarFile != null && vm.AvatarFile.Length > 0)
            {
                var ext = Path.GetExtension(vm.AvatarFile.FileName);
                var fileName = Guid.NewGuid().ToString() + ext;
                var savePath = Path.Combine(_env.WebRootPath, "images/avatars", fileName);
                using var stream = new FileStream(savePath, FileMode.Create);
                await vm.AvatarFile.CopyToAsync(stream);
                member.AvatarPath = $"/images/avatars/{fileName}";
            }

            // 處理角色
            _db.MemberRoles.RemoveRange(_db.MemberRoles.Where(mr => mr.MemberId == member.Id));
            var selectedIds = vm.SelectedRoleIds?.Any() == true
                ? vm.SelectedRoleIds
                : new List<int> { _db.Roles.First(r => r.Name == "User").Id };

            foreach (var rid in selectedIds)
                _db.MemberRoles.Add(new MemberRole { MemberId = member.Id, RoleId = rid });

            await _db.SaveChangesAsync();
            return RedirectToAction("List");
        }





        [HttpGet]
public IActionResult Delete(int id)
{
    var member = _db.Members
        .Include(m => m.City)
        .Include(m => m.District)
        .FirstOrDefault(m => m.Id == id);

    if (member == null)
        return NotFound();

    var vm = new CMemberFormViewModel
    {
        Id = member.Id,
        UserName = member.UserName,
        Email = member.Email,
        CityId = member.CityId,
        DistrictId = member.DistrictId,
        Resume = member.Resume,
        IsActive = member.IsActive,
        IsVerified = member.IsVerified,
        AvatarPath = member.AvatarPath,
        Sex = member.Sex,
        Birthday = member.Birthday,
        CityOptions = _db.Cities.ToList(),
        DistrictOptions = _db.Districts.ToList()
    };

    return View(vm);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var member = _db.Members.FirstOrDefault(m => m.Id == id);
    if (member == null)
        return NotFound();

    // 刪除對應的 MemberRoles 資料（避免 FK 限制）
    var roles = _db.MemberRoles.Where(mr => mr.MemberId == id);
    _db.MemberRoles.RemoveRange(roles);

    _db.Members.Remove(member);
    _db.SaveChanges();

    return RedirectToAction("List");
}
















    }
}
