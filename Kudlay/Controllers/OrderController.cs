using BAL.ModelsDTO;
using BAL.OrderService;
using Kudlay.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kudlay.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("get-new-order")]
        public IActionResult GetOrderNew(int? page, int pagesize = 30)
        {
            var result = _orderService.GetOrderNew(page, pagesize);
            return Ok(result);
        }
        [HttpPut]
        [Route("edit-status-order")]
        public IActionResult EditStatusOrder(NewStatus newStatus)
        {
            _orderService.EditStatusOrder(newStatus.Id);
            return Ok();
        }

        [HttpGet]
        [Route("get-pending-order")]
        public IActionResult GetOrderPending(int? page, int pagesize = 30)
        {
            var result = _orderService.GetOrderPending(page, pagesize);
            return Ok(result);
        }
        [HttpPost]
        [Route("add-order")]
        public IActionResult AddOrder(OrderDTO orderDTO)
        {
           
            bool result = _orderService.AddOrder(orderDTO);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "Дата уже занята !" });
            }
        }
        [HttpGet]
        [Route("get-completed-order")]
        public IActionResult GetOrderCompleted(int? page, int pagesize = 30)
        {
            var result = _orderService.GetOrderCompleted(page, pagesize);
            return Ok(result);

        }
        [HttpPost]
        [Route("completed-order")]
        public IActionResult CompletedOrder(NewStatus newStatus)
        {
            _orderService.CompletedOrder(newStatus.Id);
            return Ok();
        }
        [HttpGet]
        [Route("get-canceled-order")]
        public IActionResult GetOrderCanceled(int? page, int pagesize = 30)
        {
            var result = _orderService.GetOrderCanceled(page, pagesize);
            return Ok(result);

        }
        [HttpPost]
        [Route("canceled-order")]
        public IActionResult CanceledOrder(NewStatus newStatus)
        {
            _orderService.CanceledOrder(newStatus.Id);
            return Ok();
        }
        [HttpGet]
        [Route("get-search")]
        public IActionResult GetSearch(string text, int? page, int pagesize = 30)
        {
            var result = _orderService.GetSearch(text, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-search-date")]
        public IActionResult GetSearchDate(DateTime dateSearch, int? page, int pagesize = 30)
        {
            var result = _orderService.GetSearchDate(dateSearch, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("sort-price")]
        public IActionResult SortPrice(string sortParam, int? page, int pagesize = 30)
        {
            var result = _orderService.SortPrice(sortParam, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("sort-date")]
        public IActionResult SortDate(string sortParam, int? page, int pagesize = 30)
        {
            var result = _orderService.SortDate(sortParam, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-search-orders-completed")]
        public IActionResult GetSearchOrderCompleted(string text, int? page, int pagesize = 30)
        {
            var result = _orderService.GetSearchOrderCompleted(text, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-search-orders-canceled")]
        public IActionResult GetSearchOrderCanceled(string text, int? page, int pagesize = 30)
        {
            var result = _orderService.GetSearchOrderCanceled(text, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("sort-price-orders-completed")]
        public IActionResult SortPriceOrderCompleted(string sortParam, int? page, int pagesize = 30)
        {
            var result = _orderService.SortPriceOrderCompleted(sortParam, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("sort-date-orders-completed")]
        public IActionResult SortDateOrderCompleted(string sortParam, int? page, int pagesize = 30)
        {
            var result = _orderService.SortPriceOrderCompleted(sortParam, page, pagesize);
            return Ok(result);
        }
        [HttpGet]
        [Route("get-edit-order")]
        public IActionResult GetEditOrder(string idOrder)
        {
            if(int.TryParse(idOrder, out int id))
            {
                var result = _orderService.GetEditOrder(id);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("edit-order")]
        public IActionResult EditOrder(OrderDTO order)
        {
            bool result = _orderService.EditOrder(order);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "Дата уже занята !" });
            }
        }
    }
}
