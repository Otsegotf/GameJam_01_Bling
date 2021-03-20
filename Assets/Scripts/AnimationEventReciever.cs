using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class AnimationEventReciever : MonoBehaviour
    {
        public GameObject StunFX;

        public GameObject BashFX;

        public void Clear()
        {
            StunFX.SetActive(false);
            BashFX.SetActive(false);
        }
        public void StunStart()
        {
            StunFX.SetActive(true);
        }

        public void BashStart()
        {
            BashFX.SetActive(true);
        }
    }
}