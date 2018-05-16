using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
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
            item.SellIn--;

            var newQuality = item.Quality - (item.SellIn >= 0 ? Increment : Increment * 2);
            item.Quality = Increment >= 0 
                ? newQuality > Bound ? newQuality : Bound
                : newQuality < Bound ? newQuality : Bound;
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

    public class SulfurasItemUpdateStrategy : StandardItemUpdateStrategy
    {
        public override void Update(Item item)
        {
        }
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
            else if (item.SellIn <= 5 && item.SellIn > 0)
            {
                Increment = -3;
            }
            else if (item.SellIn <= 0)   
            {
                item.SellIn--;
                item.Quality = 0;
                return;
            }

            base.Update(item);

            Increment = -1;
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
                { "aged", new AgedItemUpdateStrategy() }
            };
        }

        public void UpdateItem(WarehouseItem warehouseItem)
        {
            switch (warehouseItem.Category)
            {
                case WarehouseItemCategories.Aged:
                    _strategies["aged"].Update(warehouseItem.Item);
                    break;
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
