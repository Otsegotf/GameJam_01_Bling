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

        public float CD = 10;

        private float _curCD = 5;

        public Bounds MovementBounds;

        public Vector3 Target;
        private void Update()
        {
            _curCD -= Time.deltaTime;
            if (_curCD <= 0)
            {
                _curCD = CD;
                Repath();
            }
        }

        void Repath()
        {
            var extents = Random.insideUnitSphere;
            extents.Scale(MovementBounds.extents);
            Target = extents + new Vector3(TileMap.Instance.Size.x, 0, TileMap.Instance.Size.y) * 2;
            Agent.SetDestination(Target);
        }
    }
}