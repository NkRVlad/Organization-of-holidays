using BAL.CategoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.ModelsDTO;
namespace Kudlay.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("get-category")]
        public IActionResult GetCategory(int? page, int pagesize = 30)
        {
            var result = _categoryService.GetCategory(page, pagesize);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            
            bool result = _categoryService.DeleteCategory(id);
            
            if(result)
            {
                return Ok(new { Message = "Удалено !" }); 
            }
            else
            {
                return BadRequest(new { Message = "Не найдено !" });
            }
                
        }

        [HttpPost]
        [Route("add-category")]
        public IActionResult AddStatus(CategoryDTO categoryDTO)
        {
            if(categoryDTO.Name.Length > 0)
            {
                _categoryService.AddCategory(categoryDTO);
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "Пустая строка !" });
            }
        }
    }
}
