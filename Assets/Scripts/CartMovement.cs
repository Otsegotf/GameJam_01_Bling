using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class CartMovement : MonoBehaviour, IPickupAble
    {
        public void SendCanBeAttached()
        {
            var player = GameManager.Instance.Player;
            player.SetTrackedPickupable(this);
        }

        public void AttachCartToPlayer()
        {
            var player = GameManager.Instance.Player;
            if (player.CarriedItem != null)
                return;
            player.SetCartState(true);
            transform.SetParent(player.Hands.transform);
            var pos = player.Hands.transform.position;
            pos.y = 0;
            transform.position = pos;
            transform.rotation = Quaternion.LookRotation(player.transform.forward);
            player.SetCurrentPickup(this);
        }

        public void DeatachCart()
        { 
            var player = GameManager.Instance.Player;
            player.SetCartState(false);
            transform.SetParent(null);
            player.SetCurrentPickup(null);
        }

        public void Pickup()
        {
            AttachCartToPlayer();
        }

        public void Drop()
        {
            DeatachCart();
        }
    }
}