using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class BuyListManager : Singleton<BuyListManager>
    {

        public Dictionary<ShopItem, int> CurrentList;
        public void GenerateList(int length)
        {
            CurrentList = new Dictionary<ShopItem, int>();
            AisleConstructor aisle = null;
            for (int i = 0; i < length; i++)
            {
                while(aisle == null || !aisle.ItemPrefab)
                    aisle = GameManager.Instance.GetRandomAisle();
                if(!CurrentList.ContainsKey(aisle.ItemPrefab))
                {
                    CurrentList[aisle.ItemPrefab] = 0;
                }
                CurrentList[aisle.ItemPrefab]++;
                aisle = null;
            }
            Debug.Log("[SHOPPING LIST] NEW LIST IS");
            foreach (var item in CurrentList)
            {
                Debug.Log($"{item.Key.name} x {item.Value}");
            }
        }
    }
}