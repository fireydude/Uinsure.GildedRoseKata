using GildedRoseKata;

namespace GildedRose.UpdateStrategies
{
    internal class SulfurasUpdater : IUpdater
    {
        public void Update(Item item)
        {
            item.Quality = 80;
        }
    }
}