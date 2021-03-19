using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateToggler : MonoBehaviour
{
    public GameObject ToggledState;

    public GameObject DeactivatedState;

    public void SetState(bool state)
    {
        ToggledState.SetActive(state);
        DeactivatedState.SetActive(!state);
    }
}
