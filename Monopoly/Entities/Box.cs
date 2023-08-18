namespace Monopoly.Entities
{
    public class Box : InventoryObject
    {
        public Box(int id, double width, double height, double length, double weight, DateOnly productionDate) 
            : base(id, width, height, length, weight)
        {
            ProductionDate = productionDate;
            ExpirationDate = productionDate.AddDays(100);
        }

        public Box() : base() { }

        public Box(int id, double width, double height, double length, double weight, DateOnly? productionDate, DateOnly expirationDate) 
            : base(id, width, height, length, weight)
        {
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }

        public DateOnly? ProductionDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }
}
