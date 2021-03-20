using GJgame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float TileSize = 4f;

    public Vector2Int TileId;

    public StateToggler Ntoggle;

    public StateToggler Stoggle;

    public StateToggler Etoggle;

    public StateToggler Wtoggle;

    public AisleConstructor N;

    public AisleConstructor S;

    public AisleConstructor E;

    public AisleConstructor W;

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

    public void UpdateAisleState(ShopItemType type)
    {
        Etoggle.SetState(_state.HasFlag(TileStates.E));
        Ntoggle.SetState(_state.HasFlag(TileStates.N));
        Wtoggle.SetState(_state.HasFlag(TileStates.W));
        Stoggle.SetState(_state.HasFlag(TileStates.S));
        E.SetAisleState(_state.HasFlag(TileStates.E), _state.HasFlag(TileStates.N), _state.HasFlag(TileStates.S), type);
        W.SetAisleState(_state.HasFlag(TileStates.W), _state.HasFlag(TileStates.S), _state.HasFlag(TileStates.N), type);
        S.SetAisleState(_state.HasFlag(TileStates.S), _state.HasFlag(TileStates.E), _state.HasFlag(TileStates.W), type);
        N.SetAisleState(_state.HasFlag(TileStates.N), _state.HasFlag(TileStates.W), _state.HasFlag(TileStates.E), type);
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

