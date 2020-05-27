using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerCollision : MonoBehaviour
{
    GameObject tree;
    SaplingPickup saplingPickup;
    private MovementScript mv;
    Points points;

    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.FindGameObjectsWithTag("tree")[0];
        saplingPickup = tree.GetComponent<SaplingPickup>();
        mv = GetComponent<MovementScript>();
        points = gameObject.GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "follower")
        {
            foreach (GameObject sapling in saplingPickup.GetFollowers().ToArray())
                if(sapling.GetInstanceID() == collision.gameObject.GetInstanceID())
                {
                    saplingPickup.GetFollowers().Remove(sapling);
                    sapling.tag = "sapling";
                    sapling.GetComponent<FollowAI>().enabled = false;
                    sapling.GetComponent<SpiritState>().change = false;
                    points.points += points.kill_value;
                }
        }   
    }
}
