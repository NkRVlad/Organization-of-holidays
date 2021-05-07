using AutoMapper;
using BAL.ModelsDTO;
using DAL;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.StatusService
{
    public class StatusService : ISatusService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public StatusService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PageResult<StatusDTO> GetStatus(int? page, int pagesize)
        {
            var resultTemp = _dbContext.Statuses.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

            var resultList = _mapper.Map<List<StatusDTO>>(resultTemp);

            var result = new PageResult<StatusDTO>
            {
                Count = _dbContext.Statuses.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList
            };

            return result;
        }

        public bool DeleteStatus(int id)
        {
            if (_dbContext.Statuses.FirstOrDefault(i => i.Id == id) != null)
            {
                var tempStatus =_dbContext.Statuses.FirstOrDefault(i => i.Id == id);
                _dbContext.Statuses.Remove(tempStatus);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddStatus(StatusDTO statusDTO)
        {
            var result = _mapper.Map<Status>(statusDTO);
            _dbContext.Statuses.Add(result);
            _dbContext.SaveChanges();
        }
    }
}
