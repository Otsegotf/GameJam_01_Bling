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

        public CinemachineVirtualCamera BobCamera;

        public CinemachineVirtualCamera CashierCamera;

        public ShopItemLibrary ItemLibrary;
        
        public LabelLibrary LabelLibrary;

        public int Difficulty = 0;

        public EndZoneTrigger TriggerForEnd;

        public float LevelTime = 60;

        public GameObject JailPrefab;

        private Coroutine _gameTimerRoutine;
        public void Restart()
        {
            ItemLibrary.Init();
            LabelLibrary.Init();
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

        public IEnumerator TimerRoutine()
        {
            while (LevelTime > 0f)
            {
                var newTime = LevelTime - Time.deltaTime;
                if (LevelTime > 6f && newTime < 6f) 
                {
                    JayAudioManager.Instance.PlayTimer();
                }
                LevelTime = newTime;
                yield return null;
            }
            GameOver(GameOverType.TimeOut);
        }
        public IEnumerator StartGame()
        {
            Transition.Instance.SetState(true);
            while (Transition.Instance.CurrentTransitionState < 1)
                yield return null;

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

            BobCamera.Follow = Jay.transform;
            BobCamera.LookAt = Jay.transform;


            LevelTime = Difficulty * 30 + 15;
            BuyListManager.Instance.GenerateList(Difficulty * 2);

            ShoppingListUI.Instance.UpdateList();

            Player.enabled = false;

            MusicPlayer.Instance.Stop();
            MusicPlayer.Instance.Play();

            Transition.Instance.SetState(false);
            while (Transition.Instance.CurrentTransitionState > 0)
                yield return null;
            Player.enabled = true;
            Jay.IsPlaying = true;
            TriggerForEnd.gameObject.SetActive(true);
            _gameTimerRoutine = StartCoroutine(TimerRoutine());
        }

        public AisleConstructor GetRandomAisle()
        {
            return Aisles[Random.Range(0, Aisles.Length)];
        }

        public void TriggerLevelEnd()
        {
            if(Player.CurrentPickup is CartMovement)
            {
              StartCoroutine(BeginVictoryCheck());
            }
            else
            {
                Debug.Log($"BRING THE CART TO END LEVEL");
            }
        }

        public Transform ItemStartPosition;

        public Transform ItemEndPosition;

        public float ItemMoveTime = 1;

        public float CurrentItemPosition;
        public IEnumerator BeginVictoryCheck()
        {
            var currentItems = FindObjectOfType<CartBasket>().CartInventory;
            TriggerForEnd.gameObject.SetActive(false);
            StopCoroutine(_gameTimerRoutine);
            CashierCamera.gameObject.SetActive(true);
            Player.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);

            if (Jay)
                Jay.gameObject.SetActive(false);
            if (Cart)
                Cart.gameObject.SetActive(false);

            var neededItems = BuyListManager.Instance.CurrentList;
            while(currentItems.Count > 0)
            {
                CurrentItemPosition = 0;
                var item = currentItems.Pop();
                item.transform.SetParent(ItemStartPosition);
                while(CurrentItemPosition < 1)
                {
                    CurrentItemPosition += Time.deltaTime / ItemMoveTime;
                    item.transform.position = Vector3.Lerp(ItemStartPosition.position, ItemEndPosition.position, CurrentItemPosition);
                    yield return null;
                }
                if (neededItems.TryGetValue(item.name, out var value))
                {
                    if (value.Count <= 0)
                    {
                        GameOver(GameOverType.TooMuch);
                        yield break;
                    }
                    neededItems[item.name].Count--;
                }
                else
                {
                    GameOver(GameOverType.TooMuch);
                    yield break;
                }
            }
            foreach (var item in neededItems)
            {
                if(item.Value.Count > 0)
                {
                    GameOver(GameOverType.TooLittle);
                    yield break;
                }
            }

            Win("YOU WON");
        }

        private Coroutine _gameOver;
        public void BobGameOver()
        {
            if(_gameOver == null)
                _gameOver = StartCoroutine(BobGameOverRoutine());
        }

        private IEnumerator BobGameOverRoutine()
        {
            StopCoroutine(_gameTimerRoutine);
            TriggerForEnd.gameObject.SetActive(false);
            Player.enabled = false;
            JayAudioManager.Instance.Stop();
            MusicPlayer.Instance.Stop();
            JayAudioManager.Instance.SendOhOh();
            yield return new WaitForSeconds(1f);
            MusicPlayer.Instance.PlaySiren();
            Jay.Agent.enabled = false;
            BobCamera.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            MusicPlayer.Instance.Stop();
            var Jail = GameObject.Instantiate(JailPrefab, Jay.transform);
            yield return new WaitForSeconds(5f);
            GameOver(GameOverType.BobFail);
        }
        public void GameOver(GameOverType type)
        {
            TriggerForEnd.gameObject.SetActive(false);
            JayAudioManager.Instance.Stop();
            MainMenuManager.Instance.GameOver(type);
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

    public enum GameOverType
    {
        BobFail,
        TimeOut,
        TooMuch,
        TooLittle
    }
}