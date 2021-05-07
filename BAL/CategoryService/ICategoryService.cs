using BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.CategoryService
{
    public interface ICategoryService
    {
        PageResult<CategoryDTO> GetCategory(int? page, int pagesize);
        bool DeleteCategory(int id);
        void AddCategory(CategoryDTO categoryDTO);
    }
}
