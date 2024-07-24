using GildedRoseKata;

namespace GildedRose.UpdateStrategies
{
    internal class StandardUpdater : IUpdater
    {
        public void Update(Item item)
        {
            item.SellIn--;
            if (item.Quality > 0)
            {
                item.Quality--;
                if (item.SellIn < 0 && item.Quality > 0)
                {
                    item.Quality--;
                }
            }
        }
    }
}