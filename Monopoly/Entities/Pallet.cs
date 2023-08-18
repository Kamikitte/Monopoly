namespace Monopoly.Entities
{
    public class Pallet : InventoryObject
    {
        public Pallet(int id, double width, double length, double height) : base(id, width, height, length, 30)
        {
            Boxes = new List<Box>();
        }

        public List<Box> Boxes { get; set; }

        public override double Weight 
        { 
            get => base.Weight + Boxes.Sum(x => x.Weight);
        }

        public override double Volume
        {
            get => base.Volume + Boxes.Sum(x => x.Volume);
        }

        public DateOnly? ExpirationDate 
        {
            get => Boxes.Any() ? Boxes.Min(x => x.ExpirationDate) : null;
        }

        public void AddBox(Box box)
        {
            if (box.Length > this.Length || box.Width > this.Width)
            {
                throw new ArgumentException("Коробка слишком большая для паллеты");
            }
            Boxes.Add(box);
        }
    }
}
