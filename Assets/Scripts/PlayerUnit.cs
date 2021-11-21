using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public Tile2 location;
    public int range;
    public bool canMove = true;
    public Vector3 destination;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, destination) > 1)
        {
            Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
        }
    }

    public void Move(Tile2 newLocation)
    {
        location = newLocation;
        canMove = false;
        destination = newLocation.transform.position;
    }
}
