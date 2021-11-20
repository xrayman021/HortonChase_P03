using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    public UnitManager UnitManager;

    //public GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateGrid();
        /*Tile playerTile;
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
        Debug.Log(allTiles.Length);
        playerTile = allTiles[Random.Range(0, allTiles.Length-1)].GetComponent<Tile>();
        while (true)
        {
            playerTile = allTiles[Random.Range(0, allTiles.Length-1)].GetComponent<Tile>();
            if(playerTile.transform.position.y <= 5 && playerTile.Walkable)
            {
                break;
            }
        }*/
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Hero");
        //player = Instantiate(UnitManager.SelectedHero.gameObject, playerTile.transform.position, Quaternion.identity);
        foreach (GameObject P in Players)
        {
            P.transform.Translate(0, 0, -0.5f);
            P.transform.Rotate(90, 0, 0);
        }


        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        /*foreach(GameObject E in Enemies)
        {
            E.transform.Translate(0, 0, -0.5f);
            E.transform.Rotate(90, 0, 0);
        }*/
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y} {0}";

                
                spawnedTile.Init(x,y);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }


        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < Mathf.Ceil(_width / 2) && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
