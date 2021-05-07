using BAL.ModelsDTO;
using BAL.StatusService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kudlay.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ISatusService _satusService;
        public StatusController(ISatusService satusService)
        {
            _satusService = satusService;
        }

        [HttpGet]
        [Route("get-status")]
        public IActionResult GetStatus(int? page, int pagesize = 30)
        {
            var result = _satusService.GetStatus(page, pagesize);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatus(int id)
        {
            
            bool result = _satusService.DeleteStatus(id);
            
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
        [Route("add-status")]
        public IActionResult AddStatus(StatusDTO statusDTO)
        {
            if(statusDTO.Name.Length > 0)
            {
                _satusService.AddStatus(statusDTO);
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "Пустая строка !" });
            }
        }
            

    }
}
