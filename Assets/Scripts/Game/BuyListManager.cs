using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class BuyListManager : Singleton<BuyListManager>
    {

        public List<ShopItem> CurrentList;
        public void GenerateList(int length)
        {
            CurrentList = new List<ShopItem>();
            var aisles = GameManager.Instance.Aisles;
            for (int i = 0; i < length; i++)
            {
                var aisle = GameManager.Instance.GetRandomAisle();
                CurrentList.Add(aisle.ItemPrefab);
            }
            Debug.Log("[SHOPPING LIST] NEW LIST IS");
            for (int i = 0; i < CurrentList.Count; i++)
            {
                Debug.Log(CurrentList[i]);
            }
        }
    }
}