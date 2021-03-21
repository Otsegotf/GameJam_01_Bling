using System.Collections;
using UnityEngine.SceneManagement;
using GJgame;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TMPro.TMP_Text DifSet;
    private void Start()
    {
        DifSet.text = MainMenuManager.Instance.GameOverText;
    }
}
