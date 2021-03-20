using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class JayAI : MonoBehaviour, IPickupAble
    {
        public NavMeshAgent Agent;

        public Animator BobAnim;

        //0 - idle 1 - walk 2 - steal 3 - stun
        public string BlendParam = "AnimBlend";

        public AisleConstructor TargetAisle;

        public float IdlingTime = 10;

        public float StunTime = 10;

        public float StealCD = 10;

        public float StealTime = 15;

        public float _curStealCd = 5;

        public float _curActionCd = 5;

        private Coroutine _linkRoutine;

        public BobState CurrentState;

        public bool IsPlaying = false;

        private void Awake()
        {
            Agent.autoTraverseOffMeshLink = false;
            _curStealCd = StealCD;
            SetState(BobState.Idling);
        }
        private void Update()
        {
            if (!IsPlaying)
                return;
            _curActionCd -= Time.deltaTime;
            _curStealCd -= Time.deltaTime;
            if(_curStealCd < 0 && CurrentState < BobState.Stunned && _linkRoutine == null)
            {
                SetState(BobState.MovingForSteal);
            }
            if(_curActionCd < 0 && CurrentState < BobState.MovingForSteal && _linkRoutine == null)
            {
                SetState((BobState)Random.Range(0, 2));
            }
            if(_curActionCd < 0 && CurrentState == BobState.Stealing)
            {
                CurrentState = BobState.MissionFailedSuccesfuly;
                GameManager.Instance.GameOver("BOB STOLE SOME STUFF, GAME OVER");
            }
            if (!Agent.enabled)
                return;

            if(_curActionCd < 0 && Agent.remainingDistance < 1)
            {
                if (CurrentState == BobState.MovingForSteal)
                {
                    var direction = TargetAisle.AisleTarget.position - Agent.transform.position;
                    Agent.transform.rotation = Quaternion.LookRotation(direction);
                    SetState(BobState.Stealing);
                }
                else
                    SetState(BobState.Idling);
            }

            if (Agent.isOnOffMeshLink)
            {
                if(_linkRoutine == null)
                {
                    _linkRoutine = StartCoroutine(NormalSpeed(Agent));
                }
            }
        }

        private void SetState(BobState newState)
        {
            CurrentState = newState;
            switch (newState)
            {
                case BobState.Idling:
                    Agent.enabled = false;
                    BobAnim.SetFloat(BlendParam, 0);
                    _curActionCd = IdlingTime;
                    break;
                case BobState.Strolling:
                    Agent.enabled = true;
                    BobAnim.SetFloat(BlendParam, 1);
                    Repath();
                    _curActionCd = IdlingTime;
                    break;
                case BobState.MovingForSteal:
                    Agent.enabled = true;
                    BobAnim.SetFloat(BlendParam, 1);
                    _curActionCd = 1f;
                    Repath(true);
                    break;
                case BobState.Stealing:
                    Agent.enabled = false;
                    BobAnim.SetFloat(BlendParam, 2);
                    _curActionCd = StealTime;
                    break;
                case BobState.Stunned:
                    Agent.enabled = false;
                    BobAnim.SetFloat(BlendParam, 3);
                    _curActionCd = StunTime;
                    _curStealCd = StealCD;
                    break;
                default:
                    break;
            }
        }

        void Repath(bool mustHaveItem = false)
        {
            TargetAisle = GameManager.Instance.GetRandomAisle();

            while (mustHaveItem && TargetAisle.ItemPrefab == null)
            {
                TargetAisle = GameManager.Instance.GetRandomAisle();
            }

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

        public void Pickup()
        {
            if(CurrentState == BobState.Stealing)
            {
                SetState(BobState.Stunned);
                GameManager.Instance.Player.PlayerAnim.SetTrigger("Hit");
            }
        }

        public void Drop()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponentInParent<Movement>();
            if (player)
            {
                player.SetTrackedPickupable(this);
            }
        }
    }

    public enum BobState
    {
        Idling,
        Strolling,
        Stunned,
        MovingForSteal,
        Stealing,
        MissionFailedSuccesfuly
    }
}