using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MicroService.MessageBus;
using MicroService.Services.ShoppingCartAPI.Data;
using MicroService.Services.ShoppingCartAPI.Model;
using MicroService.Services.ShoppingCartAPI.Model.Dto;
using MicroService.Services.ShoppingCartAPI.Services.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MicroService.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;

        public CartApiController(AppDbContext db, IMapper mapper, IProductService productService, ICouponService couponService, IMessageBus messageBus, IConfiguration configuration)
        {
            _db = db;
            this._response = new ResponseDto();
            _mapper = mapper;
            _productService = productService;
            _couponService = couponService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                var header = await _db.CartHeaders.FirstOrDefaultAsync(s => s.UserId == userId);
                var details = await _db.CartDetails.Where(s => s.CartHeaderId == header.CartHeaderId).ToListAsync();

                CartDto cart = new CartDto()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(header),
                    CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(details)
                };

                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);

                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb =
                    await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();

                    //cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    var detail = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                    detail.CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(detail);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _db.CartDetails.FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId
                             && u.CartHeaderId == cartHeaderFromDb.CartHeaderId
                    );

                    if (cartDetailsFromDb == null)
                    {
                        //cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        //_db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        var detail = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                        detail.CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(detail);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        cartDetailsFromDb.Count += cartDto.CartDetails.First().Count;
                        _db.CartDetails.Update(cartDetailsFromDb);
                        await _db.SaveChangesAsync();
                    }
                }

                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }


        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("EmailCartRequest")]
        public async Task<ResponseDto> EmailCartRequest([FromBody] CartDto cartDto)
        {
            try
            {
                await _messageBus.PublicMessage(cartDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCart"));
                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                var cartDetails = await _db.CartDetails.FirstOrDefaultAsync(
                    u => u.CartDetailsId == cartDetailsId);

                int totalCount = _db.CartDetails.Count(s => s.CartHeaderId == cartDetails.CartHeaderId);

                if (totalCount == 1)
                {
                    var header = await _db.CartHeaders.FirstOrDefaultAsync(s => s.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(header);
                }

                _db.CartDetails.Remove(cartDetails);
                await _db.SaveChangesAsync();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
