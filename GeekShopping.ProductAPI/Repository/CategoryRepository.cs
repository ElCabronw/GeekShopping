using System;
using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		public CategoryRepository()
		{
		}
        private readonly MySQLContext _context;
        private IMapper _mapper;
        public CategoryRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryVO>> FindAll()
        {
            var products = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryVO>>(products);
        }
        public async Task<CategoryVO> FindById(long id)
        {
            Category category = await _context.Categories.FindAsync(id);
            return _mapper.Map<CategoryVO>(category);
        }
        public async Task<CategoryVO> Create(CategoryVO vo)
        {
            Category category = _mapper.Map<Category>(vo);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryVO>(category);

        }
        public async Task<CategoryVO> Update(CategoryVO vo)
        {
            Category category = _mapper.Map<Category>(vo);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryVO>(category);
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                Category category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return false;
                }
                _context.Categories.Remove(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}

