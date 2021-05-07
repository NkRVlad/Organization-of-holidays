using BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.StatusService
{
    public interface ISatusService
    {
        PageResult<StatusDTO> GetStatus(int? page, int pagesize);
        bool DeleteStatus(int id);
        void AddStatus(StatusDTO statusDTO);

    }
}
