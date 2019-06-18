using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderEnemy : BaseEnemy
{
    public GameObject AttackCylinder;
    public float RotateSpeed = 20f;
    public Transform Player;
    public static GameManager gameManager;

    private bool attackReady = true;

    public override void Start()
    {
        base.Start();
        //objectPooler = ObjectPooler.Instance;
        gameManager = GameManager.Instance;

        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").transform;

        ChildGameObject = AttackCylinder;
    }

    public override void Update()
    {
        base.Update();

        AttackCylinder.transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);

    }
}
