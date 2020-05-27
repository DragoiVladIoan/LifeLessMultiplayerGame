using Assets.SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private NetworkClient networkClient;

    [SerializeField]
    private int seed;

    public GameObject[] interactiveObjects;
    public float CooldownTime;
    public int spawnLimit;
    private Vector2 screenBounds;
    public List<GameObject> spawns = new List<GameObject>();
    private bool waveEnabled = true;

    public List<GameObject> getSpawns()
    {
        return spawns;
    }

    // Start is called before the first frame update
    void Start()
    {

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        if (seed != 0)
        {
            if (waveEnabled)
            {
                StartCoroutine(objectWave());
                waveEnabled = false;
            }
        }
        else if (networkClient.GetRandomSpawnSeed() != -1)
        {
            if (waveEnabled && networkClient.GetNumberOfPlayers() == 2)
            {
                Random.InitState(networkClient.GetRandomSpawnSeed());
                StartCoroutine(objectWave());
                waveEnabled = false;
            }
        }
    }

    private void spawnObject()
    {
        if (spawns.Count < spawnLimit)
        {
            int objectSelected = Random.Range(0, interactiveObjects.Length);
            GameObject obj = Instantiate(interactiveObjects[objectSelected]) as GameObject;
            spawns.Add(obj);
            obj.transform.position = new Vector3(Random.Range(-screenBounds.x + 0.5f, screenBounds.x - 0.5f), Random.Range(-screenBounds.y + 0.5f, screenBounds.y - 0.5f), 0f);
     
        }
    }

    IEnumerator objectWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(CooldownTime);
            spawnObject();
        }

    }
}
