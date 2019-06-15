using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : BaseEnemy
{
    public List<GameObject> AttackEffects;
    public List<Transform> SpawnPoints;
    public List<AttackStats> Attacks;
    public float TimeBetweenBasicAttacks = 2f;
    public float TimeBetweenHeavyAttacks = 10f;
    public float SpawnAmount = 3f;
    public Slider HealthBar;
    public Slider ShieldBar;
    public GameObject FirePoint;
    public GameObject FriendObject;
    public Transform Player;

    private delegate void OnHPChangedCallBack();
    OnHPChangedCallBack onHpChangedCallBack;
    private delegate void OnAttackCallBack(int index);
    OnAttackCallBack onAttackCallBack;

    private bool basicAttackReady = true;
    private bool heavyAttackReady = false;
    private bool callHeavy = false;
    private bool spawnReady = false;
    private bool callSpawn = false;

    private void Awake()
    {
        onAttackCallBack += BasicAttack;
        HealthBar.maxValue = MaxHealth;
        ShieldBar.maxValue = MaxShield;

    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;

        StartCoroutine(SpawnAfterTime());
    }

    private void LateUpdate()
    {
        HealthBar.value = Health;
        ShieldBar.value = Shield;

        FirePoint.transform.LookAt(Player);

        if (basicAttackReady)
            StartCoroutine(WaitAndAttack());

        if (heavyAttackReady)
        {
            onAttackCallBack += HeavyAttack;
            heavyAttackReady = false;
            callHeavy = true;
        }
    }

    private void BasicAttack(int index)
    {
        GameObject effect = objectPooler.SpawnFromPool(AttackEffects[index].name, FirePoint.transform.position, Quaternion.identity);
        Attack attack = effect.GetComponent<Attack>();
        if(attack != null)
        {
            attack.Target = Player;
            attack.Speed = Attacks[index].AttackSpeed;
            attack.Impact = Attacks[index].Damage;
            attack.SetTargetPosition(Player);
            attack.ObjectPOoler = objectPooler;
        }
    }

    private void HeavyAttack(int index)
    {
        GameObject effect = objectPooler.SpawnFromPool(AttackEffects[index].name, FirePoint.transform.position, Quaternion.identity);
        Attack attack = effect.GetComponent<Attack>();
        if (attack != null)
        {
            attack.Target = Player;
            attack.Speed = Attacks[index].AttackSpeed;
            attack.Impact = Attacks[index].Damage;
        }
    }

    private IEnumerator WaitAndAttack()
    {
        basicAttackReady = false;
        yield return new WaitForSeconds(TimeBetweenBasicAttacks);
        if (!callHeavy)
        {
            onAttackCallBack(0);
        }
        else
        {
            onAttackCallBack -= BasicAttack;
            onAttackCallBack(1);
        }

        if(spawnReady)
        {
            SpawnFriends(FriendObject, 3);
            spawnReady = false;
        }

        basicAttackReady = true;
    }

    private void SpawnFriends(GameObject friend, int amount)
    {
        if (amount > SpawnPoints.Count) return;

        for(int index = 0; index <= amount; index++)
        {
            GameObject mate = objectPooler.SpawnFromPool(friend.name, SpawnPoints[index].position, Quaternion.identity);
            mate.transform.parent = SpawnPoints[index];
            BossManager.Instance.UpdateBossChildren(mate);
        }
    }

    private void SpawnFriends(int index)
    {
        foreach(Transform point in SpawnPoints)
        {
            GameObject friend = objectPooler.SpawnFromPool(FriendObject.name, point.position, Quaternion.identity);
        }
    }

    private void SpawnAFterTime()
    {
        spawnReady = true;
    }

    public IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(TimeBetweenHeavyAttacks);
        spawnReady = true;
    }

    private void CheckWeakSpot()
    {

    }
}
