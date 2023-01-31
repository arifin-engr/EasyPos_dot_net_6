﻿using EasyPos.Models.AppVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using EasyPos.Models.AppEntity;

namespace EasyPos.UI.Controllers
{
    public class BrandsController : Controller
    {

        Uri baseUrl = new Uri("https://localhost:7173/api");
       
        private readonly HttpClient _client;
        public BrandsController( )
        {
           
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int?id)
        {
            BrandVM brandVM = new()
            {
                Brand = new Brand()
            };

            if (id!=null)
            {
                
                HttpResponseMessage response = _client.GetAsync(baseUrl + "/Brands/GetById?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    brandVM.Brand = JsonConvert.DeserializeObject<Brand>(data);

                }
                return View(brandVM);
            }
            return View(brandVM);
           
        }

        [HttpPost]
        
        public IActionResult Upsert(BrandVM obj)
        {
            ModelState.Remove("Brand.Id");
            if (ModelState.IsValid)
            {
                var model = new Brand();
                model = obj.Brand;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage respone = _client.PostAsync(_client.BaseAddress + "/Brands/Create", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            

            return View();
        }
    }
}
