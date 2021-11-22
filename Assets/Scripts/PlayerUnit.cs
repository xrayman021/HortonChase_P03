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
        destination = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canMove);
        if (Vector3.Distance(this.transform.position, destination) > 1.5f)
        {
            //Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
            transform.LookAt(destination);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.Translate(0, 0, speed * Time.deltaTime);
            

        }
    }

    public void Move(Tile2 newLocation)
    {
        Debug.Log(GameManager2.playerTurn);
        if (canMove && Vector3.Distance(this.transform.position, newLocation.transform.position) <= range && GameManager2.playerTurn)
        {
            Debug.Log("moving");
            Debug.Log(newLocation);
            location.occupier = null;
            location = newLocation;
            location.occupier = this.gameObject;
            canMove = false;
            destination = newLocation.transform.position;
            Debug.Log(destination);
        }
        else
        {
            Debug.Log("Can't move");
        }
    }

    public void Attack(GameObject opponent)
    {
        EnemyUnit enemy = opponent.GetComponent<EnemyUnit>();
        enemy.health -= 1;
    }


}
