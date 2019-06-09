using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace library
{
   public  class BookStore : IBookStore
    {
        public BookStore()
        {
            this.Category = new List<Category>();
            this.Catalog = new List<Book>();
        }
        public List<Category> Category { get; set; }
        public List<Book> Catalog { get; set; }   


        public void Import(string storeData)
        {
            var data =  JsonConvert.DeserializeObject<BookStore>(storeData);
            this.Category.AddRange(data.Category);
            this.Catalog.AddRange(data.Catalog);
        }

        public int Quantity(string name)
        {
            return this.Catalog
                .Where(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()))
                .Select(c => c.Quantity)
                .FirstOrDefault();
        }
        public double Buy(params string[] basket)
        {          
            var panier = new Panier();
           foreach(var item in basket)
            {
                panier.Books.Add(GetBookDetails(item));
            }
            var invalidItems = panier.IsValid();
            bool valid = (!invalidItems.Any());
            if (!valid) throw new NotEnoughInventoryException(invalidItems.ToList());
            return CalculatePrice(panier);
        }
        private double CalculatePrice(Panier panier)
        {
            bool applyDiscount = false;
            panier.TotalAmount = 0;                 
           foreach(var book in panier.Books)
            {
                int categoryCount = panier.Books.Count(x => x.CategoryName.Contains                                 (book.CategoryName));
                int nameCount = panier.Books.Count(x => x.BookName == book.BookName);

                if (categoryCount == 1) applyDiscount = false;
                else 
                {
                    if (nameCount > 1)
                    {
                        if (panier.DiscountApplied.Any())
                        {
                            bool value = panier.DiscountApplied.Where(x => x.Key ==                                 book.BookName).Select(v => v.Value).FirstOrDefault();

                            if (value) applyDiscount = false;
                            else applyDiscount = true;
                        }
                        else applyDiscount = true;
                    }
                    else applyDiscount = true;
                }

                if (applyDiscount)
                {
                    panier.DiscountApplied.Add(book.BookName, true);
                    panier.TotalAmount = panier.TotalAmount + ((book.Price) * (1 - book.Discount));
                }
                else panier.TotalAmount += book.Price;              
            }
            return panier.TotalAmount;
        }
       
        //Details of selected book
        private BoughtBooks GetBookDetails(string book)
        {
            return (from b in this.Catalog
                    join
                    ct in this.Category
                    on b.Category equals ct.Name
                    where b.Name == book
                    select new BoughtBooks
                    {
                        BookName = b.Name,
                        CategoryName = b.Category,
                        Discount = ct.Discount,
                        Price = b.Price,
                        availableQuantity = b.Quantity
                    }).FirstOrDefault();
           
        }

    }
}

