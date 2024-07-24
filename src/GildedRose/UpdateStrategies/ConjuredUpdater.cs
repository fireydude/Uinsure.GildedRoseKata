using GildedRoseKata;

namespace GildedRose.UpdateStrategies
{
    internal class ConjuredUpdater : IUpdater
    {
        public void Update(Item item)
        {
            item.SellIn--;
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 2;
                if (item.SellIn < 0 && item.Quality > 0)
                {
                    item.Quality = item.Quality - 2;
                }
            }
        }
    }
}