using Monopoly;
using Monopoly.Entities;

namespace MonopolyConsole
{
    internal class Program
    {
        private static List<Pallet> _pallets;
        private static Dictionary<string, Action> _actions = new Dictionary<string, Action>()
        {
            {"new pallet", CreatePallet},
            {"new box", CreateBox},
            {"random", RandomPallets},
            {"load", LoadFromFile},
            {"save", SaveToFile},
            {"max expiration", MaxExpiration},
            {"sorted groups", SortedGroups},
            {"help", Help }
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Введите \"help\", чтобы просмотреть список команд.");
            while(true)
            {
                var command = Console.ReadLine();
                if (_actions.ContainsKey(command))
                {
                    _actions[command].Invoke();
                }
            }
        }

        private static void CreatePallet()
        {
            Console.WriteLine("Введите через пробел id, ширину, длину и высоту паллета");
            var parameters = Console.ReadLine().Split(' ');
            
            var id = int.Parse(parameters[0]);
            var width = double.Parse(parameters[1]);
            var length = double.Parse(parameters[2]);
            var height = double.Parse(parameters[3]);
            
            _pallets.Add(new Pallet(id, width, length, height));

            Console.WriteLine("Паллет успешно создан");
        }
        private static void CreateBox()
        {
            Console.WriteLine("Введите через пробел id коробки, id паллета, ширину, высоту, длину, вес коробки, \n" +
                "дату её производства, а также (опционально) дату истечения срока годности в формате dd.mm.yyyy.");
            var parameters = Console.ReadLine().Split(' ');

            var boxId = int.Parse(parameters[0]);
            var palletId = int.Parse(parameters[1]);
            var width = double.Parse(parameters[2]);
            var height = double.Parse(parameters[3]);
            var length = double.Parse(parameters[4]);
            var weight = double.Parse(parameters[5]);
            var productionDate = DateOnly.Parse(parameters[6]);

            Box box;
            if (parameters.Length == 7)
            {
                box = new Box(boxId, width, height, length, weight, productionDate);
            }
            else
            {
                var expirationDay = DateOnly.Parse(parameters[7]);
                box = new Box(boxId, width, height, length, weight, productionDate, expirationDay);
            }
            var pallet = _pallets.Find(x => x.Id == palletId);
            if (pallet == null)
            {
                Console.WriteLine("Паллет не найден.");
                return;
            }

            try
            {
                pallet.AddBox(box);
                Console.WriteLine("Коробка успешно создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        private static void RandomPallets()
        {
            Console.WriteLine("Введите количество паллет.");

            var count = int.Parse(Console.ReadLine());
            _pallets = PalletService.GeneratePallets(count);
            Console.WriteLine("Паллеты созданы.");
        }
        private static async void LoadFromFile()
        {
            Console.WriteLine("Введите путь к файлу.");
            var path = Console.ReadLine();
            try
            {
                var result = await PalletSerializer.DeserializeFromFile(path);
                _pallets = result.ToList();
                Console.WriteLine("Файл загружен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        private static async void SaveToFile()
        {
            Console.WriteLine("Введите путь сохранения.");
            var path = Console.ReadLine();
            await PalletSerializer.SerializeToFile(_pallets, path);
            Console.WriteLine("Файл сохранён");
        }
        private static void MaxExpiration()
        {
            var list = PalletService.GetThreeWithMaxExpirationDate(_pallets);
            foreach (var item in list)
            {
                Console.WriteLine($"Паллета {item.Id}: Срок годности: {item.Boxes.Max(x => x.ExpirationDate)}, Объём: {item.Volume}");
            }
        }
        private static void SortedGroups()
        {
            var groups = PalletService.GroupAndSortPallets(_pallets);
            foreach (var group in groups)
            {
                Console.WriteLine($"Срок годности: {group[0].ExpirationDate}");
                foreach(var pallet in group)
                {
                    Console.WriteLine($"Паллета {pallet.Id}: Вес {pallet.Weight}");
                }
            }
        }
        private static void Help()
        {
            Console.WriteLine("new pallet: Создать новую паллету");
            Console.WriteLine("new box: Создать новую коробку и добавить в паллету");
            Console.WriteLine("random: Создать случайный набор паллет с коробками");
            Console.WriteLine("load: Загрузить список паллет из файла");
            Console.WriteLine("save: Сохранить список паллет в файл");
            Console.WriteLine("max expiration: Получить 3 паллеты с наибольшим сроком годности");
            Console.WriteLine("sorted groups: Сгруппировать паллеты по сроку годности, отсортировать по весу.");
        }
    }
}