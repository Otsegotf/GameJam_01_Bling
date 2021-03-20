using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class CartMovement : MonoBehaviour
    {
        public void SendCanBeAttached()
        {
            var player = GameManager.Instance.Player;
            player.SetCartAvailable(this, true);
        }

        public void SendCannotAttach()
        {
            var player = GameManager.Instance.Player;
            player.SetCartAvailable(this, false);
        }


        public void AttachCartToPlayer()
        {
            var player = GameManager.Instance.Player;
            player.SetCartState(true);
            transform.SetParent(player.Hands.transform);
            transform.position = player.transform.position + player.transform.forward;
            transform.rotation = Quaternion.LookRotation(player.transform.forward);
        }

        public void DeatachCart()
        { 
            var player = GameManager.Instance.Player;
            player.SetCartState(false);
            transform.SetParent(null);
        }

        private void Update()
        {
            //if (_trackedTransform != null)
            //{
            //    var targetPoint = _trackedTransform.position + _trackedTransform.forward;
            //    targetPoint.y = 0;
            //    var distance = targetPoint - transform.position;
            //    distance.y = 0;
            //    var rot = Quaternion.LookRotation(_trackedTransform.forward);
            //    if (distance.sqrMagnitude > 0.1f)
            //    {
            //        Body.MovePosition (targetPoint);
            //    }

            //    Body.rotation = Quaternion.LookRotation(_trackedTransform.forward);
            //    return;
            //    var forw = _trackedTransform.forward;
            //    forw.y = 0;
            //    var forwMy = transform.forward;
            //    forwMy.y = 0;

            //    var angle = Vector3.SignedAngle(forwMy, forw, Vector3.up);
            //    Body.angularVelocity = new Vector3(0,angle,0);
            //}
        }
    }
}