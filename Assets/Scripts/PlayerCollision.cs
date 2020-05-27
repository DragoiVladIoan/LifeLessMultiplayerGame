using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    SpriteRenderer rend;
    Color c;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        c = rend.color;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulldozer")
        {
            StartCoroutine("Stunned");
            collision.gameObject.GetComponent<Points>().points += collision.gameObject.GetComponent<Points>().stun_value;
        }

    }

    IEnumerator Stunned()
    {
        Physics2D.IgnoreLayerCollision(0, 0, true);
        gameObject.GetComponent<MovementScript>().enabled = false;
        for (var i = 0; i < 3; i++)
        {
            rend.color = Color.gray;
            yield return new WaitForSeconds(0.3f);
            rend.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
        gameObject.GetComponent<MovementScript>().enabled = true;
        rend.color = Color.yellow;
        yield return new WaitForSeconds(1.5f);
        rend.color = Color.white;
        Physics2D.IgnoreLayerCollision(0, 0, false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
