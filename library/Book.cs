﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class Book
    {
        public string Name { get; set; }
        public string Category { get; set; }     
        public double Price { get; set; }
        public int Quantity { get; set; }
        
    }

    public class Category
    {
        public string Name { get; set; }
        public double Discount { get; set; }
    }
    
}
