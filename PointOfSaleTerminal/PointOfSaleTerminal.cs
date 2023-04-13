using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSaleTerminal
{
    public class PointOfSaleTerminal
    {
        private List<Pricing> _pricingList;

        private readonly Dictionary<string, int> _cart;

        public PointOfSaleTerminal()
        {
            _pricingList = new List<Pricing>();
            _cart = new Dictionary<string, int>();
        }

        public void SetPricing(List<Pricing> pricingList)
        {
            _pricingList = pricingList;
        }

        public void ScanProduct(string code)
        {
            //check pricing for code exists
            if (_pricingList.Any(p => p.Code == code))
            {
                //add item if new to cart, otherwise increment unit value
                if (!_cart.ContainsKey(code))
                {
                    _cart.Add(code, 1);
                }
                else
                {
                    _cart[code]++;
                }
            }
            else
            {
                throw new ArgumentException($"Product with code {code} does not exist in pricing list");
            }
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (var item in _cart)
            {
                var pricing = _pricingList.FirstOrDefault(p => p.Code == item.Key);

                if (pricing != null)
                {
                    if (pricing.VolumeQuantity.HasValue && pricing.VolumePrice.HasValue)
                    {
                        // calculate volume price if applicable
                        var quotient = item.Value / pricing.VolumeQuantity.Value;
                        var remainder = item.Value % pricing.VolumeQuantity.Value;
                        total += quotient * pricing.VolumePrice.Value + remainder * pricing.UnitPrice;
                    }
                    else
                    {
                        // calculate unit price
                        total += item.Value * pricing.UnitPrice;
                    }
                }
                // Warning: It's possible the pricing has not been set here. See Notes in the README.md
            }

            return total;
        }
    }
}
