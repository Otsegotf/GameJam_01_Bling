using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobTimerUI : Singleton<BobTimerUI>
{
    public TMPro.TMP_Text Text;

    public GameObject BobTimerObj;
    private void Update()
    {
        var jay = GameManager.Instance.Jay;

        if (jay.CurrentState == BobState.Stealing)
        {
            BobTimerObj.SetActive(true);
            var time = Mathf.Clamp(jay._curActionCd, 0, 99);
            Text.text = $"{Mathf.RoundToInt(time)}";
        }
        else
            BobTimerObj.SetActive(false);

    }
}
