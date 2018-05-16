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
    public interface IWarehouseItemUpdateStrategy
    {
        void Update(Item item);
    }

    public class StandardItemUpdateStrategy : IWarehouseItemUpdateStrategy
    {
        public void Update(Item item)
        {
            if (item.Quality <= 0)
                return;

            if (item.SellIn <= 0 && item.Quality > 1)
                item.Quality = item.Quality - 2;
            else
                item.Quality--;
        }
    }

    public class WarehouseItemUpdateService
    {
        private Dictionary<string, IWarehouseItemUpdateStrategy> _strategies;

        public WarehouseItemUpdateService()
        {
            _strategies = new Dictionary<string, IWarehouseItemUpdateStrategy>()
            {
                { "default", new StandardItemUpdateStrategy() }
            };
        }

        public void UpdateItem(WarehouseItem warehouseItem)
        {
            switch (warehouseItem.Category)
            {
                default:
                    _strategies["default"].Update(warehouseItem.Item);
                    break;
            }
        }
    }
}
