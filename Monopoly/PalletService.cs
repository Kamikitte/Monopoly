using Monopoly.Entities;

namespace Monopoly
{
    public static class PalletService
    {
        public static List<Pallet> GetThreeWithMaxExpirationDate(List<Pallet> pallets)
        {
            return pallets
                .Where(p => p.Boxes.Count > 0)
                .OrderByDescending(p => p.Boxes.Max(b => b.ExpirationDate))
                .Take(3)
                .OrderByDescending(p => p.Volume)
                .ToList();
        }

        public static List<List<Pallet>> GroupAndSortPallets(List<Pallet> pallets)
        {
            var groupedPallets = pallets
                .GroupBy(p => p.ExpirationDate)
                .OrderBy(g => g.Key);

            var sortedGroups = groupedPallets
                .Select(g => g.OrderBy(p => p.Weight).ToList())
                .ToList();

            return sortedGroups;
        }

        public static List<Pallet> GeneratePallets(int palletsCount)
        {
            Random random = new Random();
            List<Pallet> pallets = new List<Pallet>();

            for (int i = 0; i < palletsCount; i++)
            {
                var palletLength = random.Next(10, 30);
                var palletWidth = random.Next(10, 30);
                var palletHeight = random.Next(1, 5);

                Pallet pallet = new Pallet(i, palletWidth, palletHeight, palletLength);

                int boxCount = random.Next(1, 5); 

                for (int j = 0; j < boxCount; j++)
                {
                    var id = random.Next(palletsCount + 1, palletsCount + 10000);
                    var boxWidth = random.Next(1, palletWidth);
                    var boxLength = random.Next(1, palletLength);
                    var boxHeight = random.Next(1, 50);
                    var boxWeight = random.Next(1, 100);
                    var productionDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(1, 100)));

                    Box box = new Box(id, boxWidth, boxHeight, boxLength, boxWeight, productionDate);

                    pallet.Boxes.Add(box);
                }

                pallets.Add(pallet);
            }

            return pallets;
        }
    }
}
