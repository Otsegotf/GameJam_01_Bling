using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class BuyListManager : Singleton<BuyListManager>
    {

        public Dictionary<string, ShopItemCount> CurrentList;
        public void GenerateList(int length)
        {
            CurrentList = new Dictionary<string, ShopItemCount>();
            AisleConstructor aisle = null;
            for (int i = 0; i < length; i++)
            {
                while(aisle == null || !aisle.ItemPrefab)
                    aisle = GameManager.Instance.GetRandomAisle();
                if(!CurrentList.ContainsKey(aisle.ItemPrefab.name))
                {
                    CurrentList[aisle.ItemPrefab.name] = new ShopItemCount() { Item = aisle.ItemPrefab };
                }
                CurrentList[aisle.ItemPrefab.name].Count++;
                aisle = null;
            }
            Debug.Log("[SHOPPING LIST] NEW LIST IS");
            foreach (var item in CurrentList)
            {
                Debug.Log($"{item.Key} x {item.Value}");
            }
        }
    }

    public class ShopItemCount
    {
        public int Count;

        public ShopItem Item;
    }
}