using Catalog.Controllers;
using Catalog.Entities;

namespace Catalog.Repositories
{
    // the repository implements the interface - and so it must provide 
    // concrete implementations of the methods mentioned in the interface
    public class InMemItemsRepository :  IInMemItemsRepository  
    {
        // init List of class Items - it maintains a list of private
        private readonly  List<Item> items = new()
        {
            new Item { Id=Guid.NewGuid(), Name = "Shoes", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id=Guid.NewGuid(), Name = "Purse", Price = 12, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id=Guid.NewGuid(), Name = "Bags", Price = 20, CreatedDate = DateTimeOffset.UtcNow }
        };
        
        public  async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await Task.FromResult(items.Where(item => item.Id == id).SingleOrDefault());
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }

    }
}