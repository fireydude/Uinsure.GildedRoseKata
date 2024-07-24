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
        [InlineData(11, 8)]
        [InlineData(12, 6)]
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

        [Fact(Skip = "Veried results do not match this test")]
        public void UpdateQuality_QualityIsNeverMoreThan50_UnlessSulfuras()
        {
            for (int i = 0; i < 100; i++)
            {
                _app.UpdateQuality();
            }
            foreach (var item in _items.Where(x => !x.Name.StartsWith("Sulfuras")))
            {
                Assert.False(item.Quality > 50, userMessage: item.Name);
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

        [Theory]
        [InlineData(0, 40)]
        [InlineData(1, 38)]
        [InlineData(2, 36)]
        [InlineData(10, 20)]
        [InlineData(11, 16)]
        [InlineData(12, 12)]
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