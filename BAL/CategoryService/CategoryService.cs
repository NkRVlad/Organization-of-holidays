using AutoMapper;
using BAL.ModelsDTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entity;
namespace BAL.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public PageResult<CategoryDTO> GetCategory(int? page, int pagesize)
        {
            var resultTemp = _dbContext.Categories.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList();

            var resultList = _mapper.Map<List<CategoryDTO>>(resultTemp);

            var result = new PageResult<CategoryDTO>
            {
                Count = _dbContext.Categories.Count(),
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = resultList
            };

            return result;
        }
        public bool DeleteCategory(int id)
        {
            if (_dbContext.Categories.FirstOrDefault(i => i.Id == id) != null)
            {
                var tempCategory =_dbContext.Categories.FirstOrDefault(i => i.Id == id);
                _dbContext.Categories.Remove(tempCategory);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddCategory(CategoryDTO categoryDTO)
        {
             var result = _mapper.Map<Category>(categoryDTO);
            _dbContext.Categories.Add(result);
            _dbContext.SaveChanges();
        }
    }
}
