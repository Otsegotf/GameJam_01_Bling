using System;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class ShopItem : MonoBehaviour
    {
        public ShopItemType ItemType;
    }

    [Flags]
    public enum ShopItemType
    {
        Baked = 1 << 0,
        Meats = 1 << 1,
        FruitAndVeg = 1 << 2,
        Alchohol = 1 << 3,
        Frozen = 1 << 4,
        Dairy = 1 << 5,
        Drinks = 1 << 6
    }
}