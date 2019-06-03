using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public Transform Target;

    public float Damage;
    public float Speed = 50f;

    // Update is called once per frame
    public virtual void Update()
    {
        //This Method is meant to be overridden
    }

    /*
    public virtual void HitTarget()
    {
        //This Method is meant to be overridden
    }*/
}
