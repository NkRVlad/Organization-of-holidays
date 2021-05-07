using AutoMapper;
using BAL.ModelsDTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public AdminService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool CheckAdmin(AdminDTO adminDTO)
        {
            if(_dbContext.Admins.FirstOrDefault(l => l.Login == adminDTO.Login && l.Password == adminDTO.Password) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
