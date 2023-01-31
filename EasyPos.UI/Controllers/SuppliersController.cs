using EasyPos.Models.AppVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using EasyPos.Models.AppEntity;

namespace EasyPos.UI.Controllers
{
    public class SuppliersController : Controller
    {

        Uri baseUrl = new Uri("https://localhost:7173/api");
       
        private readonly HttpClient _client;
        public SuppliersController( )
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
            SupplierVM supplierVM = new()
            {
                Supplier = new Supplier()
            };

            if (id!=null)
            {
                
                HttpResponseMessage response = _client.GetAsync(baseUrl + "/Suppliers/GetById?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    supplierVM.Supplier = JsonConvert.DeserializeObject<Supplier>(data);

                }
                return View(supplierVM);
            }
            return View(supplierVM);
           
        }

        [HttpPost]
        
        public IActionResult Upsert(SupplierVM obj)
        {
            ModelState.Remove("Category.Id");
            if (ModelState.IsValid)
            {
                var model = new Supplier();
                model = obj.Supplier;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage respone = _client.PostAsync(_client.BaseAddress + "/Suppliers/Create", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            

            return View();
        }
    }
}
