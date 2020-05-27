using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class SaplingPickupOffline : MonoBehaviour
    {
        private GameObject mainCamera;
        private ObjectSpawnerOffline objectSpawner;
        private List<GameObject> followers;

        // Start is called before the first frame update
        void Start()
        {
            followers = new List<GameObject>();
            mainCamera = GameObject.Find("Main Camera");
            objectSpawner = mainCamera.GetComponent<ObjectSpawnerOffline>();
        }

        // Update is called once per frame
        void Update()
        {
            //int i = 0;
            //foreach (var sapling in saplings)
            //{
            //    sapling.GetComponent<ObjectSpawner>().getSpawns(i);
            //    Debug.Log(sapling.GetInstanceID());
            //    i++;
            //}
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "sapling")
            {
                foreach (GameObject sapling in objectSpawner.spawns.ToArray())
                    if (sapling.GetInstanceID() == collision.gameObject.GetInstanceID())
                    {
                        sapling.tag = "follower";
                        followers.Add(sapling);
                        sapling.GetComponent<SpiritState>().change = true;
                    }
            }
        }

        public List<GameObject> GetFollowers()
        {
            return this.followers;
        }
    }

}
