using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using RazorHtmlToPdfDemo.Models;
using RazorHtmlToPdfDemo.Services;

namespace RazorHtmlToPdfDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public HomeController(IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Pdf([FromServices] INodeServices nodeServices)
        {
            var htmlContent = await _razorViewToStringRenderer.RenderViewToStringAsync("Home/Pricing", new List<PricingViewModel>()
            {
                new PricingViewModel
                {
                    Id = 1,
                    Price = 0.0,
                    StorageLimit = 2,
                    Support = "Email support",
                    Type = "Free",
                    UserLimit = 10
                },
                new PricingViewModel
                {
                    Id = 2,
                    Price = 15,
                    StorageLimit = 2,
                    Support = "Priority email support",
                    Type = "Pro",
                    UserLimit = 20
                },
                new PricingViewModel
                {
                    Id = 3,
                    Price = 29,
                    StorageLimit = 2,
                    Support = "Phone and email support",
                    Type = "Enterprise"
                }
            });

            var result = await nodeServices.InvokeAsync<byte[]>("./NodeServices/pdf", htmlContent);

            return File(result, "application/pdf");
        }
    }
}
