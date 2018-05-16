using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    public class GildedRose
    {
        private List<WarehouseItem> _warehouse;
        IList<Item> Items; // old solution
        private WarehouseItemUpdateService _warehouseItemUpdateService;

        public GildedRose(IList<Item> items)
        {
            _warehouseItemUpdateService = new WarehouseItemUpdateService();

            this.Items = items; // old solution
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
                    Category = WarehouseItemCategories.GetCategory(item.Name)
                });
            }
        }

        public List<Item> GetItems()
        {
            return _warehouse.Select(x => x.Item).ToList();
        }

        public List<Item> GetItems_Old()
        {
            return Items.ToList();
        }

        private void UpdateItem(WarehouseItem item)
        {
            _warehouseItemUpdateService.UpdateItem(item);
        }

        public void UpdateQuality()
        {
            foreach (var item in _warehouse)
            {
                UpdateItem(item);
            }
        }

        // old solution
        public void UpdateQuality_Old()
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
