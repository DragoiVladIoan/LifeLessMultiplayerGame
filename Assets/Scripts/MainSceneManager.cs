using Assets.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private NetworkClient networkClient;

    //references for disabling/enabling scripts
    private Scoreboard scoreboard;
    private ObjectSpawner objSpawner;
    private GameObject life, death;

    public Text Title;

    private float timeLeft = 30, counter = 1.5f, connectingTime = 0;
    private int dotCounter = 0, defaultFontSize, clientsDisconnected = 0;
    private bool roundStarted = false, coroutineStarted = false, lastCoroutineStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        scoreboard = GameObject.Find("Main Camera").GetComponent<Scoreboard>();
        objSpawner = GameObject.Find("Main Camera").GetComponent<ObjectSpawner>();


        objSpawner.enabled = false;

        Title.text = "WAITING FOR ANOTHER CONNECTION";
        defaultFontSize = Title.fontSize;
        Title.fontSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (networkClient.GetNumberOfPlayers() == 1 && life == null)
        {
            life = GameObject.FindGameObjectWithTag("tree");
            life.GetComponent<MovementScript>().enabled = false;
        } 

        if (networkClient.GetNumberOfPlayers() == 2)
        {
            
            if (!roundStarted && !coroutineStarted)
            {
                if (death == null)
                {
                    life = GameObject.FindGameObjectWithTag("tree");
                    death = GameObject.FindGameObjectWithTag("bulldozer");
                    death.GetComponent<MovementScript>().enabled = false;
                    death.GetComponent<Dash>().enabled = false;
                }
                Title.fontSize = defaultFontSize;
                StartCoroutine("RoundStarting");
                coroutineStarted = true;
            }

            else if (roundStarted)
            {
                timeLeft -= Time.deltaTime;
                if (counter < 1)
                {
                    counter += Time.deltaTime;
                }
                if (timeLeft < 0 && clientsDisconnected == 0)
                {
                    if (!lastCoroutineStarted)
                    {
                        if (scoreboard.GetDeath().GetComponent<Points>().points > scoreboard.GetLife().GetComponent<Points>().points)
                        {
                            lastCoroutineStarted = true;
                            StartCoroutine("DeathWinningScene");

                        }
                        else
                        {
                            lastCoroutineStarted = true;
                            StartCoroutine("LifeWinningScene");
                        }
                    }
                    Title.text = "0";
                }
                if (counter > 1)
                {
                    Title.text = ((int)timeLeft).ToString();
                    counter = 0;
                }
            }


        }
        else
        {
            connectingTime += Time.deltaTime;
            if (connectingTime > 0.5 && dotCounter < 3)
            {
                Title.text = Title.text.ToString() + ".";
                dotCounter++;
                connectingTime = 0;
            }
            else if (connectingTime > 0.5)
            {
                Title.text = "WAITING FOR ANOTHER CONNECTION";
                connectingTime = 0;
                dotCounter = 0;
            }

        }
    }

    private void EnableScripts(bool enable)
    {
        objSpawner.enabled = enable;
        life.GetComponent<MovementScript>().enabled = enable;
        death.GetComponent<MovementScript>().enabled = enable;
        death.GetComponent<Dash>().enabled = enable;
    }

    IEnumerator RoundStarting()
    {
        for (int i = 3; i > 0; i--)
        {
            Title.text = "ROUND STARTING IN " + i;
            yield return new WaitForSeconds(1.0f);
        }

        Title.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        EnableScripts(true);
        roundStarted = true;
        Title.text = timeLeft.ToString();


    }

    IEnumerator DeathWinningScene()
    {
        yield return new WaitForSeconds(0.1f);
        if (clientsDisconnected < 2)
        {
            Debug.Log(clientsDisconnected);
            HaltConnection();
            clientsDisconnected++;
        }
        SceneManager.LoadSceneAsync("DeathWinningScene", LoadSceneMode.Single);

    }

    IEnumerator LifeWinningScene()
    {
        yield return new WaitForSeconds(0.1f);
        if (clientsDisconnected < 2)
        {
            HaltConnection();
            clientsDisconnected++;
        }
        SceneManager.LoadSceneAsync("LifeWinningScene", LoadSceneMode.Single);
        
    }

    private void HaltConnection()
    {
        string id = NetworkClient.clientId;
        GameObject network = GameObject.Find("Network");
        NetworkClient client = network.GetComponent<NetworkClient>();
        NetworkIdentity ni = client.GetServerObjects()[id];
        GameObject[] saplings = GameObject.FindGameObjectsWithTag("sapling");
        foreach(GameObject sap in saplings)
        {
            sap.GetComponent<FollowAI>().enabled = false;
        }
        ni.GetSocket().Emit("disconnect", new JSONObject(JsonUtility.ToJson(id)));
    }
}
