namespace PointOfSaleTerminal
{
    public class Pricing
    {
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public int? VolumeQuantity { get; set; }
        public decimal? VolumePrice { get; set; }
    }
}
