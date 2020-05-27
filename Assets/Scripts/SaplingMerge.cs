using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//interactions fixed 

public class SaplingMerge : MonoBehaviour
{
    private SaplingPickup saplingPickup;
    private MovementScript mv;
    private Coroutine mergingCoroutine;

    public float startMergeChargeTime, startMergeCooldown;
    public int maxMerges = 3;

    private float mergeChargeTime, mergeCooldown;

    private float speed = 10f;
    private int followers = 0;

    private string mergeState;
    private int followersMerged = 0;
    private int currentFollower = 0;
    
    
    void Start()
    {
        
        saplingPickup = GetComponent<SaplingPickup>();
        mv = GetComponent<MovementScript>();
       
        mergeState = "active";
        mergeChargeTime = startMergeChargeTime;
        mergeCooldown = startMergeCooldown;
    }

    void Update()
    {

        switch (mergeState)
        {
            case "active":
                PerformActiveMerge();
                break;
            case "inactive":
                PerformInactiveMerge();
                break;
        }

    }

    private void PerformActiveMerge()
    {
        
        if (Input.GetButton("Dash"))
        {
            if (mergeChargeTime > 0)
            {
                mergingCoroutine = StartCoroutine(Merging());
                mv.setAllowMove(false);
               
                if (saplingPickup.GetFollowers().Count > followers)
                {
                    currentFollower = 0;
                    List<GameObject> saplingFollowers = saplingPickup.GetFollowers();
                    if (saplingFollowers.Count > 0)
                    {
                           
                        while (currentFollower < saplingPickup.GetFollowers().Count)
                        {
                            Rigidbody2D saplingRb = saplingPickup.GetFollowers()[currentFollower].GetComponent<Rigidbody2D>();
                            SpriteRenderer saplingRenderer = saplingPickup.GetFollowers()[currentFollower].GetComponent<SpriteRenderer>();

                            saplingRenderer.color = Color.red;
                            saplingPickup.GetFollowers()[currentFollower].GetComponent<FollowAI>().enabled = false;
                            Transform saplingTransform = saplingPickup.GetFollowers()[currentFollower].GetComponent<Transform>();
                            Transform life = GameObject.FindGameObjectWithTag("tree").GetComponent<Transform>();

                            saplingTransform.position = Vector3.MoveTowards(saplingTransform.position, life.position, speed * Time.deltaTime);
                            if (Mathf.Abs(saplingTransform.position.x - life.position.x) < 0.5f && Mathf.Abs(saplingTransform.position.y - life.position.y) < 0.5f)
                            {
                                saplingTransform.position = life.position;
                                GameObject saplingToRemove = saplingPickup.GetFollowers()[currentFollower];
                                Destroy(saplingToRemove);
                                saplingPickup.GetFollowers().RemoveAt(currentFollower);
                                followersMerged++;
                                if (followersMerged == maxMerges)
                                {
                                    mergeChargeTime = 0;
                                }
                                mv.setScale(0.02f, 0.02f, 0.02f);
                            }
                            else
                            {
                                currentFollower++;
                            }
                        }
                    }
                }
               
                mergeChargeTime -= Time.deltaTime;
            }
            
        }
        if (Input.GetButtonUp("Dash") || mergeChargeTime <= 0)
        {
            mergeState = "inactive";
            followersMerged = 0;

            StopAllCoroutines();
            SpriteRenderer lifeRenderer = GameObject.FindGameObjectWithTag("tree").GetComponent<SpriteRenderer>();
            lifeRenderer.color = Color.white;

            mergeChargeTime = startMergeChargeTime;
            mv.setAllowMove(true);
            foreach(GameObject follower in saplingPickup.GetFollowers())
            {
                follower.GetComponent<FollowAI>().enabled = true;
                SpriteRenderer followerRenderer = follower.GetComponent<SpriteRenderer>();
                followerRenderer.color = Color.white;
            }

        }
    }

    
    private void PerformInactiveMerge()
    {
        mv.setAllowMove(true);
        if (mergeCooldown > 0f)
        {
            mergeCooldown -= Time.deltaTime;
        }
        else
        {
            mergeCooldown = startMergeCooldown;
            mergeState = "active";
        }
    }

    IEnumerator Merging()
    {
        SpriteRenderer lifeRenderer = GameObject.FindGameObjectWithTag("tree").GetComponent<SpriteRenderer>();
        lifeRenderer.color = Color.red;
        yield return new WaitForSeconds(startMergeChargeTime);
        lifeRenderer.color = Color.white;
    }


}
