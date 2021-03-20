using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    [CreateAssetMenu(fileName = "ItemLibrary.asset", menuName = "Asset/ItemLibrary")]
    public class ShopItemLibrary : ScriptableObject
    {
        public ShopItem[] Items;

        private Dictionary<ShopItemType, List<ShopItem>> _possibleItems;

        public void Init()
        {
            _possibleItems = new Dictionary<ShopItemType, List<ShopItem>>();
            for (int i = 0; i < Items.Length; i++)
            {
                var type = Items[i].ItemType;
                if (!_possibleItems.TryGetValue(type, out var value))
                {
                    _possibleItems[type] = new List<ShopItem>() { Items[i] };
                }
                else
                {
                    _possibleItems[type].Add(Items[i]);
                }
            }
        }

        public ShopItem GetRandomItemOfType(ShopItemType type)
        {
            var testedList = _possibleItems[type];
            return (testedList[Random.Range(0, testedList.Count)]);
        }
    }
}