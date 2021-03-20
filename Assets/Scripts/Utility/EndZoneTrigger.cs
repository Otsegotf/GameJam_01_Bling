using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class EndZoneTrigger : MonoBehaviour
    {
        public UnityEvent OnTriggered;

        public UnityEvent OnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Movement>())
                OnTriggered.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<Movement>())
                OnExit.Invoke();
        }
    }
}