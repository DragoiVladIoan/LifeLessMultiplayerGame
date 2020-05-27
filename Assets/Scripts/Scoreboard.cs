using Assets.SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    private NetworkClient networkClient;

    private GameObject mainCamera;
    public Text Player1Score;
    public Text Player2Score;
    private GameObject death;
    private GameObject life;
    private bool playersEnabled = false;
    private bool activeScene;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        activeScene = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (networkClient.GetNumberOfPlayers() == 2)
        {
            if (!playersEnabled)
            {
                death = GameObject.FindGameObjectWithTag("bulldozer");
                life = GameObject.FindGameObjectWithTag("tree");
                playersEnabled = true;
            }

            //Debug.Log(Player1.GetComponent<Points>().points.ToString());
            if (death && life)
            {
                Player1Score.text = death.GetComponent<Points>().points.ToString();
                Player2Score.text = life.GetComponent<Points>().points.ToString();
            }
        }

    }

    public GameObject GetDeath()
    {
        return death;
    }

    public GameObject GetLife()
    {
        return life;
    }
}
