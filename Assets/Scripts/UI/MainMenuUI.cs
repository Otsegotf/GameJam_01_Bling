using System.Collections;
using UnityEngine.SceneManagement;
using GJgame;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public bool IsPlayMenu;

    public TMPro.TMP_Text DifSet;
    private void Start()
    {
        if (IsPlayMenu)
        {
            MainMenuManager.Instance.Controls.UI.ToggleMenu.performed += ToggleMenu_performed;
            CloseMenu();
        }
    }

    private void OnDestroy()
    {
        if (IsPlayMenu && MainMenuManager.Instance)
            MainMenuManager.Instance.Controls.UI.ToggleMenu.performed -= ToggleMenu_performed;
    }

    private void ToggleMenu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 0f : 1f;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        MainMenuManager.Instance.LoadPlayScene();
    }

    public void ChangeDificulty(int change)
    {
        MainMenuManager.Instance.DifficultySetting += change;
        DifSet.text = MainMenuManager.Instance.DifficultySetting.ToString();
    }

    public void ExitGame()
    {
        gameObject.SetActive(false);
        MainMenuManager.Instance.Exit();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false); 
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenuFromPlay()
    {
        CloseMenu();
        MainMenuManager.Instance.ToMainMenuFromGame();     
    }

    public void ReturnToMainMenuFromGG()
    {
        CloseMenu();
        MainMenuManager.Instance.ToMainMenuFromGGScreen();
    }
}
