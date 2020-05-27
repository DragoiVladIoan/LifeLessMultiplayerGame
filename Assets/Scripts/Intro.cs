using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject timer;
    public GameObject title;
    public GameObject life;
    public GameObject death;
    public GameObject sapling;
    public GameObject round1background;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectsWithTag("player1")[0];
        player2 = GameObject.FindGameObjectsWithTag("player2")[0];
        timer = GameObject.FindGameObjectsWithTag("timer")[0];
        title = GameObject.FindGameObjectsWithTag("title")[0];
        life = GameObject.FindGameObjectsWithTag("tree")[0];
        death = GameObject.FindGameObjectsWithTag("bulloser")[0];
        sapling = GameObject.FindGameObjectsWithTag("sapling")[0];
        round1background = GameObject.FindGameObjectsWithTag("round1")[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("f"))
        {
            active = true;
            if (active)
            {
                player1.SetActive(true);
                player2.SetActive(true);
                timer.SetActive(true);
                title.SetActive(true);
                life.SetActive(true);
                death.SetActive(true);
                sapling.SetActive(true);
                round1background.SetActive(true);
                this.enabled = false;
            }

        }
        else
        {
            player1.SetActive(false);
            player2.SetActive(false);
            timer.SetActive(false);
            title.SetActive(false);
            life.SetActive(false);
            death.SetActive(false);
            sapling.SetActive(false);
            round1background.SetActive(false);
            this.enabled = true;

        }
    }

}
