﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Services.OrderAPI.Model.Dto;

namespace MicroService.Services.OrderAPI.Model.Dto
{
    public class OrderHeaderDto
    {
        public int OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }

        public double Discount { get; set; }
        public double OrderTotal { get; set; }

        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }

        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}
