using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile2 : MonoBehaviour
{
    public int type;
    public GameObject[] costumes;
    public string[] labels;
    public bool walkable;
    public GameObject occupier = null;
    private GameObject currentcostume;


    public void Initialize(int newType, bool newWalkable)
    {
        type = newType;
        walkable = newWalkable;
        currentcostume = Instantiate(costumes[type], this.transform.position, this.transform.rotation);
    }

    public void Change(int newType, bool newWalkable)
    {
        type = newType;
        walkable = newWalkable;
        Destroy(currentcostume);
        currentcostume = Instantiate(costumes[type], this.transform.position, this.transform.rotation);
    }

}
