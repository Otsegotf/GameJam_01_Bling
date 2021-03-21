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
            if (StunFX != null)
                StunFX.SetActive(false);
            if (BashFX != null)
                BashFX.SetActive(false);
        }
        public void StunStart()
        {
            if (StunFX != null)
                StunFX.SetActive(true);
        }

        public void BashStart()
        {
            if (BashFX != null)
                BashFX.SetActive(true);
        }
    }
}