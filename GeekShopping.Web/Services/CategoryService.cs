using System;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
	public class CategoryService : ICategoryService
	{
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/category";

        public CategoryService(HttpClient client)
		{
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CategoryViewModel>> FindAllCategories()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<CategoryViewModel>>();
        }

        public async Task<CategoryViewModel> FindCategoryById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<CategoryViewModel>();
        }
        public async Task<CategoryViewModel> CreateCategory(CategoryViewModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CategoryViewModel>();

            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<CategoryViewModel> UpdateCategory(CategoryViewModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CategoryViewModel>();

            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<bool> DeleteCategoryById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong when calling API");
        }

       

       
    }
}

