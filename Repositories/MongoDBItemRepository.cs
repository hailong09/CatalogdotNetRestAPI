using System;
using System.Linq;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Catalog.Repositories
{
    class MongoDBItemRepository : IItemsRepository
    {

        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterDefinitionBuilder = Builders<Item>.Filter;

        public MongoDBItemRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);

        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {

            return await itemsCollection.Find(new BsonDocument()).ToListAsync();


        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterDefinitionBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);


        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(existingItem => existingItem.Id, id);
            await itemsCollection.DeleteOneAsync(filter);

        }

    }
}
