using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Objects;

    private void OnEnable()
    {
        var active = Random.Range(0, Objects.Length);
        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(active == i);
        }
    }
}
