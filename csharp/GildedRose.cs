using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    public class WarehouseItem
    {
        public Item Item { get; set; }
        public string WarehouseItemCategory { get; set; }
    }

    public class WarehouseItemCategories
    {
        public const string Aged = "Aged";
        public const string Backstage = "Backstage";
        public const string Sulfuras = "Sulfuras";
        public const string Conjured = "Conjured";

        private static List<string> _categories = new List<string>()
        {
            Aged, Backstage, Sulfuras, Conjured
        };

        public static void AddCategory(string category)
        {
            _categories.Add(category);
        }

        public static string GetCategory(string itemName)
        {
            return itemName == null ? null : _categories.FirstOrDefault(
                x => itemName.StartsWith(x, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class GildedRose
    {
        private List<WarehouseItem> _warehouse;
        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            this.Items = items;
            InitializeWarehouse(items);
        }

        private void InitializeWarehouse(IList<Item> items)
        {
            if (items == null || !items.Any())
                return;

            _warehouse = new List<WarehouseItem>(items.Count);
            foreach (var item in items)
            {
                _warehouse.Add(new WarehouseItem()
                {
                    Item = item,
                    WarehouseItemCategory = WarehouseItemCategories.GetCategory(item.Name)
                });
            }
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
