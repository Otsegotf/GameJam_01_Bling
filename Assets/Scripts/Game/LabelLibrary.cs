using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    [CreateAssetMenu(fileName = "LabelLibrary.asset", menuName = "Asset/LabelLibrary")]
    public class LabelLibrary : ScriptableObject
    {
        public ShopLabel[] Items;

        private Dictionary<ShopItemType, ShopLabel> _labels;

        public void Init()
        {
            _labels = new Dictionary<ShopItemType, ShopLabel>();
            for (int i = 0; i < Items.Length; i++)
            {
                var type = Items[i].ItemType;
                _labels[type] = Items[i];
            }
        }

        public ShopLabel GetLabel(ShopItemType type)
        {
            return _labels[type];
        }
    }
}