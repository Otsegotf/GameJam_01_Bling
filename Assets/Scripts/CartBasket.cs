using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class CartBasket : MonoBehaviour, IPickupAble
    {
        public Stack<ShopItem> CartInventory = new Stack<ShopItem>();
      

        public void Pickup()
        {
            var player = GameManager.Instance.Player;
            if (player.CarriedItem != null)
            {
                CartInventory.Push(player.CarriedItem);
                PutItemInBasket(player.CarriedItem);
                player.CarriedItem = null;
                player.SetCurrentPickup(null);
            }
            else
            {
                if (CartInventory.Count > 0)
                {
                    var item = CartInventory.Pop();
                    player.CarriedItem = item;
                    item.transform.SetParent(player.Hands.transform, false);
                }
            }
        }

        public void Drop()
        {

        }

        public void RegisterPickup()
        {
            var player = GameManager.Instance.Player;
            player.SetTrackedPickupable(this);
        }

        private void PutItemInBasket(ShopItem item)
        {
            item.transform.SetParent(transform, false);
            item.transform.localPosition = Vector3.zero;
        }
    }
}