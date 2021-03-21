using System.Collections;
using UnityEngine.SceneManagement;
using GJgame;
using UnityEngine;

public class MainMenuManager : Singleton<MainMenuManager>
{
    public int MainMenuScene = 1;

    public int GameOverScene = 2;

    public int PlayScene = 3;

    public int DifficultySetting = 0;

    public string GameOverText;

    public DefaultMovement Controls;

    private IEnumerator Start()
    {
        Controls = new DefaultMovement();
        Controls.Enable();
        yield return new WaitForSeconds(1f);
        LoadNewScene(MainMenuScene);
        Transition.Instance.SetState(false);
    }

    public void LoadPlayScene()
    {
        StartCoroutine(MainSceneToPlaySceneTransition());
    }

    public void GameOver(string text)
    {
        GameOverText = text;
        StartCoroutine(PlaySceneToGameOverTransition());
    }

    public void ToMainMenuFromGame()
    {
        StartCoroutine(PlaySceneToMainTransition());
    }

    public void ToMainMenuFromGGScreen()
    {
        StartCoroutine(GameOverSceneToMainMenuTransition());
    }

    public void LoadNewScene(int sceneId)
    {
        StartCoroutine(LoadingRoutine(sceneId));
    }

    private void UnloadScene(int sceneId)
    {
        SceneManager.UnloadSceneAsync(sceneId);
    }

    private IEnumerator PlaySceneToMainTransition()
    {
        while (Transition.Instance.CurrentTransitionState < 1)
        {
            Transition.Instance.SetState(true);
            yield return null;
        }

        UnloadScene(PlayScene); 
        
        yield return LoadingRoutine(MainMenuScene); 
        
        Transition.Instance.SetState(false);
    }

    private IEnumerator PlaySceneToGameOverTransition()
    {
        while (Transition.Instance.CurrentTransitionState < 1)
        {
            Transition.Instance.SetState(true);
            yield return null;
        }

        UnloadScene(PlayScene);

        yield return LoadingRoutine(GameOverScene);

        Transition.Instance.SetState(false);
    }

    private IEnumerator GameOverSceneToMainMenuTransition()
    {
        while (Transition.Instance.CurrentTransitionState < 1)
        {
            Transition.Instance.SetState(true);
            yield return null;
        }

        UnloadScene(GameOverScene);

        yield return LoadingRoutine(MainMenuScene);

        Transition.Instance.SetState(false);
    }

    private IEnumerator MainSceneToPlaySceneTransition()
    {
        while (Transition.Instance.CurrentTransitionState < 1)
        {
            Transition.Instance.SetState(true);
            yield return null;
        }

        UnloadScene(MainMenuScene);

        yield return LoadingRoutine(PlayScene);

        GameManager.Instance.Difficulty = DifficultySetting;
        GameManager.Instance.Restart();
    }

    private IEnumerator LoadingRoutine(int sceneId)
    {        
       
        var op = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        op.allowSceneActivation = true;
        while (!op.isDone)
            yield return null;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneId));
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
