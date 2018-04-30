using System;

namespace RazorHtmlToPdfDemo.Models
{
    public class PricingViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int UserLimit { get; set; }
        public int StorageLimit { get; set; }
        public string Support { get; set; }
    }
}