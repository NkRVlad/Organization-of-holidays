using BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.AdminService
{
    public interface IAdminService
    {
        bool CheckAdmin(AdminDTO adminDTO);
    }
}
