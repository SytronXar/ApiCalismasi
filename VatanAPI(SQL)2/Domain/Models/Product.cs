﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace VatanAPI.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EMarka Marka { get; set; }
        public double Cost { get; set; }
        public double PreviousCost { get; set; }
        public int CategoryId { get; set; }
        public string Url { get; set; }
        public int NumberInStock { get; set; }
        public string Info { get; set; }
        public double KargoFiyatı { get; set; }
        public Category Category { get; set; }
        public IList<Image> Images { get; set; } = new List<Image>();
    }
}