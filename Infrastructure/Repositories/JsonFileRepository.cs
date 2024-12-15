using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class JsonFileRepository<T> where T : class
    {
        private readonly string _filePath;

        public JsonFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        protected List<T> ReadData()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }

        protected void WriteData(List<T> data)
        {
            var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
