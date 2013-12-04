using System.Collections.Generic;
using MvcMusicStoreHocLapTrinhWeb.Models;

namespace MvcMusicStoreHocLapTrinhWeb.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}