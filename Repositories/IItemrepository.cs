
using Catalog.Entities;

namespace Catalog.Repositories
{

// this is our contract that we are following, and so it provides the method signatures that
// are must for the class that relies on this interface

public interface IInMemItemsRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);

    }



}
    