using System;
using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class AisleItemProducer : MonoBehaviour, IPickupAble
    {
        public AisleConstructor Aisle;

        public void Drop()
        {

        }

        public void Pickup()
        {
            if (Aisle.ItemPrefab == null)
                return;
            var player = GameManager.Instance.Player;
            if (player.CarriedItem == null && player.CurrentPickup == null)
            {
                player.CarriedItem = GameObject.Instantiate(Aisle.ItemPrefab, player.Hands.transform);
                player.CarriedItem.name = Aisle.ItemPrefab.name;
            }
            else
            if (player.CarriedItem != null && player.CarriedItem.name == Aisle.ItemPrefab.name)
            {
                GameObject.Destroy(player.CarriedItem.gameObject);
            }

        }

        public void RegisterPickup()
        {
            var player = GameManager.Instance.Player;
            player.SetTrackedPickupable(this);
        }
    }
}