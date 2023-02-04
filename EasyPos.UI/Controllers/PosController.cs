using EasyPos.Models.AppEntity;
using EasyPos.Models.AppVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EasyPos.UI.Controllers
{
    public class PosController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7173/api");

        private readonly HttpClient _client;
        public PosController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Product> productList = new List<Product>();
            List<Customer> customerList = new List<Customer>();
            List<Category> categoryList = new List<Category>();


            //Product List
            HttpResponseMessage productResponse = _client.GetAsync(baseUrl + "/Products/GetAll").Result;
            if (productResponse.IsSuccessStatusCode)
            {
                string data = productResponse.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<Product>>(data);
            }



            //Product List
            HttpResponseMessage customerResponse = _client.GetAsync(baseUrl + "/Customers/GetAll").Result;
            if (customerResponse.IsSuccessStatusCode)
            {
                string data = customerResponse.Content.ReadAsStringAsync().Result;
                customerList = JsonConvert.DeserializeObject<List<Customer>>(data);
            }



            //Category List
            HttpResponseMessage Categoryresponse = _client.GetAsync(baseUrl + "/Categories/GetAll").Result;
            if (Categoryresponse.IsSuccessStatusCode)
            {
                string data = Categoryresponse.Content.ReadAsStringAsync().Result;
                categoryList = JsonConvert.DeserializeObject<List<Category>>(data);
            }


            PosVM purchaseVM = new()
            {
                Sales = new(),
                ProductList = productList.Select(i => new SelectListItem
                {
                    Text = i.Name + " - " + i.Code,
                    Value = i.Id.ToString()
                }),

                CustomerList = customerList.Select(i => new SelectListItem
                {
                    Text = i.Name + " - " + i.PhoneNumber.ToString(),
                    Value = i.Id.ToString()
                }),
                CategoryList = categoryList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            return View(purchaseVM);
        }
    }
}
