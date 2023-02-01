using EasyPos.Models.AppVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using EasyPos.Models.AppEntity;

namespace EasyPos.UI.Controllers
{
    public class UnitsController : Controller
    {

        Uri baseUrl = new Uri("https://localhost:7173/api");
       
        private readonly HttpClient _client;
        public UnitsController( )
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
            List<Unit> unitList = new List<Unit>();
            HttpResponseMessage response = _client.GetAsync(baseUrl + "/Units/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                unitList = JsonConvert.DeserializeObject<List<Unit>>(data);
            }


            UnitVM unitVM = new()
            {
                Unit = new(),
                UnitList = unitList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {

                return View(unitVM);
            }
            else
            {
               
                HttpResponseMessage result = _client.GetAsync(baseUrl + "/Units/GetById?id=" + id).Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    unitVM.Unit = JsonConvert.DeserializeObject<Unit>(data);
                }
                return View(unitVM);
            }



           

        }

        [HttpPost]
        
        public IActionResult Upsert(UnitVM obj)
        {
            ModelState.Remove("Unit.Id");
            if (ModelState.IsValid)
            {
                var model = new Unit();
                model = obj.Unit;
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage respone = _client.PostAsync(_client.BaseAddress + "/Units/Create", content).Result;
                if (respone.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            

            return View();
        }
    }
}
