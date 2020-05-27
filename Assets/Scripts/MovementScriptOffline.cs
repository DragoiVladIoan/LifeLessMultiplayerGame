using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class MovementScriptOffline : MonoBehaviour
    {

        public float moveSpeed = 5f;
        private Rigidbody2D rb;
        Vector2 movement;
        public string horizontal_name;
        public string vertical_name;
        private bool allowMove = true;
        private int count = 0;

  
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if (allowMove)
            {
                movement.x = Input.GetAxisRaw(horizontal_name);
                movement.y = Input.GetAxisRaw(vertical_name);
            }
            
        }

        private void FixedUpdate()
        {
            
            if (allowMove)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
            
        }

        public void setAllowMove(bool active)
        {
            this.allowMove = active;
        }

        public Vector3 getMovement()
        {
            movement.x = Input.GetAxisRaw(horizontal_name);
            movement.y = Input.GetAxisRaw(vertical_name);
            return this.transform.position;
        }

        public void setScale(float xsize, float ysize, float zsize)
        {
            rb.transform.localScale = new Vector3(rb.transform.localScale.x + xsize, rb.transform.localScale.y + ysize, rb.transform.localScale.z);
        }

        public void setExactScale(float xsize, float ysize, float zsize)
        {
            rb.transform.localScale = new Vector3(xsize, ysize, zsize);
        }

        public Vector3 getScale()
        {
            return transform.localScale;
        }

        public bool getAllowMove()
        {
            return this.allowMove;
        }

    }
    
    
}
