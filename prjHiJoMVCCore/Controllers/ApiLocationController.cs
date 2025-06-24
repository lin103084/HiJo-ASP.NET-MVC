using Microsoft.AspNetCore.Mvc;
using prjHiJoMVCCore.Models;

namespace prjHiJoMVCCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiLocationController : ControllerBase
    {
        private readonly PrjFriendShipContext _db;

        public ApiLocationController(PrjFriendShipContext context)
        {
            _db = context;
        }

        // GET: /api/ApiLocation/GetDistrictsByCityId/1
        [HttpGet("GetDistrictsByCityId/{cityId}")]
        public IActionResult GetDistrictsByCityId(int cityId)
        {
            var districts = _db.Districts
                .Where(d => d.ParentCityId == cityId)
                .Select(d => new
                {
                    d.DistrictId,
                    d.DistrictName
                })
                .ToList();

            return Ok(districts);
        }
    }
}
