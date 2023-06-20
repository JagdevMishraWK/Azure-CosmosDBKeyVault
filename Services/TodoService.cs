using Microsoft.Azure.Cosmos;
using ShoppingWeb.Models;

namespace ShoppingWeb.Services
{
    public class TodoService : ITodoService
    {
        private readonly Container _container;
        public TodoService(CosmosClient cosmosClient,
        string databaseName,
        string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<TodoItemsModel> AddAsync(TodoItemsModel newTodo)
        {
            var item = await _container.CreateItemAsync<TodoItemsModel>(newTodo, new PartitionKey(newTodo.Id));
            return item;
        }

        public async Task<List<TodoItemsModel>> Get(string sqlCosmosQuery)
        {
            var query = _container.GetItemQueryIterator<TodoItemsModel>(new QueryDefinition(sqlCosmosQuery));

            List<TodoItemsModel> result = new List<TodoItemsModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }

            return result;
        }

        public async Task<TodoItemsModel> Update(TodoItemsModel todoToUpdate)
        {
            var item = await _container.UpsertItemAsync<TodoItemsModel>(todoToUpdate, new PartitionKey(todoToUpdate.Id));
            return item;
        }
        public async Task Delete(string id)
        {
            await _container.DeleteItemAsync<TodoItemsModel>(id, new PartitionKey(id));
        }
    }
}
