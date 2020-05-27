using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Server
{
    class FinalSceneManager : MonoBehaviour
    {
        bool activeScene;

        public void Awake()
        {
            activeScene = true;
            Debug.Log("Final Scene Awaken");
        }

        void Update()
        {

            if (activeScene)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    activeScene = false;
                    StartCoroutine("LoadMainMenu");
                }

            }

        }


        IEnumerator LoadMainMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            yield return new WaitForSeconds(0.1f);
            

        }
    }
}
