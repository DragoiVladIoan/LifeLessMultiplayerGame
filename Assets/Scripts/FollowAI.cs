using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;
    private int direction;
    private float distance;
    public bool enabled;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        if (Random.value >= 0.5)
            direction = 1;
        else
            direction = -1;

        distance = Random.Range(4f, 8f);

        target = GameObject.FindGameObjectWithTag("tree").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (gameObject)
            {

                if (Vector2.Distance(gameObject.transform.position, target.position) > distance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                }
                else
                {
                    Vector3 point = target.transform.position;
                    Vector3 axis = new Vector3(0, 0, direction);
                    transform.RotateAround(point, axis, Time.deltaTime * 100);
                }
            }
        }
    }
}
