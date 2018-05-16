using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    /*
    - Once the sell by date has passed, Quality degrades twice as fast
	- The Quality of an item is never negative
	- "Aged Brie" actually increases in Quality the older it gets
	- The Quality of an item is never more than 50
	- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
	        Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
	        Quality drops to 0 after the concert

    We have recently signed a supplier of conjured items. This requires an update to our system:

	- "Conjured" items degrade in Quality twice as fast as normal items
     */
    public class WarehouseItem
    {
        public Item Item { get; set; }
        public string Category { get; set; }
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

    public interface IWarehouseItemUpdateStrategy
    {
        void Update(Item item);
    }

    public class StandardItemUpdateStrategy : IWarehouseItemUpdateStrategy
    {
        protected int Increment { get; set; }
        protected int Bound { get; set; }

        public StandardItemUpdateStrategy()
        {
            Increment = 1;
            Bound = 0;
        }

        public virtual void Update(Item item)
        {
            if (item.Quality <= 0)
                return;

            var newQuality = item.Quality - (item.SellIn >= 0 ? Increment : Increment * 2);
            item.Quality = newQuality > Bound ? newQuality : Bound;
        }
    }

    public class ConjuredItemUpdateStrategy : StandardItemUpdateStrategy
    {
        public ConjuredItemUpdateStrategy()
        {
            Increment = 2;
            Bound = 0;
        }
    }

    public class SulfurasItemUpdateStrategy : IWarehouseItemUpdateStrategy
    {
        public void Update(Item item) {}
    }

    public class AgedItemUpdateStrategy : StandardItemUpdateStrategy
    {
        public AgedItemUpdateStrategy()
        {
            Increment = -1;
            Bound = 50;
        }
    }

    public class BackstageItemUpdateStrategy : StandardItemUpdateStrategy
    {
        public BackstageItemUpdateStrategy()
        {
            Increment = -1;
            Bound = 50;
        }

        public override void Update(Item item)
        {
            if (item.SellIn <= 10 && item.SellIn > 5)
            {
                Increment = -2;
            }
            else if (item.SellIn <= 5)
            {
                Increment = -3;
            }

            base.Update(item);
        }
    }

    public class WarehouseItemUpdateService
    {
        private Dictionary<string, IWarehouseItemUpdateStrategy> _strategies;

        public WarehouseItemUpdateService()
        {
            _strategies = new Dictionary<string, IWarehouseItemUpdateStrategy>()
            {
                { "default", new StandardItemUpdateStrategy() },
                { "conjured", new ConjuredItemUpdateStrategy() },
                { "sulfuras", new SulfurasItemUpdateStrategy() },
                { "backstage", new BackstageItemUpdateStrategy() },
            };
        }

        public void UpdateItem(WarehouseItem warehouseItem)
        {
            switch (warehouseItem.Category)
            {
                case WarehouseItemCategories.Backstage:
                    _strategies["backstage"].Update(warehouseItem.Item);
                    break;
                case WarehouseItemCategories.Sulfuras:
                    _strategies["sulfuras"].Update(warehouseItem.Item);
                    break;
                case WarehouseItemCategories.Conjured:
                    _strategies["conjured"].Update(warehouseItem.Item);
                    break;
                default:
                    _strategies["default"].Update(warehouseItem.Item);
                    break;
            }
        }
    }
}
