using ShoppingWeb.Models;

namespace ShoppingWeb.Services
{
    public interface ITodoService
    {
        Task<List<TodoItemsModel>> Get(string sqlCosmosQuery);
        Task<TodoItemsModel> AddAsync(TodoItemsModel newTodo);
        Task<TodoItemsModel> Update(TodoItemsModel todoToUpdate);

        Task Delete(string id);
    }
}
