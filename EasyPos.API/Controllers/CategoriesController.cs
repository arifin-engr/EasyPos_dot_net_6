using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace EasyPos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Category> GetAll()
        {
            var categories=_unitOfWork.Category.GetAll();
            return categories;
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id) 
        {
            var category=_unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            return Ok(category);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Category entity)
        {
            if (entity.Id==0)
            {
                _unitOfWork.Category.Add(entity);
                _unitOfWork.Save();
                return Ok();
            }
            else
            {
                entity.ModifiedDate = DateTime.Now;
                _unitOfWork.Category.Update(entity);
                _unitOfWork.Save();
                return Ok();
            }
         

           
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id) 
        {
            var category= _unitOfWork.Category.GetFirstOrDefault(_ => _.Id == id);
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
