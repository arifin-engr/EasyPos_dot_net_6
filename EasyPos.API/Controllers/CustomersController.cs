using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace EasyPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Customer> GetAll()
        {
            var customers=_unitOfWork.Customer.GetAll();
            return customers;
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id) 
        {
            var customer=_unitOfWork.Customer.GetFirstOrDefault(x => x.Id == id);
            return Ok(customer);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Customer entity)
        {
            if (entity.Id==0)
            {
                _unitOfWork.Customer.Add(entity);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                entity.ModifiedDate = DateTime.Now;
                _unitOfWork.Customer.Update(entity);
                _unitOfWork.Save();
                return Ok();
            }
         

           
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id) 
        {
            var customer= _unitOfWork.Customer.GetFirstOrDefault(_ => _.Id == id);
            _unitOfWork.Customer.Delete(customer);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
