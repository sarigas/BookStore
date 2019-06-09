using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
   public interface IBookStore
    {
        void Import(string storeData);
        int Quantity(string name);
        double Buy(params string[] basket);
    }
}
