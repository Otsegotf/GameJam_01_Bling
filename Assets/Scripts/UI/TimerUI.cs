using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : Singleton<TimerUI>
{
    public TMPro.TMP_Text Text;
    private void Update()
    {
        var time = Mathf.RoundToInt(GameManager.Instance.LevelTime);
        var min = time / 60 < 10 ? "0" + (time / 60).ToString() : (time / 60).ToString();
        var sec = time % 60 < 10 ? "0" + (time % 60).ToString() : (time % 60).ToString();
        Text.text = $"{min}:{sec}";
    }
}
