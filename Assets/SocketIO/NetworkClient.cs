using SocketIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SocketIO
{
    class NetworkClient : SocketIOComponent
    {
        [SerializeField]
        private Transform networkContainer;

        private Dictionary<String, NetworkIdentity> serverObjects;

        public GameObject life, death;

        public static string clientId { get; private set; }

        private string randomSpawnSeed;

        private int numberOfPlayers = 0;

        public override void Start()
        {
            base.Start();
            Initialize();
            SetupEvents();

        }

        public override void Update()
        {
            base.Update();
        }

        private void Initialize()
        {
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }

        public int GetRandomSpawnSeed()
        {
            if (randomSpawnSeed != null)
            {
                return Int32.Parse(randomSpawnSeed);
            }
            return -1;
        }

        public int GetNumberOfPlayers()
        {
            return numberOfPlayers;
        }

        public Dictionary<String, NetworkIdentity> GetServerObjects()
        {
            return serverObjects;
        }

        private void SetupEvents()
        {
            On("open", (E) =>
            {
                Debug.Log("Connection made to the server!");
            });

            On("register", (E) =>
            {
                clientId = E.data["id"].ToString();

                Debug.Log("Client id is " + clientId);
            });

            On("spawn", (E) =>
            {
                string id = E.data["id"].ToString();
                randomSpawnSeed = E.data["randomSpawnSeed"].ToString();
                Debug.Log(E.data["id"].ToString());
                if (E.data["count"].ToString() == "1")
                {
                    GameObject playerSpawned = Instantiate(life, networkContainer);
                    playerSpawned.transform.SetParent(networkContainer);
                    NetworkIdentity ni = playerSpawned.GetComponent<NetworkIdentity>();
                    ni.SetControllerId(id);
                    ni.SetSocketReference(this);
                    serverObjects.Add(id, ni);
                    numberOfPlayers++;
                }
                else if (E.data["count"].ToString() == "2")
                {
                    GameObject playerSpawned = Instantiate(death, networkContainer);
                    playerSpawned.transform.SetParent(networkContainer);
                    NetworkIdentity ni = playerSpawned.GetComponent<NetworkIdentity>();
                    ni.SetControllerId(id);
                    ni.SetSocketReference(this);
                    serverObjects.Add(id, ni);
                    numberOfPlayers++;
                }
            });

            On("move", (E) =>
            {
                string id = E.data["id"].ToString();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(x, y, 0);
            });

            On("dash", (E) =>
            {
                string id = E.data["id"].ToString();
                float posx = E.data["position"]["x"].f;
                float posy = E.data["position"]["y"].f;
                float scalex = E.data["scale"]["x"].f;
                float scaley = E.data["scale"]["y"].f;

                NetworkIdentity ni = serverObjects[id];
                ni.transform.position = new Vector3(posx, posy, 0);
                ni.setRb(new Vector3(scalex, scaley, 0));
               
            });

            On("disconnected", (E) =>
            {
                string id = E.data["id"].ToString();
                Debug.Log("D/C" +id);
                GameObject playerDestroy = serverObjects[id].gameObject;
                Destroy(playerDestroy);
                serverObjects.Remove(id);
            });
        }

    }

    [Serializable]
    public class Player
    {
        public string id;
        public Position position;
        public Scale scale;

    }

    [Serializable]
    public class Position
    {
        public float x;
        public float y;
    }

    [Serializable]
    public class Scale
    {
        public float x;
        public float y;
    }
}
