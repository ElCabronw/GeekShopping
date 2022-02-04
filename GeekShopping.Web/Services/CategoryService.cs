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

        public async Task<IEnumerable<CategoryModel>> FindAllCategories()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<CategoryModel>>();
        }

        public async Task<CategoryModel> FindCategoryById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<CategoryModel>();
        }
        public async Task<CategoryModel> CreateCategory(CategoryModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CategoryModel>();

            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<CategoryModel> UpdateCategory(CategoryModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<CategoryModel>();

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

