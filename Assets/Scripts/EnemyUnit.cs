using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{ 
    public Tile2 location;
    public int range;
    public bool canMove = true;
    public Vector3 destination;
    public float speed;
    GameObject[,] tiles = GameManager2.tiles;
    int height = GameManager2.height;
    int width = GameManager2.width;
    public float attack_player_at = 4;
    public int maxHealth = 3;
    public int health;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        destination = this.transform.position;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, destination) > 1.5f)
        {
            //Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
            transform.LookAt(destination);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.Translate(0, 0, speed * Time.deltaTime);

        }
        if(health <= 0)
        {
            Debug.Log("Enemy Died");
            Destroy(this.gameObject);

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




    public GameObject ClosestObjectWithTag(string tag)
    {
        // returns the closest GameObject to you with a given tag
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        if (objects.Length != 0)
        {
            float closest_dist = Mathf.Infinity;
            int closest_index = 0;
            int index = 0;
            foreach (GameObject obj in objects)
            {
                if (obj != null && this.gameObject != null)
                {
                    float dist = Vector3.Distance(this.transform.position, obj.transform.position);
                    if (dist < closest_dist)
                    {
                        closest_dist = dist;
                        closest_index = index;
                    }
                }
                index += 1;

            }
            return objects[closest_index];
        }
        else
        {
            return null;
        }

    }





    public void Move()
    {
        List<GameObject> adj = getAdjacent((int)location.transform.position.x, (int)location.transform.position.z);
        int count = 0;
        foreach(GameObject g in adj)
        {
            Tile2 t = g.GetComponent<Tile2>();
            if(t.occupier != null)
            {
                count += 1;
            }
        }
        if(count == adj.Count)
        {
            canMove = false;
            Debug.Log("No Where To GO!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            GameObject closest = ClosestObjectWithTag("Hero");
            Tile2 newLocation;

            if (Vector3.Distance(transform.position, closest.transform.position) < attack_player_at)
            {
                Debug.Log("Chasing the player");
                GameObject best = adj[0];
                float best_dist = Vector3.Distance(adj[0].transform.position, closest.transform.position);
                foreach (GameObject t in adj)
                {
                    if (Vector3.Distance(t.transform.position, closest.transform.position) < best_dist)
                    {
                        best = t;
                    }
                }
                newLocation = best.GetComponent<Tile2>();
                if (newLocation.occupier != null)
                {
                    GameObject choice = adj[Random.Range(0, adj.Count)];
                    newLocation = choice.GetComponent<Tile2>();
                    while (newLocation.occupier != null)
                    {
                        choice = adj[Random.Range(0, adj.Count)];
                        newLocation = choice.GetComponent<Tile2>();
                    } 
                }

            }
            else
            {
                
                GameObject choice = adj[Random.Range(0, adj.Count)];
                newLocation = choice.GetComponent<Tile2>();
                while (newLocation.occupier != null)
                {
                    choice = adj[Random.Range(0, adj.Count)];
                    newLocation = choice.GetComponent<Tile2>();
                }
            }

           

            if (canMove && Vector3.Distance(this.transform.position, newLocation.transform.position) <= range && GameManager2.playerTurn == false)
            {
                
                location.occupier = null;
                location = newLocation;
                location.occupier = this.gameObject;
                canMove = false;
                destination = newLocation.transform.position;
            }
            else
            {
                Debug.Log("Can't move");
            }
        }
        
    }

    public void Attack(GameObject opponent)
    {
        if (GameManager2.playerTurn==false && canMove) 
        {
            PlayerUnit unit = opponent.GetComponent<PlayerUnit>();
            unit.health -= 1;
            healthBar.SetHealth(health);
            canMove = false;
        }
        
    }

}
