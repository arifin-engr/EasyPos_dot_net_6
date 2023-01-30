using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace EasyPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Brand> GetAll()
        {
            var brands=_unitOfWork.Brand.GetAll();
            return brands;
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id) 
        {
            var brand=_unitOfWork.Brand.GetFirstOrDefault(x => x.Id == id);
            return Ok(brand);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Brand entity)
        {
            if (entity.Id==0)
            {
                _unitOfWork.Brand.Add(entity);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                entity.ModifiedDate = DateTime.Now;
                _unitOfWork.Brand.Update(entity);
                _unitOfWork.Save();
                return Ok();
            }
         

           
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id) 
        {
            var brand= _unitOfWork.Brand.GetFirstOrDefault(_ => _.Id == id);
            _unitOfWork.Brand.Delete(brand);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
