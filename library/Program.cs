using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    class Program
    {
        static void Main(string[] args)
        {
            BookStore store = new BookStore();
            string json = "";
            using (StreamReader r = new StreamReader("data.json"))
            {
               json = r.ReadToEnd();
            }
            //Import
            store.Import(json);

            //Quantity
            Console.WriteLine("Enter the book name :");
            var name = Console.ReadLine();
            int quantity =  store.Quantity(name);
            Console.WriteLine(quantity);

            //Price

            Console.WriteLine("Enter books to buy");
            string[] books = Console.ReadLine().Split(',');
            try
            {
                var totalPrice = store.Buy(books);
                Console.WriteLine(totalPrice);
            }catch(NotEnoughInventoryException ex)
            {
                Console.WriteLine("Not enough copies!!!");
                foreach (var m in ex.Missing)
                {
                    Console.WriteLine("{0} copies of {1} are currently unavailable", m.Quantity,m.Name);
                }
            }
            

            Console.ReadLine();

        }
    }
}
