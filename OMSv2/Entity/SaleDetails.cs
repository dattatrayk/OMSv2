
namespace OMSv2.Service.Entity
{
    public class SaleDetails : AuditInfo
    {
        /// <summary>
        /// unique identififier of SaleDetails
        /// </summary>
        public int SaleDetailID { get; set; }

        /// <summary>
        /// SaleID of SaleDetails
        /// </summary>
        public int SaleID { get; set; }

        /// <summary>
        /// ItemID of SaleDetails
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// Price of saledetails
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Quantity of items in saledetails
        /// </summary>
        public int Quantity { get; set; }

        public string ItemName { get; set; }

    }
}
