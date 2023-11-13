using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Web.Models;

namespace MicroService.Web.Models
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
