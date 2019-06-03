using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AbstractBehavior
{
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float ProjectileSpeed;

    public Transform Target;
    public Transform FirePoint;

    public string EnergyAttackEffect = "EnergyExplosion";
    public string PlayerHitEffect = "Shockwave";

    private GameManager gameManager;
    private ObjectPooler objectPooler;
    private bool ReadyToFire = true;
    private bool attackReady = true;

    public override void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        //InvokeRepeating("UpdateTarget", 0f, 1f);
        //InvokeRepeating("CheckTargetStatus", 0f, 1f);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && colorManager.playerColors == ColorS.Blue)
        {
            if (ReadyToFire)
            {
                FireEnergy();
                StartCoroutine(CheckReady(AttackSpeed));
            }
        }
        /*
        if (Input.GetMouseButtonDown(0) && colorManager.playerColors == ColorS.Blue)
        {
            UpdateTarget();
            CheckTargetStatus();
            if (Target != null && ReadyToFire)
            {
                FireEnergy();
                StartCoroutine(CheckReady(AttackSpeed));
            }                    
        }*/
    }

    private IEnumerator CheckReady(float time)
    {
        ReadyToFire = false;
        yield return new WaitForSeconds(time);
        ReadyToFire = true;
    }

    //Update the current Target.
    void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in Enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= AttackRange)
        {
            Target = nearestEnemy.transform;
        }
    }

    //Check if Target is still alive to prevent shooting at nothing.
    void CheckTargetStatus()
    {
        if (Target != null)
        {
            if (!Target.gameObject.activeSelf)
            {
                Target = null;
            }
        }
    }

    /*
    public void FireEnergy()
    {
        GameObject FireBall = objectPooler.SpawnFromPool(EnergyAttackEffect, FirePoint.position, transform.rotation);
        FireBallProjectile fireBallScript = FireBall.GetComponent<FireBallProjectile>();
        if (fireBallScript != null)
        {
            fireBallScript.Damage = AttackDamage;
            fireBallScript.FirePoint = FirePoint;
            fireBallScript.Target = Target;
            fireBallScript.Speed = AttackSpeed;
        }
    }*/

    public void FireEnergy()
    {
        GameObject FireBall = objectPooler.SpawnFromPool(EnergyAttackEffect, FirePoint.position, transform.rotation);
        ForwardProjectile fireBallScript = FireBall.GetComponent<ForwardProjectile>();
        if (fireBallScript != null)
        {
            fireBallScript.Shoot((int)inputState.Direction);
            fireBallScript.Damage = AttackDamage;
            fireBallScript.FirePoint = FirePoint;
            fireBallScript.Speed = ProjectileSpeed;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "CylinderHand")
        {
            if (attackReady)
            {
                GameObject ShockWave = objectPooler.SpawnFromPool(PlayerHitEffect, transform.position, transform.rotation);
                AttackPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlackWall")
        {
            if (colorManager.playerColors == ColorS.Black)
            {
                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    private void AttackPlayer()
    {
        StartCoroutine(AttackPlayerOnce());
    }
    private IEnumerator AttackPlayerOnce()
    {
        attackReady = false;
        gameManager.SetLives(1, true);
        yield return new WaitForSeconds(1f);
        attackReady = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlackWall")
        {
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = true;

        }
    }

}
