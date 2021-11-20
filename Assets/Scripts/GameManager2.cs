using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public int width, height;
    public GameObject tile;
    public GameObject[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
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

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
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


                bool hasgrass = false;
                foreach (GameObject t in adjacent) 
                {
                    Tile2 curtile = t.GetComponent<Tile2>();
                    if (curtile.labels[curtile.type] == "Grass") 
                    {
                        hasgrass = true;
                        break;
                    }
                }

                

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
