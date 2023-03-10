using EasyPos.Models.AppVM;
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
        private readonly IWebHostEnvironment _webHost;
        public BrandsController(IWebHostEnvironment webHost)
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
        
        public IActionResult Upsert(BrandVM obj, IFormFile? file)
        {
            ModelState.Remove("Brand.Id");
            if (ModelState.IsValid)
            {
                var model = new Brand();
                model = obj.Brand;
                string mmRoot = _webHost.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string filePath = Path.Combine(mmRoot, @"img\BrandImages");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var fileExtention = file.FileName;
                    if (obj.Brand.LogoUrl != null)
                    {
                        string oldPath = Path.Combine(mmRoot, obj.Brand.LogoUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName + fileExtention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.LogoUrl = @"\img\BrandImages\" + fileName + fileExtention;

                }


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
