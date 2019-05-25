using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float Speed;

    [HideInInspector]
    public int NextPoint = 0;

    public float MaxHealth;
    public float Health;

    public float MaxShield;
    public float Shield;

    public int GoldGiven;

    public List<Transform> EnemySpots;

    public AnimationCurve DeactivationCurve;

    private GameManager gameManager;
    private ObjectPooler objectPooler;
    public EnemyMovePoints MovePoints;

    public bool IsSlowed;

    public bool HasShield;


    // Start is called before the first frame update
    void Start()
    {
        MovePoints = MovePoints.Instance;
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        NextPoint = 0;
        Health = MaxHealth;
        Shield = MaxShield;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector3 nextPointPosition = Vector3.zero;

        if (NextPoint < EnemySpots.Count)
        {
            //Move from point to point
            nextPointPosition = new Vector3(EnemySpots[NextPoint].position.x, transform.position.y, EnemySpots[NextPoint].position.z);

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(EnemySpots[NextPoint].position.x, 0, EnemySpots[NextPoint].position.z)) < 0.1f)
            {
                NextPoint++;
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
            transform.LookAt(nextPointPosition);
        }
        else
        {
            NextPoint = 0;
            nextPointPosition = new Vector3(EnemySpots[NextPoint].position.x, transform.position.y, EnemySpots[NextPoint].position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
            transform.LookAt(nextPointPosition);
        }

        

        //Disable if fall from map
        if (transform.position.y <= -5)
        {
            gameObject.SetActive(false);
        }

        //If health is less than 0 execute Die function
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //objectPooler.SpawnFromPool("Blood", transform.position, Quaternion.identity);
        //gameManager.Gold += GoldGiven;
        SpawnEffect SpawnAndDeactivateScript = GetComponent<SpawnEffect>();

        SpawnAndDeactivateScript.SetTimer = false;

        StartCoroutine(Deactivate()); 
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public IEnumerator Slow(float slowMultiplier, float slowTime)
    {
        IsSlowed = true;
        float baseSpeed = Speed;
        Speed *= slowMultiplier;
        yield return new WaitForSeconds(slowTime);
        Speed = baseSpeed;
        IsSlowed = false;
    }

    public void Heal(float healAmount)
    {
        if (healAmount <= (MaxHealth - Health))
        {
            Health += healAmount;
        }
        else
        {
            Health = MaxHealth;
        }
    }

    public void TakeDamage(float Damage)
    {
        if (HasShield && Shield > 0)
        {
            Shield -= Damage;
        }
        else
        {
            Health -= Damage;
        }
    }
}