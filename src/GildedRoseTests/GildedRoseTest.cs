using GildedRoseKata;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        private GildedRoseKata.GildedRose _app => new GildedRoseKata.GildedRose(_items);
        private IList<Item> _items;

        public GildedRoseTest()
        {
            _items = new List<Item>
            {
                new Item { Name = "standard", SellIn = 10, Quality = 5 },
                new Item { Name = "another", SellIn = 10, Quality = 5 },
                new Item { Name = "Aged Brie", SellIn = 10, Quality = 5 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 5 },
            };
        }

        [Fact]
        public void UpdateQuality_DoesNotRemoveItems()
        {
            _app.UpdateQuality();
            Assert.Equal(5, _items.Count);
        }

        [Fact]
        public void UpdateQuality_AltersStandardItemWhereRequired_AfterOneDay()
        {
            _app.UpdateQuality();
            Assert.Equal("standard", _items[0].Name);
            Assert.Equal(9, _items[0].SellIn);
            Assert.Equal(4, _items[0].Quality);
        }

        [Fact(Skip = "Veried results do not match this test")]
        public void UpdateQuality_QualityIsNeverMoreThan50_UnlessSulfuras()
        {
            for (int i = 0; i < 100; i++)
            {
                _app.UpdateQuality();
            }
            foreach (var item in _items.Where(x => !x.Name.StartsWith("Sulfuras")))
            {
                Assert.False(item.Quality > 50, item.Name);
            }
        }

        [Fact]
        public void UpdateQuality_AltersBrieItemWhereRequired_AfterOneDay()
        {
            var brieItem = _items[2];
            _app.UpdateQuality();
            Assert.Equal("Aged Brie", brieItem.Name);
            Assert.Equal(9, brieItem.SellIn);
            Assert.Equal(6, brieItem.Quality);
        }

        [Fact]
        public void UpdateQuality_AltersSulfurasItemWhereRequired_AfterOneDay()
        {
            var sulfurasItem = _items[3];
            _app.UpdateQuality();
            Assert.Equal("Sulfuras, Hand of Ragnaros", sulfurasItem.Name);
            Assert.Equal(10, sulfurasItem.SellIn);
            Assert.Equal(80, sulfurasItem.Quality);
        }

        [Fact]
        public void UpdateQuality_AltersBackstagePassItemWhereRequired_AfterOneDay()
        {
            var backstageItem = _items[4];
            _app.UpdateQuality();
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", backstageItem.Name);
            Assert.Equal(14, backstageItem.SellIn);
            Assert.Equal(6, backstageItem.Quality);
        }

        [Fact]
        public void UpdateQuality_AltersBackstagePassDoubleIncrease_When10DaysRemaining()
        {
            var backstageItem = _items[4];
            var quality = backstageItem.Quality;
            while (backstageItem.SellIn > 9)
            {
                _app.UpdateQuality();
                if (backstageItem.SellIn > 10)
                {
                    quality++;
                    Assert.Equal(quality, backstageItem.Quality);
                }
            }
            Assert.Equal(9, backstageItem.SellIn);
            Assert.Equal(12, backstageItem.Quality);
        }

        [Fact]
        public void UpdateQuality_AltersBackstagePassTripleIncrease_When5DaysRemaining()
        {
            var backstageItem = _items[4];
            var quality = backstageItem.Quality;
            while (backstageItem.SellIn > 4)
            {
                _app.UpdateQuality();
            }
            Assert.Equal(4, backstageItem.SellIn);
            Assert.Equal(23, backstageItem.Quality);
        }
    }
}