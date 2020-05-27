using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritState : MonoBehaviour
{
    public Sprite liveSprite;
    public Sprite deadSprite;
    public bool state = false;
    public bool change = false;
    private FollowAI follow;
    // Start is called before the first frame update
    void Start()
    {
        follow = gameObject.GetComponent<FollowAI>();
        gameObject.tag = "sapling";
        gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
        follow.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != change)
        {
            state = change;
            if (state == true)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = liveSprite;
                follow.enabled = true;
                gameObject.tag = "follower";
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
                follow.enabled = false;
                gameObject.tag = "sapling";
            }
        }
    }
}
