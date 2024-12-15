using System.Text.Json;

namespace Infrastructure.Persistence
{
    public class JsonDBContext
    {
        private readonly string _dataFilePath;

        public JsonDBContext(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        // Carregar todos os itens do arquivo JSON
        public async Task<List<T>> GetAllAsync<T>()
        {
            if (!File.Exists(_dataFilePath))
                return new List<T>();

            var jsonData = await File.ReadAllTextAsync(_dataFilePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }

        // Adicionar ou atualizar um item no arquivo JSON
        public async Task SaveAsync<T>(List<T> data)
        {
            var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_dataFilePath, jsonData);
        }

        // Adicionar um item ao JSON
        public async Task AddAsync<T>(T item)
        {
            var data = await GetAllAsync<T>();
            data.Add(item);
            await SaveAsync(data);
        }

        // Atualizar um item no JSON
        public async Task UpdateAsync<T>(Guid id, T updatedItem, Func<T, Guid> getId)
        {
            var data = await GetAllAsync<T>();
            var itemIndex = data.FindIndex(item => getId(item) == id);

            if (itemIndex != -1)
            {
                data[itemIndex] = updatedItem;
                await SaveAsync(data);
            }
        }

        // Remover um item do JSON
        public async Task DeleteAsync<T>(Guid id, Func<T, Guid> getId)
        {
            var data = await GetAllAsync<T>();
            var itemToRemove = data.FirstOrDefault(item => getId(item) == id);

            if (itemToRemove != null)
            {
                data.Remove(itemToRemove);
                await SaveAsync(data);
            }
        }
    }
}

