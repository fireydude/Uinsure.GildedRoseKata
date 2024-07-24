using GildedRoseKata;

namespace GildedRose.UpdateStrategies
{
    internal class BackstagePassesUpdater : IUpdater
    {
        public void Update(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }
            if (item.Quality < 50)
            {
                item.Quality++;
            }
            if (item.Quality < 50 && item.SellIn < 10)
            {
                item.Quality++;
            }
            if (item.Quality < 50 && item.SellIn < 5)
            {
                item.Quality++;
            }
        }
    }
}