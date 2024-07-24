namespace GildedRose.UpdateStrategies
{
    internal static class UpdateFactory
    {
        public static IUpdater GetUpdater(string name)
        {
            return name switch
            {
                "Aged Brie" => new AgedBrieUpdater(),
                "Sulfuras, Hand of Ragnaros" => new SulfurasUpdater(),
                "Backstage passes to a TAFKAL80ETC concert" => new BackstagePassesUpdater(),
                "Conjured Mana Cake" => new ConjuredUpdater(),
                _ => new StandardUpdater(),
            };
        }
    }
}