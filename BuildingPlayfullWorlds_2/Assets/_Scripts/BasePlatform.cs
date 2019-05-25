using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var platform = transform.parent;
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider2D>(), platform.GetComponent<BoxCollider2D>());
            //platform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            var platform = transform.parent;
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<CapsuleCollider2D>(), platform.GetComponent<BoxCollider2D>(), false);
            //platform.GetComponent<BoxCollider2D>().enabled = true;

        }
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") == -1)
            transform.parent.gameObject.layer = 10;
        else
            transform.parent.gameObject.layer = 9;
    }
}
