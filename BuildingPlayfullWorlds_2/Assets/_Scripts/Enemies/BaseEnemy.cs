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

    public float DeactivationTime = 2f;

    public int GoldGiven;

    public List<Transform> EnemySpots;

    public GameObject ChildGameObject;

    public GameObject DeathEffect;

    public ObjectPooler objectPooler;

    public bool IsSlowed;

    public bool HasShield;

    // Start is called before the first frame update
    public virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
        NextPoint = 0;
        Health = MaxHealth;
        Shield = MaxShield;
        foreach(Transform spot in EnemySpots)
        {
            spot.parent = null;
        }
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
            }
            else
            {
                NextPoint = 0;
                nextPointPosition = new Vector3(EnemySpots[NextPoint].position.x, transform.position.y, EnemySpots[NextPoint].position.z);
                transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
            }
        

        

        //Disable if fall from map
        if (transform.position.y <= -5)
        {
            gameObject.SetActive(false);
        }


    }

    public void SetChild(GameObject _child)
    {
        ChildGameObject = _child;
    }

    public void Die()
    {
        //objectPooler.SpawnFromPool("Blood", transform.position, Quaternion.identity);
        //gameManager.Gold += GoldGiven;

        GameObject deathEffect = objectPooler.SpawnFromPool(DeathEffect.name, transform.position, DeathEffect.transform.rotation);
        
        if (GameObject.Find("BossEnemy"))
            BossManager.Instance.BossChildren.Remove(gameObject);
        SpawnEffect SpawnAndDeactivateScript = GetComponent<SpawnEffect>();
        if (ChildGameObject == null) ChildGameObject = transform.GetChild(1).gameObject;
        SpawnEffect SpawnAndDeactivateScriptChild = ChildGameObject.GetComponent<SpawnEffect>();
        
        SpawnAndDeactivateScript.SetTimer = false;
        SpawnAndDeactivateScriptChild.SetTimer = false;
        StartCoroutine(Deactivate()); 
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(DeactivationTime);
        Destroy(this.gameObject);
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

        //If health is less than 0 execute Die function
        if (Health <= 0)
        {
            Die();
        }
    }
}