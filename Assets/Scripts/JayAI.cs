using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class JayAI : MonoBehaviour
    {
        public NavMeshAgent Agent;

        public AisleConstructor TargetAisle;

        public float CD = 10;

        private float _curCD = 5;

        public Vector3 Target;

        private Coroutine _linkRoutine;

        public void SetAiActive(bool state)
        {
            Agent.enabled = state;
            if (state)
                Repath();
        }
        private void Awake()
        {
            SetAiActive(false);
            Agent.autoTraverseOffMeshLink = false;            
        }
        private void Update()
        {
            if (!Agent.enabled)
                return;
            _curCD -= Time.deltaTime;
            if (_curCD <= 0)
            {
                _curCD = CD;
                Repath();
            }
            if (Agent.isOnOffMeshLink)
            {
                if(_linkRoutine == null)
                {
                    _linkRoutine = StartCoroutine(NormalSpeed(Agent));
                }
            }
        }

        void Repath()
        {
            TargetAisle = GameManager.Instance.GetRandomAisle();
            Agent.SetDestination(TargetAisle.AisleTarget.position);
        }
        IEnumerator NormalSpeed(NavMeshAgent agent)
        {
            var data = agent.currentOffMeshLinkData;
            var endPos = data.endPos + Vector3.up * agent.baseOffset;
            agent.updateRotation = false;
            while (agent.transform.position != endPos)
            {
                agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, Quaternion.LookRotation(endPos - agent.transform.position), agent.angularSpeed * Time.deltaTime);
                agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);           
                yield return null;
            }
            agent.updateRotation = true;
            agent.CompleteOffMeshLink();
            _linkRoutine = null;
        }
    }
}