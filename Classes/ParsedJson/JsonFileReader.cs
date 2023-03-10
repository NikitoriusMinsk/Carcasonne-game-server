using System.Text.Json;

namespace Carcasonne_game_server.Classes.ParsedJson
{
    public static class JsonFileReader
    {
        public static async Task<T> ReadFileAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public static T ReadFileSync<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<T>(text);
        }
    }
}
