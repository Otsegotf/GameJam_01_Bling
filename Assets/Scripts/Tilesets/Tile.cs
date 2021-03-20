using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float TileSize = 4f;

    public Vector2Int TileId;

    public StateToggler N;

    public StateToggler S;

    public StateToggler E;

    public StateToggler W;

    public GameObject WallN;

    public GameObject WallS;

    public GameObject WallE;

    public GameObject WallW;

    public GameObject ISLE;

    public GameObject BORDERED;

    private TileStates _state;
    public TileStates State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            SetState(_state);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(TileStates state)
    {
        E.SetState(State.HasFlag(TileStates.E));
        S.SetState(State.HasFlag(TileStates.S));
        N.SetState(State.HasFlag(TileStates.N));
        W.SetState(State.HasFlag(TileStates.W));
    }
}
[Flags]
public enum TileStates
{
    None = 0,
    N = 1<<0,//2
    S = 1<<1,//4
    W = 1<<2,//6
    E = 1<<3,//8
    All = N|S|W|E
}

