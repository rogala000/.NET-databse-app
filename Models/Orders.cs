using System;

namespace RogalskiJaroslaw
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderComments { get; set; }
        public string OrderOrigin { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
    }
}
