using BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.OrderService
{
    public interface IOrderService
    {
        PageResult<OrderDTO> GetOrderNew(int? page, int pagesize);
        PageResult<OrderDTO> GetOrderPending(int? page, int pagesize);
        void EditStatusOrder(int id);
        bool EditOrder(OrderDTO order);
        bool AddOrder(OrderDTO orderDTO);
        PageResult<OrderDTO> GetOrderCompleted(int? page, int pagesize);
        void CompletedOrder(int id);
        PageResult<OrderDTO> GetOrderCanceled(int? page, int pagesize);
        void CanceledOrder(int id);
        PageResult<OrderDTO> GetSearch(string text, int? page, int pagesize);
        PageResult<OrderDTO> GetSearchDate(DateTime dateSearch, int? page, int pagesize);
        PageResult<OrderDTO> GetSearchOrderCompleted(string text, int? page, int pagesize);
        PageResult<OrderDTO> GetSearchOrderCanceled(string text, int? page, int pagesize);
        PageResult<OrderDTO> SortPrice(string sortParam, int? page, int pagesize);
        PageResult<OrderDTO> SortDate(string sortParam, int? page, int pagesize);
        PageResult<OrderDTO> SortPriceOrderCompleted(string sortParam, int? page, int pagesize);
        PageResult<OrderDTO> SortDateOrderCompleted(string sortParam, int? page, int pagesize);
        OrderDTO GetEditOrder(int id);
    }
}
