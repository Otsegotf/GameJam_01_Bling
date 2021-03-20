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

        public CartMovement Cart;

        public AisleConstructor[] Aisles;

        public JayAI JayPrefab;

        public Movement PlayerPrefab;

        public CartMovement CartPrefab;

        public Transform PlayerSpawnPoint;

        public Transform JaySpawnPoint;

        public Transform CartSpawnPoint;

        public CinemachineVirtualCamera PlayerCamera;

        public ShopItemLibrary ItemLibrary;
        
        public LabelLibrary LabelLibrary;

        public int Difficulty = 0;

        public EndZoneTrigger TriggerForEnd;
        private void Start()
        {
            ItemLibrary.Init();
            LabelLibrary.Init();
            Restart();
        }

        public void Restart()
        {
            LevelMap.Seed = Random.Range(0, 200);
            var size = (int)Mathf.Clamp(Difficulty * 1.5f, 5, 15);
            var oblong = Random.Range(-2, 3);
            LevelMap.Size =new Vector2Int(size + oblong, size - oblong);
            LevelMap.MaxBreaks = Mathf.Clamp(10 - Difficulty, 1, 5);
            var allowed = ShopItemType.Baked;
            for (int i = 1; i <= Difficulty && i < 7; i++)
            {
                allowed |= (ShopItemType)(1 << i);
            }
            LevelMap.AvailableItems = allowed;
            Difficulty++;
            Debug.Log($"FOR SALE NOW {allowed}");
            StartCoroutine(StartGame());
        }
        public IEnumerator StartGame()
        {
            if (Jay)
                GameObject.Destroy(Jay.gameObject);
            if (Player)
                GameObject.Destroy(Player.gameObject);
            if (Cart)
                GameObject.Destroy(Cart.gameObject);
            yield return LevelMap.GenerateMap();
            Aisles = LevelMap.Target.GetComponentsInChildren<AisleConstructor>();
            Jay = GameObject.Instantiate(JayPrefab, JaySpawnPoint);
            Player = GameObject.Instantiate(PlayerPrefab, PlayerSpawnPoint);
            Cart = GameObject.Instantiate(CartPrefab, CartSpawnPoint);
            Player.ControlCamera = PlayerCamera.transform;
            PlayerCamera.Follow = Player.transform;
            PlayerCamera.LookAt = Player.transform;
            Jay.SetAiActive(true);
            BuyListManager.Instance.GenerateList(Difficulty * 2);
            TriggerForEnd.gameObject.SetActive(true);
        }

        public AisleConstructor GetRandomAisle()
        {
            return Aisles[Random.Range(0, Aisles.Length)];
        }

        public void TriggerLevelEnd()
        {
            if(Player.CurrentPickup is CartMovement)
            {
                BeginVictoryCheck();
            }
            else
            {
                Debug.Log($"BRING THE CART TO END LEVEL");
            }
        }

        public void BeginVictoryCheck()
        {
            TriggerForEnd.gameObject.SetActive(false);
            var currentItems = FindObjectOfType<CartBasket>().CartInventory;
            var neededItems = BuyListManager.Instance.CurrentList;
            while(currentItems.Count > 0)
            {
                var item = currentItems.Pop();
                if (neededItems.TryGetValue(item.name, out var value))
                {
                    if (value <= 0)
                    {
                        GameOver("MORE ITEMS THAN NEEDED GAME OVER");
                        return;
                    }
                    neededItems[item.name]--;
                }
                else
                {
                    GameOver("ITEM NOT IN THE LIST. GAME OVER");                  
                    return;
                }
            }
            foreach (var item in neededItems)
            {
                if(item.Value > 0)
                {
                    GameOver("NOT ENOUGH ITEMS. GAME OVER");
                    return;
                }
            }

            Win("YOU WON");
        }

        private void GameOver(string text)
        {
            Debug.Log(text);
            Difficulty = 0;
            StartCoroutine(RestartIn(5));
        }

        private void Win(string text)
        {
            Debug.Log(text);
            StartCoroutine(RestartIn(5));
        }

        private IEnumerator RestartIn(float time)
        {
            yield return new WaitForSeconds(time);
            Restart();
        }
    }
}