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
    public Slider HealthBar;
    public Slider ShieldBar;
    public GameObject FriendObject;
    public Transform Player;

    private delegate void OnHPChangedCallBack();
    OnHPChangedCallBack onHpChangedCallBack;
    private delegate void OnAttackCallBack(int index);
    OnAttackCallBack onAttackCallBack;

    private ObjectPooler objectPooler;
    private bool basicAttackReady = true;
    private bool heavyAttackReady = false;
    private bool callHeavy = false;
    

    private void Awake()
    {
        onAttackCallBack += BasicAttack;
        HealthBar.maxValue = MaxHealth;
        ShieldBar.maxValue = MaxShield;
        HealthBar.value = Health;
        ShieldBar.value = Shield;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void LateUpdate()
    {
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
        GameObject effect = objectPooler.SpawnFromPool(AttackEffects[index].name, transform.position, Quaternion.identity);
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
        GameObject effect = objectPooler.SpawnFromPool(AttackEffects[index].name, transform.position, Quaternion.identity);
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
            onAttackCallBack(0);
        else
            onAttackCallBack(1);
        basicAttackReady = true;
    }

    private void SpawnFriends(GameObject friend, int amount)
    {

    }
}
