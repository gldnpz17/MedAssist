using System;
using System.Collections.Generic;
using System.Text;

namespace UILibrary.Models
{
    public class CartModel
    {
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
    }
}
