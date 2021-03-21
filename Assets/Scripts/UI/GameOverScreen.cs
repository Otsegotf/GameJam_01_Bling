using System.Collections;
using UnityEngine.SceneManagement;
using GJgame;
using UnityEngine;
using System;

public class GameOverScreen : MonoBehaviour
{
    public TMPro.TMP_Text DifSet;

    public StringCollection[] Strings;

    private void Start()
    {
        var type = MainMenuManager.Instance.GameOverType;
        var actualStrings = Strings[(int)type];
        DifSet.text = actualStrings.Texts[UnityEngine.Random.Range(0,actualStrings.Texts.Length)];
    }
}

[Serializable]
public struct StringCollection
{
    public string[] Texts;
}
