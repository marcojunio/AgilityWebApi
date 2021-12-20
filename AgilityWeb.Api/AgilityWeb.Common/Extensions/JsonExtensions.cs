using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IO;

namespace AgilityWeb.Common.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions =
            new()
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            };


        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize<T>(obj, JsonOptions);
        }


        public static T FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }


        public static async Task<string> ToJsonAsync<T>(this T obj)
        {
            string ret;
            var streamManager = new RecyclableMemoryStreamManager();
            using (MemoryStream ms = streamManager.GetStream())
            {
                await JsonSerializer.SerializeAsync(ms, obj, typeof(T), JsonOptions);
                ms.Position = 0;

                using (TextReader sr = new StreamReader(ms, System.Text.Encoding.UTF8))
                {
                    ret = await sr.ReadToEndAsync();
                }
            }

            return ret;
        }


        public static async Task<T> FromJsonAsync<T>(this Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, JsonOptions);
        }
    }
}