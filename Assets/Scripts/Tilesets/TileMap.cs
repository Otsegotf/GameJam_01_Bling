using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Tile Prefab;

    public Vector2Int Size;

    public Transform Target;

    public int Seed = 0;

    private Tile[,] _tiles;

    private Dictionary<Tile, List<Tile>> _neighboors;

    public float Cooldown = 5;

    public int MaxBreaks = 1;

    public static TileMap Instance;

    public ShopItemType AvailableItems;

    // Start is called before the first frame update
    public IEnumerator GenerateMap()
    {
        var itemCount = (int)AvailableItems.Count();
        var biomeWidth = 0;
        var isYLong = false;
        if (Size.x > Size.y)
        {
            biomeWidth = Size.x / itemCount;
        }
        else
        {
            isYLong = true;
            biomeWidth = Size.y / itemCount;
        }

        Random.InitState(Seed);
        _tiles = new Tile[Size.x, Size.y];
        _neighboors = new Dictionary<Tile, List<Tile>>();
        for (int i = 0; i < Target.childCount; i++)
        {
            GameObject.Destroy(Target.GetChild(i).gameObject);
        }
        for (int i = 0; i < Size.x; i++)
        {
            for (int z = 0; z < Size.y; z++)
            {
                var newTile = GameObject.Instantiate(Prefab);
                _neighboors[newTile] = new List<Tile>();

                var newState = (TileStates)Random.Range(3, 8);
                _tiles[i, z] = newTile;
                newTile.TileId = new Vector2Int(i, z);
                newTile.State = newState;
                newTile.transform.SetParent(Target);
                newTile.transform.localPosition = new Vector3(i, 0, z) * newTile.TileSize;

                newTile.WallW.SetActive(i == 0);
                newTile.WallE.SetActive(i == Size.x - 1);
                newTile.WallN.SetActive(z == Size.y - 1);
                if (z == 0 && i <= 1)
                {
                    newTile.State &= ~TileStates.S;
                }
                newTile.WallS.SetActive(z == 0 && i > 1);
                yield return null;
            }
        }
        yield return StartCoroutine(CheckConnectivity(_tiles[0, 0]));
        for (int xSize = 0; xSize < Size.x; xSize++)
        {
            for (int zSize = 0; zSize < Size.y; zSize++)
            {
                var typeTest = 0;
                var putLabel = true;
                if (isYLong)
                {
                    typeTest = zSize / biomeWidth;
                    putLabel = zSize % biomeWidth == 0;
                }
                else
                {
                    typeTest = xSize / biomeWidth;
                    putLabel = xSize % biomeWidth == 0;
                }
                var typedType = (ShopItemType)(1 << Mathf.Clamp(typeTest, 0, 6));
                if (putLabel)
                {
                    var labelPrefab = GameManager.Instance.LabelLibrary.GetLabel(typedType);

                    GameObject.Instantiate(labelPrefab, _tiles[xSize, zSize].transform);
                }

                _tiles[xSize, zSize].UpdateAisleState(typedType);
                yield return null;
            }
        }
    }
    private IEnumerator CheckConnectivity(Tile startPoint)
    {
        var total = Size.x * Size.y;
        var openList = new Queue<Tile>();
        openList.Enqueue(startPoint);
        var closedList = new HashSet<Tile>();
        var bordered = new HashSet<Tile>();
        while (closedList.Count < total)
        {
            yield return null;
            var currentBreak = 0;
            foreach (var item in bordered)
            {
                GetAdjasent(item, out var connected, out var disconnected);
                var allConnected = true;
                for (int i = 0; i < disconnected.Count; i++)
                {
                    if (closedList.Contains(disconnected[i]))
                        continue;
                    allConnected = false;
                    var id = item.TileId;
                    var disId = disconnected[i].TileId;
                    if (id.x > disId.x)
                    {
                        item.State &= ~TileStates.W;
                        disconnected[i].State &= ~TileStates.E;
                    }
                    if (id.x < disId.x)
                    {
                        item.State &= ~TileStates.E;
                        disconnected[i].State &= ~TileStates.W;
                    }
                    if (id.y > disId.y)
                    {
                        item.State &= ~TileStates.S;
                        disconnected[i].State &= ~TileStates.N;
                    }
                    if (id.y < disId.y)
                    {
                        item.State &= ~TileStates.N;
                        disconnected[i].State &= ~TileStates.S;
                    }
                    currentBreak++;
                }
                if (allConnected)
                    item.BORDERED.SetActive(false);
                else
                {
                    item.ISLE.SetActive(true);
                    openList.Enqueue(item);
                }
                if (currentBreak > MaxBreaks)
                    break;
            }
            foreach (var item in openList)
            {
                item.BORDERED.SetActive(false);
                bordered.Remove(item);
            }
            while (openList.Count > 0)
            {
                var tracked = openList.Dequeue();
                closedList.Add(tracked);
                tracked.ISLE.SetActive(false);
                GetAdjasent(tracked, out var connected, out var disconnected);
                var filteredConnected = new List<Tile>();
             
                foreach (var item in connected)
                {
                    if (!closedList.Contains(item))
                    {
                        filteredConnected.Add(item);
                    }
                }

                foreach (var item in filteredConnected)
                {
                    tracked.ISLE.SetActive(true);
                    openList.Enqueue(item);
                }
                var allConnected = true;
                foreach (var item in disconnected)
                {
                    if (!closedList.Contains(item))
                    {
                        allConnected = false;
                    }
                }
                if (!allConnected)
                {
                    tracked.BORDERED.SetActive(true);
                    bordered.Add(tracked);
                }
            }
        }
    }

    private void GetAdjasent(Tile starting, out List<Tile> connected, out List<Tile> disconected)
    {
        connected = new List<Tile>();
        disconected = new List<Tile>();
        var id = starting.TileId;
        if (id.x > 0)
        {
            var tile = _tiles[id.x - 1, id.y];
            if (tile.State.HasFlag(TileStates.E) || starting.State.HasFlag(TileStates.W))
            {
                disconected.Add(tile);
            }
            else
            {
                connected.Add(tile);
            }
        }
        if (id.y > 0)
        {
            var tile = _tiles[id.x, id.y - 1];

            if (tile.State.HasFlag(TileStates.N) || starting.State.HasFlag(TileStates.S))
            {
                disconected.Add(tile);
            }
            else
            {
                connected.Add(tile);
            }
        }
        if (id.x < _tiles.GetLength(0) - 1)
        {
            var tile = _tiles[id.x + 1, id.y];
            if (tile.State.HasFlag(TileStates.W) || starting.State.HasFlag(TileStates.E))
            {
                disconected.Add(tile);
            }
            else
            {
                connected.Add(tile);
            }
        }
        if (id.y < _tiles.GetLength(1) - 1)
        {
            var tile = _tiles[id.x, id.y + 1];

            if (tile.State.HasFlag(TileStates.S) || starting.State.HasFlag(TileStates.N))
            {
                disconected.Add(tile);
            }
            else
            {
                connected.Add(tile);
            }
        }
    }

}
