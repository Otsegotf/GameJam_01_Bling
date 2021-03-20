using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class GameManager : Singleton<GameManager>
    {
        public TileMap LevelMap;

        public JayAI Jay;

        public AisleConstructor[] Aisles;

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        public IEnumerator StartGame()
        {
            yield return LevelMap.GenerateMap();
            Aisles = LevelMap.Target.GetComponentsInChildren<AisleConstructor>();
            Jay.SetAiActive(true);
        }

        public AisleConstructor GetRandomAisle()
        {
            return Aisles[Random.Range(0, Aisles.Length)];
        }
    }
}