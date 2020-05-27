using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class DashOffline : MonoBehaviour
    {
        private string dashState;
        private bool activeDash = true;
        public float startDashChargeTime, startDashCooldown;
        private float dashChargeTime, dashCooldown;

        private Rigidbody2D rb;
        private MovementScript mv;
        public float dashSpeed;
        private bool inDashing = false;
        private Vector3 primaryScale;
        private CircleCollider2D collider;
        private Vector2 direction;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            mv = GetComponent<MovementScript>();
            collider = GetComponent<CircleCollider2D>();

            dashState = "active";
            dashChargeTime = startDashChargeTime;
            dashCooldown = startDashCooldown;
            primaryScale = mv.getScale();
            activeDash = true;
            inDashing = false;
        }

        void Update()
        {
           
            //3 cases activeDash inDash noActiveDash
            switch (dashState)
            {
                case "active":
                    PerformActiveDash();
                    break;
                case "dashing":
                    PerformInDash();
                    break;
                case "inactive":
                    PerformInactiveDash();
                    break;
            }
            

            //collider.size = new Vector3(primaryScale.x + 0.1f, primaryScale.y + 0.1f, primaryScale.z);

        }

        private void PerformActiveDash()
        {
            if (Input.GetButton("Dash"))
            {
                if (dashChargeTime > 0)
                {
                    mv.setAllowMove(false);
                    mv.setScale(0.01f, 0.01f, 0.01f);
                    collider.radius = collider.radius + 0.01f;
                    dashChargeTime -= Time.deltaTime;
                    direction = CalculateDirection();
                }
                else
                {
                    activeDash = false;
                    dashChargeTime = startDashChargeTime;
                }
            }

            if (Input.GetButtonUp("Dash") || dashChargeTime <= 0)
            {
                dashState = "dashing";
                activeDash = false;
                dashChargeTime = startDashChargeTime;

            }
        }

        private void PerformInDash()
        {
            if (mv.getScale().x > primaryScale.x)
            {
                //fixed position
                Debug.Log(direction);
                rb.MovePosition(rb.position + direction * dashSpeed * Time.fixedDeltaTime);
                mv.setScale(-0.01f, -0.01f, -0.01f);
                //collider.radius = collider.radius - 0.01f;
            }
            else
            {
                mv.setExactScale(primaryScale.x, primaryScale.y, primaryScale.z);
                inDashing = false;
                dashState = "inactive";
                mv.setAllowMove(true);
            }
        }

        private void PerformInactiveDash()
        {
            if (dashCooldown > 0f)
            {
                activeDash = false;
                dashCooldown -= Time.deltaTime;
            }
            else
            {
                activeDash = true;
                dashCooldown = startDashCooldown;
                dashState = "active";
            }
        }

        private Vector2 CalculateDirection()
        {
            //up-left
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                return new Vector2(-1, 1);
            }
            //down-left
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                return new Vector2(-1, -1);
            }
            //down-right
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                return new Vector2(1, -1);
            }
            //up=right
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                return new Vector2(1, 1);
            }
            //left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return new Vector2(-1, 0);
            }
            //down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                return new Vector2(0, -1);
            }
            //right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return new Vector2(1, 0);
            }
            //up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                return new Vector2(0, 1);
            }

            return new Vector2(0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "wall")
            {
                //rb.velocity = Vector2.zero;
                //Debug.Log(collider.radius);
                //Debug.Log(mv.getScale());
                mv.setAllowMove(true);
                //mv.setExactScale(primaryScale.x, primaryScale.y, primaryScale.z);
                //collider.size = new Vector3(primaryScale.x + 0.1f, primaryScale.y + 0.1f, primaryScale.z);
                //collider.radius = primaryScale.x + 0.1f;

            }
            if (collision.gameObject.tag == "tree")
            {
                mv.setAllowMove(true);
                if (inDashing)
                    inDashing = false;
                if (activeDash)
                    activeDash = false;
                // mv.setExactScale(primaryScale.x, primaryScale.y, primaryScale.z);
                //collider.size = new Vector3(primaryScale.x + 0.1f, primaryScale.y + 0.1f, primaryScale.z);
                //collider.radius = primaryScale.x + 0.1f;
            }
        }
    }
}
