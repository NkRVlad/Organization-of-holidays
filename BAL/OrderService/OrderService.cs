using AutoMapper;
using BAL.ModelsDTO;
using DAL;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddOrder(OrderDTO orderDTO)
        {
            if (_dbContext.Orders.FirstOrDefault(d => d.Date == orderDTO.Date) == null)
            {
                var resultOrder = _mapper.Map<Order>(orderDTO);
                _dbContext.Orders.Add(resultOrder);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CanceledOrder(int id)
        {
            Order tempOrder = _dbContext.Orders.FirstOrDefault(i => i.Id == id);
            Status status = _dbContext.Statuses.FirstOrDefault(n => n.Name == "Отменён");
            tempOrder.StatusId = status.Id;
            _dbContext.SaveChanges();
        }

        public void CompletedOrder(int id)
        {
            Order tempOrder = _dbContext.Orders.FirstOrDefault(i => i.Id == id);
            Status status = _dbContext.Statuses.FirstOrDefault(n => n.Name == "Выполнен");
            tempOrder.StatusId = status.Id;
            _dbContext.SaveChanges();
        }

        public void EditStatusOrder(int id)
        {
            Order tempOrder = _dbContext.Orders.FirstOrDefault(i => i.Id == id);
            Status status = _dbContext.Statuses.FirstOrDefault(n => n.Name == "В обработке");
            tempOrder.StatusId = status.Id;
            _dbContext.SaveChanges();
        }

        public PageResult<OrderDTO> GetOrderCanceled(int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "Отменён")
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderByDescending(d => d.Date)
                .ToList();

            var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

            var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

            var result = new PageResult<OrderDTO>
            {
                Count = temp.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList
            };

            return result;
        }
        

        public PageResult<OrderDTO> GetOrderCompleted(int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен")
               .Include(s => s.Status)
               .Include(c => c.Category)
               .OrderByDescending(d => d.Date)
               .ToList();

            var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

            var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

            var result = new PageResult<OrderDTO>
            {
                Count = temp.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList
            };

            return result;
        }

        public PageResult<OrderDTO> GetOrderNew(int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "Новый")
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();

            var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

            var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

            var result = new PageResult<OrderDTO>
            {
                Count = temp.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList
            };

            return result;
        }

        public PageResult<OrderDTO> GetOrderPending(int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "В обработке")
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();

            var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

            var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

            var result = new PageResult<OrderDTO>
            {
                Count = temp.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList
            };

            return result;
        }

        public PageResult<OrderDTO> GetSearch(string text, int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "В обработке" && s.Name.Contains(text))
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
            
            if(temp.Count() == 0)
            {
                var tempPhone = _dbContext.Orders.Where(s => s.Status.Name == "В обработке" && s.Phone.Contains(text))
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
                
                var resultTemp = tempPhone.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = tempPhone.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }

        public PageResult<OrderDTO> GetSearchOrderCompleted(string text, int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен" && s.Name.Contains(text))
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
            
            if(temp.Count() == 0)
            {
                var tempPhone = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен" && s.Phone.Contains(text))
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
                
                var resultTemp = tempPhone.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = tempPhone.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }

        public PageResult<OrderDTO> GetSearchOrderCanceled(string text, int? page, int pagesize)
        {
            var temp = _dbContext.Orders.Where(s => s.Status.Name == "Отменён" && s.Name.Contains(text))
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
            
            if(temp.Count() == 0)
            {
                var tempPhone = _dbContext.Orders.Where(s => s.Status.Name == "Отменён" && s.Phone.Contains(text))
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
                
                var resultTemp = tempPhone.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = tempPhone.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }
        public PageResult<OrderDTO> GetSearchDate(DateTime dateSearch, int? page, int pagesize)
        {
             var tempPhone = _dbContext.Orders.Where(s => s.Status.Name == "В обработке" && s.Date == dateSearch)
                .Include(s => s.Status)
                .Include(c => c.Category)
                .OrderBy(d => d.Date)
                .ToList();
                
                var resultTemp = tempPhone.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = tempPhone.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
        }
        public PageResult<OrderDTO> SortPrice(string sortParam, int? page, int pagesize)
        {
            if (sortParam == "asc")
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "В обработке")
                                .Include(s => s.Status)
                                .Include(c => c.Category)
                                .OrderBy(d => d.Duration * d.Category.Price)
                                .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "В обработке")
                               .Include(s => s.Status)
                               .Include(c => c.Category)
                               .OrderByDescending(d => d.Duration * d.Category.Price)
                               .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }

        public PageResult<OrderDTO> SortPriceOrderCompleted(string sortParam, int? page, int pagesize)
        {
            if (sortParam == "asc")
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен")
                                .Include(s => s.Status)
                                .Include(c => c.Category)
                                .OrderBy(d => d.Duration * d.Category.Price)
                                .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен")
                               .Include(s => s.Status)
                               .Include(c => c.Category)
                               .OrderByDescending(d => d.Duration * d.Category.Price)
                               .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }
        public PageResult<OrderDTO> SortDate(string sortParam, int? page, int pagesize)
        {
            if (sortParam == "asc")
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "В обработке")
                                .Include(s => s.Status)
                                .Include(c => c.Category)
                                .OrderBy(d => d.Date)
                                .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "В обработке")
                               .Include(s => s.Status)
                               .Include(c => c.Category)
                               .OrderByDescending(d => d.Date)
                               .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }
        public PageResult<OrderDTO> SortDateOrderCompleted(string sortParam, int? page, int pagesize)
        {
             if (sortParam == "asc")
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен")
                                .Include(s => s.Status)
                                .Include(c => c.Category)
                                .OrderBy(d => d.Date)
                                .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
            else
            {
                var temp = _dbContext.Orders.Where(s => s.Status.Name == "Выполнен")
                               .Include(s => s.Status)
                               .Include(c => c.Category)
                               .OrderByDescending(d => d.Date)
                               .ToList();

                var resultTemp = temp.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

                var resultList = _mapper.Map<List<OrderDTO>>(resultTemp);

                var result = new PageResult<OrderDTO>
                {
                    Count = temp.Count(),
                    PageIndex = page ?? 1,
                    PageSize = pagesize,
                    Items = resultList
                };

                return result;
            }
        }
        public OrderDTO GetEditOrder(int id)
        {
            var tempResult = _dbContext.Orders.FirstOrDefault(i => i.Id == id);
            var result = _mapper.Map<OrderDTO>(tempResult);
            return result;
        }
        public bool EditOrder(OrderDTO order)
        { 
            if(_dbContext.Orders.FirstOrDefault(d => d.Date == order.Date && d.Id == order.Id) != null )
            {
                Order temp = _mapper.Map<Order>(order);
                Order result = _dbContext.Orders.FirstOrDefault(i => i.Id == order.Id);
                result.Duration = temp.Duration;
                _dbContext.SaveChanges();
                return true;
            }
            else if(_dbContext.Orders.FirstOrDefault(d => d.Date == order.Date && d.Id != order.Id) != null)
            {
                return false;
            }
            else
            {
                 Order temp = _mapper.Map<Order>(order);
                _dbContext.Orders.Update(temp);
                _dbContext.SaveChanges();
                return true;
            }

        }

    }
}
