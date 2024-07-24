using GildedRoseKata;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GildedRoseTests
{
    [Trait("Category", "UnitTest")]
    public class GildedRoseTest
    {
        private GildedRoseKata.GildedRose _app => new GildedRoseKata.GildedRose(_items);
        private IList<Item> _items;

        public GildedRoseTest()
        {
            _items = new List<Item>
            {
                new Item { Name = "standard", SellIn = 10, Quality = 20 },
                new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 40 },
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

        [Theory]
        [InlineData(0, 20)]
        [InlineData(1, 19)]
        [InlineData(2, 18)]
        [InlineData(10, 10)]
        [InlineData(11, 8)] // degrades twice as fast after sell by date
        [InlineData(12, 6)]
        [InlineData(100, 0)] // quality cannot be negative
        public void UpdateQuality_AltersStandardItem(int days, int expectedQuality)
        {
            var standardItem = _items[0];
            var initialSellIn = standardItem.SellIn;
            Assert.Equal("standard", standardItem.Name);

            for (var i = 0; i < days; i++)
            {
                _app.UpdateQuality();
            }

            Assert.Equal(initialSellIn - days, standardItem.SellIn);
            Assert.Equal(expectedQuality, standardItem.Quality);
        }

        [Fact]
        public void UpdateQuality_QualityIsNeverMoreThan50OrNegative_UnlessSulfuras()
        {
            for (int i = 0; i < 100; i++)
            {
                _app.UpdateQuality();
            }
            foreach (var item in _items.Where(x => !x.Name.StartsWith("Sulfuras")))
            {
                Assert.False(item.Quality > 50, userMessage: $"{item.Name} is more than 50");
                Assert.False(item.Quality < 0, userMessage: $"{item.Name} is less than 0");
            }
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 6)]
        [InlineData(100, 50)] // max quality is 50
        public void UpdateQuality_AltersBrieItem(int days, int expectedQuality)
        {
            var brieItem = _items[2];
            var initialSellIn = brieItem.SellIn;

            for (var i = 0; i < days; i++)
            {
                _app.UpdateQuality();
            }

            Assert.Equal("Aged Brie", brieItem.Name);
            Assert.Equal(initialSellIn - days, brieItem.SellIn);
            Assert.Equal(expectedQuality, brieItem.Quality);
        }

        [Theory]
        [InlineData(0, 80)] // legendary item always has quality of 80 and remaining days do not change
        [InlineData(10, 80)]
        [InlineData(100, 80)]
        public void UpdateQuality_AltersSulfurasItem(int days, int expectedQuality)
        {
            var sulfurasItem = _items[3];
            var initialSellIn = sulfurasItem.SellIn;

            for (var i = 0; i < days; i++)
            {
                _app.UpdateQuality();
            }

            Assert.Equal("Sulfuras, Hand of Ragnaros", sulfurasItem.Name);
            Assert.Equal(initialSellIn, sulfurasItem.SellIn);
            Assert.Equal(expectedQuality, sulfurasItem.Quality);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 6)] // increases by 1 when more than 10 days remaining
        [InlineData(5, 10)]
        [InlineData(6, 12)] // increases by 2 when 10 days remaining
        [InlineData(9, 18)]
        [InlineData(10, 20)]
        [InlineData(11, 23)] // increases by 3 when 5 days remaining
        [InlineData(12, 26)]
        [InlineData(16, 0)] // no value after concert
        [InlineData(100, 0)] // quality cannot be negative
        public void UpdateQuality_AltersBackstagePass(int days, int expectedQuality)
        {
            var backstageItem = _items[4];
            var initialSellIn = backstageItem.SellIn;
            var quality = backstageItem.Quality;
            for (var i = 0; i < days; i++)
            {
                _app.UpdateQuality();
            }
            Assert.Equal(initialSellIn - days, backstageItem.SellIn);
            Assert.Equal(expectedQuality, backstageItem.Quality);
        }

        [Theory]
        [InlineData(0, 40)]
        [InlineData(1, 38)]
        [InlineData(2, 36)]
        [InlineData(10, 20)]
        [InlineData(11, 16)]
        [InlineData(12, 12)]
        [InlineData(100, 0)] // quality cannot be negative
        public void UpdateQuality_AltersConjuredItem(int days, int expectedQuality)
        {
            var conjuredItem = _items[1];
            var initialSellIn = conjuredItem.SellIn;
            Assert.Equal("Conjured Mana Cake", conjuredItem.Name);

            for (var i = 0; i < days; i++)
            {
                _app.UpdateQuality();
            }

            Assert.Equal(initialSellIn - days, conjuredItem.SellIn);
            Assert.Equal(expectedQuality, conjuredItem.Quality);
        }
    }
}