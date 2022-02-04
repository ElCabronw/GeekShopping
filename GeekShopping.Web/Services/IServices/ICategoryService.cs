using System;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryModel>> FindAllCategories();
		Task<CategoryModel> FindCategoryById(long id);
		Task<CategoryModel> CreateCategory(CategoryModel model);
		Task<CategoryModel> UpdateCategory(CategoryModel model);
		Task<bool> DeleteCategoryById(long id);
	}
}

