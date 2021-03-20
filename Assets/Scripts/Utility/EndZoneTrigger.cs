using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class EndZoneTrigger : MonoBehaviour
    {
        public UnityEvent OnTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Movement>())
                OnTriggered.Invoke();
        }
    }
}