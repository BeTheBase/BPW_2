using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AbstractBehavior
{
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;

    public Transform Target;
    public Transform FirePoint;

    private ObjectPooler objectPooler;
    private bool ReadyToFire = true;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;

        //InvokeRepeating("UpdateTarget", 0f, 1f);
        //InvokeRepeating("CheckTargetStatus", 0f, 1f);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && PlayerColor.playerColors == ColorS.Red)
        {
            if (Target != null)
                FireFireBall();
        }
        if (Input.GetMouseButtonDown(0) && PlayerColor.playerColors == ColorS.Blue)
        {
            UpdateTarget();
            CheckTargetStatus();
            if (Target != null && ReadyToFire)
            {
                FireEnergy();
                StartCoroutine(CheckReady(1.4f));
            }                    
        }
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

    public void FireEnergy()
    {
        GameObject FireBall = objectPooler.SpawnFromPool("EnergyExplosion", FirePoint.position, transform.rotation);
        FireBallProjectile fireBallScript = FireBall.GetComponent<FireBallProjectile>();
        if (fireBallScript != null)
        {
            fireBallScript.Damage = AttackDamage;
            fireBallScript.FirePoint = FirePoint;
            fireBallScript.Target = Target;
        }
    }

    public void FireFireBall()
    {
        GameObject FireBall = objectPooler.SpawnFromPool("FireBall", FirePoint.position, transform.rotation);
        ForwardProjectile fireBallScript = FireBall.GetComponent<ForwardProjectile>();
        if (fireBallScript != null)
        {
            fireBallScript.Shoot((int)inputState.Direction);
            fireBallScript.Damage = AttackDamage;
            fireBallScript.FirePoint = FirePoint;
            fireBallScript.Target = Target;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

}
