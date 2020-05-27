using SocketIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SocketIO
{
    class NetworkIdentity : MonoBehaviour
    {
        private string id;

        [SerializeField]
        private bool isControlling;

        private SocketIOComponent socket;

        private Rigidbody2D rb;

        public void Awake()
        {
            if (!isControlling)
            {
                isControlling = false;
            }
            rb = GetComponent<Rigidbody2D>();

        }

        public void SetControllerId(string ID)
        {
            id = ID;
            isControlling = NetworkClient.clientId == ID ? true : false;

        }

        public void SetSocketReference(SocketIOComponent Socket)
        {
            socket = Socket;
        }

        public string GetId()
        {
            return this.id;
        }

        public SocketIOComponent GetSocket()
        {
            return this.socket;
        }

        public bool IsControlling()
        {
            return isControlling;
        }

        public void setRb(Vector3 RB)
        {
            rb.transform.localScale = RB;
        }

    }
}
