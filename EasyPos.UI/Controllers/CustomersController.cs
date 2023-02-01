using EasyPos.Models.AppVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using EasyPos.Models.AppEntity;

namespace EasyPos.UI.Controllers
{
    public class CustomersController : Controller
    {

        Uri baseUrl = new Uri("https://localhost:7173/api");
       
        private readonly HttpClient _client;
        public CustomersController( )
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
            CustomerVM customerVM = new()
            {
                Customer = new Customer()
            };

            if (id!=null)
            {
                
                HttpResponseMessage response = _client.GetAsync(baseUrl + "/Customers/GetById?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    customerVM.Customer = JsonConvert.DeserializeObject<Customer>(data);

                }
                return View(customerVM);
            }
            return View(customerVM);
           
        }

        [HttpPost]
        
        public IActionResult Upsert(CustomerVM obj)
        {
            ModelState.Remove("CustomerId.Id");
            if (ModelState.IsValid)
            {
                var model = new Customer();
                model = obj.Customer;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage respone = _client.PostAsync(_client.BaseAddress + "/Customers/Create", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            

            return View();
        }
    }
}
