using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Reflection;

namespace csharp
{
    public class TestData
    {
        private static Dictionary<string, Item> _items = new Dictionary<string, Item>()
        {
            { "dexterity_10_20", new Item {Name = "+5 dexterity_10_20 Vest", SellIn = 10, Quality = 20} },
            { "dexterity_-1_10", new Item {Name = "+5 dexterity_10_20 Vest", SellIn = -1, Quality = 10} },
            { "dexterity_-1_1", new Item {Name = "+5 dexterity_10_20 Vest", SellIn = -1, Quality = 1} },

            { "aged_2_0", new Item {Name = "Aged Brie", SellIn = 2, Quality = 0}},
            { "aged_-1_0", new Item {Name = "Aged Brie", SellIn = -1, Quality = 0}},
            
            { "sulfuras_0_80", new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80}},
            { "sulfuras_-1_80", new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80}},

            { "backstage_15_20", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            }},
            { "backstage_10_0", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 0
            }},
            { "backstage_10_49", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            }},
            { "backstage_5_49", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            }},
            { "backstage_5_0", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 0
            }},
            { "backstage_-1_0", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = -1,
                Quality = 0
            }},
            { "backstage_14_21", new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 14,
                Quality = 21
            }},

            { "conjured_3_6", new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}},
            { "conjured_3_1", new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 1}},
            { "conjured_-1_10", new Item {Name = "Conjured Mana Cake", SellIn = -1, Quality = 10}},
            { "conjured_-1_1", new Item {Name = "Conjured Mana Cake", SellIn = -1, Quality = 1}}
        };

        public static List<Item> GetItemByNameList(string name)
        {
            return new List<Item>()
            {
                new Item()
                {
                    Name = _items[name].Name,
                    Quality = _items[name].Quality,
                    SellIn = _items[name].SellIn
                }
            };
        }

        public static Item GetItemByName(string name)
        {
            return new Item()
            {
                Name = _items[name].Name,
                Quality = _items[name].Quality,
                SellIn = _items[name].SellIn
            };
        }
    }

    [TestFixture]
    public class GildedRoseTest
    {
        [TestCase("", 0, 51)]
        [TestCase("", 0, -1)]
        public void UpdateQualityNotThrowExceptionOnInvalidData(string name, int sellIn, int quality)
        {
            var items = new List<Item> {new Item {Name = name, SellIn = sellIn, Quality = quality}};
            var gildedRose = new GildedRose(items);

            gildedRose.UpdateQuality();

            Assert.DoesNotThrow(() => gildedRose.UpdateQuality());
        }

        [TestCase("dexterity_10_20", 1)]
        [TestCase("dexterity_-1_10", 2)]
        [TestCase("dexterity_-1_1", 1)]
        [TestCase("aged_-1_0", -2)]
        [TestCase("aged_2_0", -1)]
        [TestCase("backstage_15_20", -1)]
        [TestCase("backstage_10_0", -2)]
        [TestCase("backstage_5_49", -1)]
        [TestCase("backstage_10_49", -1)]
        [TestCase("backstage_5_0", -3)]
        [TestCase("backstage_-1_0", 0)]
        [TestCase("conjured_3_6", 2)]
        [TestCase("conjured_3_1", 1)]
        [TestCase("conjured_-1_10", 4)]
        [TestCase("conjured_-1_1", 1)]
        public void DecreaseQualityByDecrement(string itemName, int decrement)
        {
            var gildedRose = new GildedRose(TestData.GetItemByNameList(itemName));

            gildedRose.UpdateQuality();

            Assert.IsTrue(gildedRose.GetItems() != null && gildedRose.GetItems().Count == 1);
            Assert.AreEqual(TestData.GetItemByName(itemName).Quality - decrement, 
                gildedRose.GetItems().First().Quality);

            Assert.AreEqual(TestData.GetItemByName(itemName).SellIn - 1,
                gildedRose.GetItems().First().SellIn);
        }

        [TestCase("backstage_15_20", -1)]
        public void DecreaseQualityByDecrement_Compare(string itemName, int decrement)
        {
            var gildedRose = new GildedRose(TestData.GetItemByNameList(itemName));

            for (int i = 0; i < 10; i++)
            {
                gildedRose.UpdateQuality();

                Assert.IsTrue(gildedRose.GetItems() != null && gildedRose.GetItems().Count == 1);
                //Assert.AreEqual(TestData.GetItemByName(itemName).Quality - decrement,
                //    gildedRose.GetItems().First().Quality);

                //Assert.AreEqual(TestData.GetItemByName(itemName).SellIn - 1,
                //    gildedRose.GetItems().First().SellIn);
            }
        }

        [TestCase("backstage_15_20", -1)]
        public void DecreaseQualityByDecrement_Old_Compare(string itemName, int decrement)
        {
            var gildedRose = new GildedRose(TestData.GetItemByNameList(itemName));

            gildedRose.UpdateQuality_Old();

            Assert.IsTrue(gildedRose.GetItems_Old() != null && gildedRose.GetItems_Old().Count == 1);
            Assert.AreEqual(TestData.GetItemByName(itemName).Quality - decrement,
                gildedRose.GetItems_Old().First().Quality);

            Assert.AreEqual(TestData.GetItemByName(itemName).SellIn - 1,
                gildedRose.GetItems_Old().First().SellIn);
        }

        [TestCase("dexterity_10_20", 1)]
        [TestCase("dexterity_-1_10", 2)]
        [TestCase("dexterity_-1_1", 1)]
        [TestCase("aged_-1_0", -2)]
        [TestCase("aged_2_0", -1)]
        [TestCase("backstage_15_20", -1)]
        [TestCase("backstage_10_0", -2)]
        [TestCase("backstage_5_49", -1)]
        [TestCase("backstage_10_49", -1)]
        [TestCase("backstage_5_0", -3)]
        [TestCase("backstage_-1_0", 0)]
        public void DecreaseQualityByDecrement_Old(string itemName, int decrement)
        {
            var gildedRose = new GildedRose(TestData.GetItemByNameList(itemName));

            gildedRose.UpdateQuality_Old();

            Assert.IsTrue(gildedRose.GetItems_Old() != null && gildedRose.GetItems_Old().Count == 1);
            Assert.AreEqual(TestData.GetItemByName(itemName).Quality - decrement,
                gildedRose.GetItems_Old().First().Quality);

            Assert.AreEqual(TestData.GetItemByName(itemName).SellIn - 1,
                gildedRose.GetItems_Old().First().SellIn);
        }

        [TestCase("sulfuras_0_80", 0)]
        [TestCase("sulfuras_-1_80", 0)]
        public void DecreaseQualityByDecrement_Sulfuras(string itemName, int decrement)
        {
            var gildedRose = new GildedRose(TestData.GetItemByNameList(itemName));

            gildedRose.UpdateQuality();

            Assert.IsTrue(gildedRose.GetItems() != null && gildedRose.GetItems().Count == 1);
            Assert.AreEqual(TestData.GetItemByName(itemName).Quality - decrement,
                gildedRose.GetItems().First().Quality);

            Assert.AreEqual(TestData.GetItemByName(itemName).SellIn,
                gildedRose.GetItems().First().SellIn);
        }

        [TestCase("dexterity_10_20", 1)]
        [TestCase("dexterity_-1_10", 2)]
        [TestCase("dexterity_-1_1", 1)]
        [TestCase("aged_-1_0", -2)]
        [TestCase("aged_2_0", -1)]
        [TestCase("sulfuras_0_80", 0)]
        [TestCase("sulfuras_-1_80", 0)]
        [TestCase("backstage_15_20", -1)]
        [TestCase("backstage_10_0", -2)]
        [TestCase("backstage_5_49", -1)]
        [TestCase("backstage_10_49", -1)]
        [TestCase("backstage_5_0", -5)]
        [TestCase("backstage_-1_0", 0)]
        [TestCase("conjured_3_6", 2)]
        [TestCase("conjured_3_1", 1)]
        [TestCase("conjured_-1_10", 4)]
        [TestCase("conjured_-1_1", 1)]
        public void QualityAlwaysGreaterThan0(string itemName, int nothing)
        {
            var gildedRose = new GildedRose(TestData.GetItemByNameList(itemName));

            for (int i = 0; i < 50; i++)
            {
                gildedRose.UpdateQuality();
            }

            Assert.IsTrue(gildedRose.GetItems() != null && gildedRose.GetItems().Count == 1);
            Assert.IsTrue(gildedRose.GetItems().First().Quality >= 0);
        }

        [TestCase("Aged Brie", WarehouseItemCategories.Aged)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", WarehouseItemCategories.Backstage)]
        [TestCase("Sulfuras, Hand of Ragnaros", WarehouseItemCategories.Sulfuras)]
        [TestCase("Conjured Mana Cake", WarehouseItemCategories.Conjured)]
        [TestCase("+5 dexterity_10_20 Vest", null)]
        [TestCase("Elixir of the Mongoose", null)]
        public void GetWarehouseItemCategory_PASS(string itemName, string expectedCategory)
        {
            Assert.AreEqual(expectedCategory, WarehouseItemCategories.GetCategory(itemName));
        }
    }
}
