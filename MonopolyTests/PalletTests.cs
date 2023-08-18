using Monopoly.Entities;
using NUnit.Framework;

namespace MonopolyTests
{
    [TestFixture]
    public class PalletTests
    {
        [Test]
        public void VolumeOfBoxIsCorrect()
        {
            var box = new Box(1, 10, 30, 5, 40, new(2023, 08, 18));
            Assert.AreEqual(box.Volume, 10 * 30 * 5);
        }

        [Test]
        public void VolumeOfPalleteIsCorrect()
        {
            var box = new Box(2, 2, 2, 2, 40, new(2023, 08, 18));
            var box2 = new Box(3, 3, 3, 3, 40, new(2023, 08, 18));
            var pallet = new Pallet(1, 20, 10, 10);
            pallet.AddBox(box);
            pallet.AddBox(box2);
            Assert.AreEqual(pallet.Volume, (20 * 10 * 10) + (2 * 2 * 2) + (3 * 3 * 3));
        }

        [Test]
        public void CanAddBoxWhenMeasuresAreEqual()
        {
            var box = new Box(1, 10, 10, 10, 40, new(2023, 08, 18));
            var pallet = new Pallet(1, 10, 10, 10);
            pallet.AddBox(box);
            Assert.Pass();
        }

        [Test]
        public void CantAddBoxWhenPalletAreSmaller()
        {
            var box = new Box(1, 10, 10, 20, 40, new(2023, 08, 18));
            var pallet = new Pallet(1, 3, 3, 3);
            Assert.Throws<ArgumentException>(() =>
            {
                pallet.AddBox(box);
            });
        }

        [Test]
        public void SumWeightIsCorrect()
        {
            var box = new Box(1, 10, 10, 20, 40, new(2023, 08, 18));
            var box2 = new Box(1, 10, 10, 20, 10, new(2023, 08, 18));
            var pallet = new Pallet(1, 50, 50, 50);
            pallet.AddBox(box);
            pallet.AddBox(box2);
            Assert.AreEqual(pallet.Weight, 40 + 10 + 30);
        }
    }
}