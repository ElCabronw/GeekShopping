﻿using System;
using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
	public class ProductRepository : IProductRepository
	{
        private readonly MySQLContext _context;
        private IMapper _mapper;    
		public ProductRepository(MySQLContext context, IMapper mapper)
		{
            _context = context;
            _mapper = mapper;
		}
        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            var products = await _context.Products.Include(x => x.Category).ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }
        public async Task<ProductVO> FindById(long id)
        {
            Product product = await _context.Products.Include(x => x.Category).Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO vo)
        {
            try
            {
                Product product = _mapper.Map<Product>(vo);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductVO>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

        }
        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product product = _mapper.Map<Product>(vo);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                Product product =
               await _context.Products.Where(p => p.Id == id)
                   .FirstOrDefaultAsync();
                if (product == null) return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

      

       

    
    }
}

