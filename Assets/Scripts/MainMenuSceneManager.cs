using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    public RectTransform startText; // deathTutorialText, lifeTutorialText;
    public GameObject death, life;
    bool textSizeUp, textSizeDown;
    float period = 0.5f, nextActionOnText = 0.0f, textAdjustmentSize = 0.003f, timer=0.0f;
    bool init;

    //public int activeElement;
    //public GameObject[] menuOptions;
    //public GameObject menuObject;

    void Awake()
    {
        textSizeUp = true;
        //menuObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!init)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                init = true;
                StartCoroutine("LoadLevel");
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                init = true;
                StartCoroutine("LoadDeathTutorial");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                init = true;
                StartCoroutine("LoadLifeTutorial");
            }

            if (textSizeUp)
            {
                if (timer < period)
                {
                    startText = AdjustTextSize(startText, true);
                    //deathTutorialText = AdjustTextSize(deathTutorialText, true);
                    //lifeTutorialText = AdjustTextSize(lifeTutorialText, true);
                    //deathTutorialText.fontSize += 1;
                    //lifeTutorialText.fontSize += 1;  
                }
                else
                {
                    timer = 0.0f;
                    textSizeUp = false;
                }
               
            }
            else
            {
                if (timer < period)
                {
                    startText = AdjustTextSize(startText, false);
                    //deathTutorialText = AdjustTextSize(deathTutorialText, false);
                   // lifeTutorialText = AdjustTextSize(lifeTutorialText, false);
                }
                else
                {
                    timer = 0.0f;
                    textSizeUp = true;
                }
            }
        }
       
    }

    private RectTransform AdjustTextSize(RectTransform text, bool increase)
    {
        if (increase)
        {
            text.localScale = new Vector3(text.localScale.x + textAdjustmentSize, text.localScale.y + textAdjustmentSize);
            return text;
        }

        text.localScale = new Vector3(text.localScale.x - textAdjustmentSize, text.localScale.y - textAdjustmentSize);
        return text;

    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);

    }

    IEnumerator LoadDeathTutorial()
    {
        //death.GetComponent<SpriteRenderer>().color = Color.black;
        //GameObject deathObj = Instantiate(death) as GameObject;
        death.transform.position = new Vector3(death.transform.position.x, death.transform.position.y, -0.4f);
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadSceneAsync("DeathTutorial", LoadSceneMode.Single);

    } 

    IEnumerator LoadLifeTutorial()
    {
        life.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadSceneAsync("LifeTutorial", LoadSceneMode.Single);
    }
}
