using Monopoly.Entities;
using Newtonsoft.Json;

namespace Monopoly
{
    public class PalletSerializer
    {
        public static async Task SerializeToFile(IEnumerable<Pallet> pallets, string path)
        {
            string json = JsonConvert.SerializeObject(pallets);
            await File.WriteAllTextAsync(path, json);
        }

        public static async Task<IEnumerable<Pallet>> DeserializeFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Файл не найден");
            }

            string json = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<IEnumerable<Pallet>>(json);
        }
    }
}
