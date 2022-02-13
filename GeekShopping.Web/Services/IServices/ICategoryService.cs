using System;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryViewModel>> FindAllCategories();
		Task<CategoryViewModel> FindCategoryById(long id);
		Task<CategoryViewModel> CreateCategory(CategoryViewModel model);
		Task<CategoryViewModel> UpdateCategory(CategoryViewModel model);
		Task<bool> DeleteCategoryById(long id);
	}
}

