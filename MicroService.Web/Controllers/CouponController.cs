using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Model;
using MicroService.Web.Model.Dto;
using MicroService.Web.Service.IService;
using Newtonsoft.Json;

namespace MicroService.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new List<CouponDto>();

            ResponseDto? response = await _couponService.GetAllCouponAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon Created Successful";
                    return RedirectToAction(nameof(CouponIndex));
                }

                TempData["error"] = response?.Message;
            }

            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int CouponId)
        {
            ResponseDto? response = await _couponService.GetCouponsByIdAsync(CouponId);

            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }

            TempData["error"] = response?.Message;

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon Deleted Successful";
                return RedirectToAction(nameof(CouponIndex));
            }

            TempData["error"] = response?.Message;

            return View(couponDto);
        }
    }
}
