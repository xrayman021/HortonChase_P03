using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static int width = 16;
    public static int height = 10;
    public GameObject tile;
    public static GameObject[,] tiles;
    public int playerUnits;
    public int enemyUnits;
    public GameObject[] playerUnitTypes;
    public List<GameObject> activePlayerUnits;
    public GameObject[] enemyUnitTypes;
    public List<GameObject> activeEnemyUnits;
    public GameObject mouseOver;
    public GameObject currentlySelected;
    public static bool playerTurn = true;
    public Button endTurn;
    public Text displayTurn;


    void EndTurn()
    {
        playerTurn = false;
        displayTurn.text = "Enemy Turn";
        foreach (GameObject g in activeEnemyUnits) 
        {
            g.GetComponent<EnemyUnit>().canMove = true;
        }
    }

    List<GameObject> getAdjacent(int w, int h)
    {
        List<GameObject> adjacent = new List<GameObject>();
        if (w == 0 && h == 0) // upper left
        {
            adjacent.Add(tiles[w + 1, h]);
            adjacent.Add(tiles[w, h + 1]);
        }
        else if (w == 0 && h == height - 1) // lower left
        {
            adjacent.Add(tiles[w + 1, h]);
            adjacent.Add(tiles[w, h - 1]);
        }
        else if (w == width - 1 && h == 0) // upper right
        {
            adjacent.Add(tiles[w - 1, h]);
            adjacent.Add(tiles[w, h + 1]);
        }
        else if (w == width - 1 && h == height - 1) // lower right
        {
            adjacent.Add(tiles[w, h - 1]);
            adjacent.Add(tiles[w - 1, h]);
        }
        else if (h == 0) // top
        {
            adjacent.Add(tiles[w + 1, h]);
            adjacent.Add(tiles[w - 1, h]);
            adjacent.Add(tiles[w, h + 1]);
        }
        else if (h == height - 1) // bottom
        {
            adjacent.Add(tiles[w + 1, h]);
            adjacent.Add(tiles[w - 1, h]);
            adjacent.Add(tiles[w, h - 1]);
        }
        else if (w == 0) // left
        {
            adjacent.Add(tiles[w, h - 1]);
            adjacent.Add(tiles[w, h + 1]);
            adjacent.Add(tiles[w + 1, h]);
        }
        else if (w == width - 1) // right
        {
            adjacent.Add(tiles[w, h - 1]);
            adjacent.Add(tiles[w, h + 1]);
            adjacent.Add(tiles[w - 1, h]);
        }
        else
        {
            Debug.Log("w: " + w);
            Debug.Log("h: " + h);
            adjacent.Add(tiles[w + 1, h + 1]);
            adjacent.Add(tiles[w - 1, h + 1]);
            adjacent.Add(tiles[w - 1, h - 1]);
            adjacent.Add(tiles[w + 1, h - 1]);
        }
        return adjacent;
    }

    // Start is called before the first frame update
    void Start()
    {
        displayTurn.text = "Player Turn";
        endTurn.onClick.AddListener(EndTurn);
        tiles = new GameObject[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                tiles[w, h] = Instantiate(tile, new Vector3(w, 0, h), Quaternion.identity);
                Tile2 curtile = tiles[w, h].GetComponent<Tile2>();
                int type = Random.Range(0, curtile.costumes.Length);
                bool walk = true;
                if (type == 1)
                {
                    walk = false;
                }
                curtile.Initialize(type, walk);
            }
        }

        int passable = Random.Range(0,width);

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (w == passable) 
                {
                    Tile2 curtile = tiles[w, h].GetComponent<Tile2>();
                    curtile.Change(0, true);
                }
                

                List<GameObject> adjacent = new List<GameObject>();
                if (w == 0 && h == 0)
                {
                    adjacent.Add(tiles[w + 1, h]);
                    adjacent.Add(tiles[w, h + 1]);
                }
                else if (w == 0 && h == height - 1)
                {
                    adjacent.Add(tiles[w + 1, h]);
                    adjacent.Add(tiles[w, h - 1]);
                }
                else if (w == width - 1 && h == 0)
                {
                    adjacent.Add(tiles[w - 1, h]);
                    adjacent.Add(tiles[w, h + 1]);
                }
                else if (w == width - 1 && h == height - 1)
                {
                    adjacent.Add(tiles[w, h - 1]);
                    adjacent.Add(tiles[w - 1, h]);
                }
                else if (h == 0)
                {
                    adjacent.Add(tiles[w + 1, h]);
                    adjacent.Add(tiles[w - 1, h]);
                    adjacent.Add(tiles[w, h + 1]);
                }
                else if (h == height - 1)
                {
                    adjacent.Add(tiles[w + 1, h]);
                    adjacent.Add(tiles[w - 1, h]);
                    adjacent.Add(tiles[w, h - 1]);
                }
                else if (w == 0)
                {
                    adjacent.Add(tiles[w, h - 1]);
                    adjacent.Add(tiles[w, h + 1]);
                    adjacent.Add(tiles[w + 1, h]);
                }
                else if (w == width - 1)
                {
                    adjacent.Add(tiles[w, h - 1]);
                    adjacent.Add(tiles[w, h + 1]);
                    adjacent.Add(tiles[w - 1, h]);
                }
                else 
                {
                    adjacent.Add(tiles[w + 1, h + 1]);
                    adjacent.Add(tiles[w - 1, h + 1]);
                    adjacent.Add(tiles[w - 1, h - 1]);
                    adjacent.Add(tiles[w + 1, h - 1]);
                }




                bool haswalkable = false;
                foreach (GameObject t in adjacent) 
                {
                    Tile2 curtile = t.GetComponent<Tile2>();
                    if (curtile.walkable) 
                    {
                        haswalkable = true;
                    }
                }

                if (haswalkable==false) 
                {
                    foreach (GameObject t in adjacent)
                    {
                        Tile2 thetile = t.GetComponent<Tile2>();
                        thetile.Change(0, true);
                    }

                }



                

            }
        }

        foreach(GameObject unit in playerUnitTypes)
        {
            Debug.Log(tiles.Length);
            Tile2 randomTile = tiles[Random.Range(0, width), Random.Range(0, height/2)].GetComponent<Tile2>();
            GameObject currentUnit = Instantiate(unit, randomTile.transform.position, Quaternion.identity);
            randomTile.occupier = currentUnit;
            currentUnit.GetComponent<PlayerUnit>().location = randomTile;
            currentUnit.transform.Translate(0, 1.5f, 0);
            activePlayerUnits.Add(currentUnit);
        }

        for (int i = 0; i < enemyUnits; i++) 
        {
            foreach (GameObject unit in enemyUnitTypes)
            {
                Tile2 randomTile = tiles[Random.Range(0, width), Random.Range(height / 2, height)].GetComponent<Tile2>();
                while (randomTile.occupier != null)
                {
                    randomTile = tiles[Random.Range(0, width), Random.Range(height / 2, height)].GetComponent<Tile2>();
                }
                GameObject currentUnit = Instantiate(unit, randomTile.transform.position, Quaternion.identity);
                randomTile.occupier = currentUnit;
                currentUnit.GetComponent<EnemyUnit>().location = randomTile;
                currentUnit.transform.Translate(0, 1.5f, 0);
                activeEnemyUnits.Add(currentUnit);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mouseOver = hit.transform.gameObject;
        }
        else
        {
            mouseOver = null;
        }
        if(currentlySelected != null && currentlySelected.tag == "Tile")
        {
            currentlySelected = null;
        }
        if (Input.GetMouseButtonDown(0) && mouseOver != null && currentlySelected == null)
        {
            currentlySelected = mouseOver;

        }
        if (currentlySelected != null && currentlySelected.tag == "Hero" && Input.GetMouseButtonDown(0))
        {
            Debug.Log("hero selected");
            if(mouseOver.tag == "Tile")
            {
                Debug.Log("tile selected");
                PlayerUnit p = currentlySelected.GetComponent<PlayerUnit>();
                //p.destination = mouseOver.transform.position;
                Tile2 t = mouseOver.GetComponent<Tile2>();
                t.occupier = currentlySelected;
                p.Move(t);
                currentlySelected = null;
            }
            if (mouseOver.tag == "Enemy")
            {

            }
        }

        if(playerTurn == false)
        {
            foreach(GameObject enemy in activeEnemyUnits)
            {
                enemy.GetComponent<EnemyUnit>().Move();
            }
            playerTurn = true;
            foreach(GameObject player in activePlayerUnits)
            {
                player.GetComponent<PlayerUnit>().canMove = true;
            }
        }


    }



}
