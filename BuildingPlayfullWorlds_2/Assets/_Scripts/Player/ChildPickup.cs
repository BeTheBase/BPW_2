using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Child")
        {
            if (!GameManager.Children.ContainsKey(collision.gameObject.name))
                GameManager.Children.Add(collision.gameObject.name, collision.gameObject);
            else
                return;
        }
    }
}
