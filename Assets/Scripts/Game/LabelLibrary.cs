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

        private Dictionary<ShopItemType, MaterialPropertyBlock> _colorBlocks;

        public void Init()
        {
            _labels = new Dictionary<ShopItemType, ShopLabel>();
            _colorBlocks = new Dictionary<ShopItemType, MaterialPropertyBlock>();
            for (int i = 0; i < Items.Length; i++)
            {
                var type = Items[i].ItemType;
                _labels[type] = Items[i];

                var block = new MaterialPropertyBlock();
                block.SetColor("_BaseColor", Items[i].TypeColor);
                _colorBlocks[type] = block;
            }
        }

        public ShopLabel GetLabel(ShopItemType type)
        {
            return _labels[type];
        }

        public MaterialPropertyBlock GetBlock(ShopItemType type)
        {
            return _colorBlocks[type];
        }
    }
}