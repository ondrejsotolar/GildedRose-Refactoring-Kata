using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        IList<Item> _items1 = new List<Item>{   
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            },
            // this conjured item does not work properly yet
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        IList<Item> _items2 = new List<Item>{
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            },
            // this conjured item does not work properly yet
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        [TestCase("", 0, 51)]
        [TestCase("", 0, -1)]
        public void UpdateQualityThrowExceptionOnInvalidData(string name, int sellIn, int quality)
        {
            var items = new List<Item> {new Item {Name = name, SellIn = sellIn, Quality = quality}};
            var gildedRose = new GildedRose(items);

            gildedRose.UpdateQuality();

            Assert.DoesNotThrow(() => gildedRose.UpdateQuality());
        }

        [TestCase("", 0, 0)]
        public void DecreaseQualityAndSellInByOne_SellinOK_PASS(string name, int sellIn, int quality)
        {
            
        }

        [TestCase("", 0, 0)]
        public void DecreaseQualityAndSellInByTwo_SellinNOK_Pass(string name, int sellIn, int quality)
        {

        }

        [TestCase("", 0, 0)]
        public void DecreaseQualityAndSellInByOne_SellinNOK_FAIL(string name, int sellIn, int quality)
        {

        }

        [TestCase("", 0, 0)]
        public void AlwaysIncreaseQualityByOne_AgedBrie_PASS(string name, int sellIn, int quality)
        {

        }

        [TestCase("", 0, 50)]
        public void AlwaysIncreaseQualityByOne_AgedBrie_FAIL(string name, int sellIn, int quality)
        {

        }

        [TestCase("", 0, 80)]
        public void HoldValue_Sulfuras_PASS(string name, int sellIn, int quality)
        {

        }

        // all test cases
        public void QualityIsNeverNegative_PASS(string name, int sellIn, int quality)
        {

        }

        [TestCase("Aged Brie", WarehouseItemCategories.Aged)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", WarehouseItemCategories.Backstage)]
        [TestCase("Sulfuras, Hand of Ragnaros", WarehouseItemCategories.Sulfuras)]
        [TestCase("Conjured Mana Cake", WarehouseItemCategories.Conjured)]
        [TestCase("+5 Dexterity Vest", null)]
        [TestCase("Elixir of the Mongoose", null)]
        public void GetWarehouseItemCategory_PASS(string itemName, string expectedCategory)
        {
            Assert.AreEqual(expectedCategory, WarehouseItemCategories.GetCategory(itemName));
        }
    }
}
