using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SocketIO
{
    class NetworkDash : MonoBehaviour
    {
        private Vector3 oldPosition;
        private Vector3 oldScale;

        private NetworkIdentity networkIdentity;
        private Rigidbody2D rb;
        private Player player;

        private float stillCounter = 0;

        public void Start()
        {
            networkIdentity = GetComponent<NetworkIdentity>();
            rb = GetComponent<Rigidbody2D>();
            oldPosition = transform.position;
            oldScale = transform.localScale;

            player = new Player();
            player.position = new Position();
            player.scale = new Scale();
            player.position.x = 0;
            player.position.y = 0;
            player.scale.x = rb.transform.localScale.x;
            player.scale.y = rb.transform.localScale.y;

            if (!networkIdentity.IsControlling())
            {
                enabled = false;
            }
        }

        public void Update()
        {
            if (networkIdentity.IsControlling())
            {
                if (oldPosition != transform.position  || oldScale != rb.transform.localScale)
                {
                    oldPosition = transform.position;
                    oldScale = rb.transform.localScale;
                    stillCounter = 0;
                    SendData();
                }
                else
                {
                    stillCounter += Time.deltaTime;
                    if (stillCounter > 1)
                    {
                        stillCounter = 0;
                        SendData();
                    }
                }
            }
        }

        private void SendData()
        {
            player.position.x = Mathf.Round(transform.position.x * 1000.0f) / 1000.0f;
            player.position.y = Mathf.Round(transform.position.y * 1000.0f) / 1000.0f;
            player.scale.x = Mathf.Round(rb.transform.localScale.x * 1000.0f) / 1000.0f;
            player.scale.y = Mathf.Round(rb.transform.localScale.y * 1000.0f) / 1000.0f;

            networkIdentity.GetSocket().Emit("dash", new JSONObject(JsonUtility.ToJson(player)));
        }

        public Rigidbody2D getRigidbody()
        {
            return rb;
        }

        public void setRbScale(Vector2 scale)
        {
            rb.transform.localScale = scale;
        }

    }

}
