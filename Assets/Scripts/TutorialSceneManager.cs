using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour
{
    private bool activeScene;
    public string character;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeScene)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                activeScene = false;
                StartCoroutine("LoadMainMenu"); 
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                activeScene = false;
                if (character == "death")
                {
                    StartCoroutine("LoadLifeTutorial");
                }
                else
                {
                    StartCoroutine("LoadDeathTutorial");
                }
            }
        }
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

    }

    IEnumerator LoadLifeTutorial()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync("LifeTutorial", LoadSceneMode.Single);
    }

    IEnumerator LoadDeathTutorial()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync("DeathTutorial", LoadSceneMode.Single);
    }
}
