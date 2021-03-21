using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : Singleton<Transition>
{
    public Image TransitionImage;

    public GameObject LoadingText;

    public float TransitionSpeed = 1;

    private bool _currentState = true;

    private float _currentTransition = 1;
    
    public float CurrentTransitionState
    {
        get { return _currentTransition; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetState(bool state)
    {
        if (state != _currentState)
            TransitionImage.fillMethod = (Image.FillMethod)Random.Range(0, 5);
        _currentState = state;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState && _currentTransition < 1)
        {
            _currentTransition += Time.deltaTime / TransitionSpeed;
        }
        if (!_currentState && _currentTransition > 0)
        {
            _currentTransition -= Time.deltaTime / TransitionSpeed;
        }

        _currentTransition = Mathf.Clamp01(_currentTransition);

        LoadingText.SetActive(_currentTransition == 1);
        TransitionImage.fillAmount = _currentTransition;
    }
}
