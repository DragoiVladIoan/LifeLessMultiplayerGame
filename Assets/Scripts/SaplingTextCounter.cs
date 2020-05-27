using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaplingTextCounter : MonoBehaviour
{
    private GameObject mainCamera;
    private ObjectSpawner objectSpawner;
    public Text saplingCounter;
    private string basicText;
    private int followerSize;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        objectSpawner = mainCamera.GetComponent<ObjectSpawner>();
        basicText = saplingCounter.text;
    }

    // Update is called once per frame
    void Update()
    {
        followerSize = objectSpawner.getSpawns().ToArray().Length;
        saplingCounter.text = basicText + followerSize.ToString();
    }
}
