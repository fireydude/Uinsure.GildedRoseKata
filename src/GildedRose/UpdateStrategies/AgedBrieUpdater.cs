using GildedRoseKata;

namespace GildedRose.UpdateStrategies
{
    internal class AgedBrieUpdater : IUpdater
    {
        public void Update(Item item)
        {
            item.SellIn = item.SellIn - 1;
            if (item.Quality < 50)
            {
                item.Quality++;
                if (item.Quality < 50 && item.SellIn < 0)
                {
                    item.Quality++;
                }
            }
        }
    }
}