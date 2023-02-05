using EasyPos.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace EasyPos.API.Controllers
{
    public class SalesController : Controller
    {

        private readonly IWebHostEnvironment _webHost;
        private IUnitOfWork _unitOfWork;
        public SalesController(IWebHostEnvironment webHost, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var sales = _unitOfWork.Sales.GetFirstOrDefault(x => x.Id == id);
            return Ok(sales);
        }

        [HttpGet]
        [Route("GetSalesItem")]
        public IActionResult GetSalesItem(int id)
        {
            var salesItem = _unitOfWork.Sales.GetSalesItems(id);
            return Ok(salesItem);
        }

        


    }
}
