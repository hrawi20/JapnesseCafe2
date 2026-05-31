using System;

namespace JapnesseCafe.Models
{
    [Serializable]
    public class CartItem
    {
        public string CartKey { get; set; }
        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
    }
}
