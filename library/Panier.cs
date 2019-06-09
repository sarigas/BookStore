using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
  
 
    public class Panier 
    {
        public List<BoughtBooks> Books { get; set; }

        public Dictionary<string, bool> DiscountApplied;       
        public double TotalAmount { get; set; }
        public Panier()
        {
            Books = new List<BoughtBooks>();
            DiscountApplied = new Dictionary<string, bool>();
        }

        //Check if Panier is Valid
        internal IEnumerable<NameQuantity> IsValid()
        {           
            var countPerBook = this.Books
                                .GroupBy(x => x.BookName)
                                .Select(g => new 
                                {
                                    Name = g.Key,
                                    Count = g.Count()
                                });
           foreach(var item in countPerBook)
            {
                int requested = item.Count;
                int available = this.Books.Where(x => x.BookName == item.Name)
                                .Select(q => q.availableQuantity)
                                .FirstOrDefault();

                if (requested > available)
                {
                    yield return new NameQuantity { Name = item.Name, Quantity = item.Count };
                } 
            }
        }
    }

    public class BoughtBooks 
    {
        public string BookName { get; set; }
        public string CategoryName { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int availableQuantity { get; set; }      
    }

    public class NameQuantity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }


}
