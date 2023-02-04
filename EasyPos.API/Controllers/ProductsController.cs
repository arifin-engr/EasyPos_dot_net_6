using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Product> GetAll()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category,Brand,Unit");
            return products;
        }


        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var brand = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category,Brand,Unit");
            return Ok(brand);
        }


        [HttpPost]
        [Route("Create")]
        public void Create(Product obj)
        {
           

            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
        }



        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(_ => _.Id == id);
            _unitOfWork.Product.Delete(product);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
