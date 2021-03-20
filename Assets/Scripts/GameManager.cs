using Cinemachine;
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

        public Movement Player;

        public AisleConstructor[] Aisles;

        public JayAI JayPrefab;

        public Movement PlayerPrefab;

        public Transform PlayerSpawnPoint;

        public Transform JaySpawnPoint;

        public CinemachineVirtualCamera PlayerCamera;

        public ShopItemLibrary ItemLibrary;
        private void Start()
        {
            ItemLibrary.Init();
            Restart();
        }

        public void Restart()
        {
            LevelMap.Seed = Random.Range(0, 200);
            StartCoroutine(StartGame());
        }
        public IEnumerator StartGame()
        {
            if (Jay)
                GameObject.Destroy(Jay.gameObject);
            if (Player)
                GameObject.Destroy(Player.gameObject);
            yield return LevelMap.GenerateMap();
            Aisles = LevelMap.Target.GetComponentsInChildren<AisleConstructor>();
            Jay = GameObject.Instantiate(JayPrefab, JaySpawnPoint);
            Player = GameObject.Instantiate(PlayerPrefab, PlayerSpawnPoint);
            Player.ControlCamera = PlayerCamera.transform;
            PlayerCamera.Follow = Player.transform;
            PlayerCamera.LookAt = Player.transform;
            Jay.SetAiActive(true);
        }

        public AisleConstructor GetRandomAisle()
        {
            return Aisles[Random.Range(0, Aisles.Length)];
        }
    }
}