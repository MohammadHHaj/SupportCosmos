using Microsoft.Azure.Cosmos;
using SupportCosmos.Shared.Models;

namespace SupportCosmos.Server.Services
{
    public class CosmosService
    {
        private readonly Container _container;

        public CosmosService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        // ✅ Tilføj ny besked
        public async Task AddItemAsync(SupportMessage message)
        {
            // Hent eksisterende items for at finde det højeste nummer
            var query = _container.GetItemQueryIterator<SupportMessage>(
                new QueryDefinition("SELECT c.id FROM c WHERE STARTSWITH(c.id, 'support-') ORDER BY c._ts DESC"));

            string nextId = "support-0001";

            if (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();
                if (results.Any())
                {
                    var last = results
                        .Select(x => x.Id.Replace("support-", ""))
                        .Where(x => int.TryParse(x, out _))
                        .Select(int.Parse)
                        .DefaultIfEmpty(0)
                        .Max();

                    nextId = $"support-{(last + 1):D4}";
                }
            }

            // Sørg for, at kategori aldrig er null
            message.Category = string.IsNullOrWhiteSpace(message.Category)
                ? "Ukendt"
                : message.Category.Trim();

            // Tildel ID og tidspunkt
            message.Id = nextId;
            message.CreatedUtc = DateTime.UtcNow;

            await _container.CreateItemAsync(message, new PartitionKey(message.Category));
        }


        // ✅ Hent alle beskeder
        public async Task<List<SupportMessage>> GetAllItemsAsync()
        {
            var query = _container.GetItemQueryIterator<SupportMessage>(
                new QueryDefinition("SELECT * FROM c"));

            var results = new List<SupportMessage>();

            while (query.HasMoreResults)
            {
                FeedResponse<SupportMessage> response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }
    }
}