using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile2 : MonoBehaviour
{
    public int type;
    public GameObject[] costumes;
    public string[] labels;
    public bool walkable;
    public GameObject occupier;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(costumes[type]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
