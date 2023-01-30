using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace EasyPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SuppliersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Supplier> GetAll()
        {
            var Suppliers = _unitOfWork.Supplier.GetAll();
            return Suppliers;
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id) 
        {
            var supplier=_unitOfWork.Supplier.GetFirstOrDefault(x => x.Id == id);
            return Ok(supplier);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Supplier entity)
        {
            if (entity.Id==0)
            {
                _unitOfWork.Supplier.Add(entity);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                entity.ModifiedDate = DateTime.Now;
                _unitOfWork.Supplier.Update(entity);
                _unitOfWork.Save();
                return Ok();
            }
         

           
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id) 
        {
            var supplier= _unitOfWork.Supplier.GetFirstOrDefault(_ => _.Id == id);
            _unitOfWork.Supplier.Delete(supplier);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
