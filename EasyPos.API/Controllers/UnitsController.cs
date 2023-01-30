using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace EasyPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Unit> GetAll()
        {
            var units=_unitOfWork.Unit.GetAll();
            return units;
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id) 
        {
            var unit=_unitOfWork.Unit.GetFirstOrDefault(x => x.Id == id);
            return Ok(unit);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Unit entity)
        {
            if (entity.Id==0)
            {
                _unitOfWork.Unit.Add(entity);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                entity.ModifiedDate = DateTime.Now;
                _unitOfWork.Unit.Update(entity);
                _unitOfWork.Save();
                return Ok();
            }
         

           
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id) 
        {
            var unit= _unitOfWork.Unit.GetFirstOrDefault(_ => _.Id == id);
            _unitOfWork.Unit.Delete(unit);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
