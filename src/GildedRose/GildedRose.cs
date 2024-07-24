using GildedRose.UpdateStrategies;
using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private IList<Item> _items;

        public GildedRose(IList<Item> Items)
        {
            _items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                var updater = UpdateFactory.GetUpdater(item.Name);
                updater.Update(item);
            }
        }
    }
}