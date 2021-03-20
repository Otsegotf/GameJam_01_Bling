using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class CartBasket : MonoBehaviour, IPickupAble
    {
        public Stack<ShopItem> CartInventory = new Stack<ShopItem>();

        public int itemPerLevel = 4;

        public float YLevel = 0.1f;

        public Collider BasketBoundary;

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
            item.transform.SetParent(BasketBoundary.transform, false);

            var localPos = Vector3.Scale(Random.insideUnitSphere, BasketBoundary.bounds.extents);
            localPos = BasketBoundary.transform.rotation * localPos;
            localPos.y = YLevel * (CartInventory.Count / itemPerLevel);
            item.transform.localRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            item.transform.localPosition = localPos;
        }
    }
}