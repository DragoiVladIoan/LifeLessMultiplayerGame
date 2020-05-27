using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ObjectSpawnerOffline : MonoBehaviour
    { 
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

        void Start()
        {

            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            StartCoroutine(objectWave());
        }

        // Update is called once per frame
        void Update()
        {
            
            
                   
        }

        private void spawnObject()
        {
            if (spawns.Count < spawnLimit)
            {
                int objectSelected = UnityEngine.Random.Range(0, interactiveObjects.Length);
                GameObject obj = Instantiate(interactiveObjects[objectSelected]) as GameObject;
                spawns.Add(obj);
                obj.transform.position = new Vector3(UnityEngine.Random.Range(-screenBounds.x + 0.5f, screenBounds.x - 0.5f), UnityEngine.Random.Range(-screenBounds.y + 0.5f, screenBounds.y - 0.5f), 0f);

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
}
