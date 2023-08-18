namespace Monopoly.Entities
{
    public class InventoryObject
    {
        public InventoryObject(int id, double width, double height, double length, double weight)
        {
            Id = id;
            Width = width;
            Height = height;
            Weight = weight;
            Length = length;
        }

        public InventoryObject() { }

        public int Id { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public virtual double Weight { get; set; }

        public virtual double Volume
        {
            get => Width * Height * Length;
        }
    }
}