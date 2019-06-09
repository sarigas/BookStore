using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class NotEnoughInventoryException : Exception
    {
        private List<NameQuantity> InvalidDemand;

        public NotEnoughInventoryException(List<NameQuantity> invalidDemand)
        {

            this.InvalidDemand = invalidDemand;
        }

        public IEnumerable<NameQuantity> Missing
        {
            get
            {
                foreach (var item in InvalidDemand)
                    yield return new NameQuantity { Name = item.Name, Quantity = item.Quantity };
            }
        }
    }
}
