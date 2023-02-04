using EasyPos.Models.AppVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using EasyPos.Models.AppEntity;

namespace EasyPos.UI.Controllers
{
    public class ProductsController : Controller
    {

        Uri baseUrl = new Uri("https://localhost:7173/api");
       
        private readonly HttpClient _client;
        private readonly IWebHostEnvironment _webHost;
        public ProductsController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
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

            List<Category> categoriesList = new List<Category>();
            List<Brand> brandList = new List<Brand>();
            List<Unit> unitList = new List<Unit>();

            HttpResponseMessage Categoryresponse = _client.GetAsync(baseUrl + "/Categories/GetAll").Result;
            if (Categoryresponse.IsSuccessStatusCode)
            {
                string data = Categoryresponse.Content.ReadAsStringAsync().Result;
                categoriesList = JsonConvert.DeserializeObject<List<Category>>(data);
            }

            HttpResponseMessage Brandresponse = _client.GetAsync(baseUrl + "/Brands/GetAll").Result;
            if (Brandresponse.IsSuccessStatusCode)
            {
                string data = Brandresponse.Content.ReadAsStringAsync().Result;
                brandList = JsonConvert.DeserializeObject<List<Brand>>(data);
            }

           
            HttpResponseMessage Unitresponse = _client.GetAsync(baseUrl + "/Units/GetAll").Result;
            if (Unitresponse.IsSuccessStatusCode)
            {
                string data = Unitresponse.Content.ReadAsStringAsync().Result;
                unitList = JsonConvert.DeserializeObject<List<Unit>>(data);
            }


            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = categoriesList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                UnitList = unitList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                BrandList = brandList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),


            };

            if (id!=null)
            {
                
                HttpResponseMessage response = _client.GetAsync(baseUrl + "/Products/GetById?id=" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    productVM.Product = JsonConvert.DeserializeObject<Product>(data);

                }
                return View(productVM);
            }
            return View(productVM);
           
        }

        [HttpPost]
        
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            ModelState.Remove("Product.Id");
            if (ModelState.IsValid)
            {
                var model = new Product();
                model = obj.Product;
                string mmRoot = _webHost.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string filePath = Path.Combine(mmRoot, @"img\ProductImages");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var fileExtention = file.FileName;
                    if (obj.Product.ImageUrl != null)
                    {
                        string oldPath = Path.Combine(mmRoot, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName + fileExtention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.ImageUrl = @"\img\ProductImages\" + fileName + fileExtention;

                }


                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage respone = _client.PostAsync(_client.BaseAddress + "/Products/Create", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            

            return View();
        }
    }
}
