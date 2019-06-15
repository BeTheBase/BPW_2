using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector3 ToZeroZ (this Vector3 currentVector)
    {
        return currentVector = new Vector3(currentVector.x, currentVector.y, 0);
    }
}

public class BossManager : MonoBehaviour
{
    public GameObject Boss;
    public float BossSpeed = 2f;
    public float TimeToHit = 5f;
    public List<GameObject> BossChildren;
    public static BossManager Instance;

    private bool begun = false;

    private void Awake()
    {
        Instance = this;
        begun = false;
    }

    private void Update()
    {
        if(begun)
        {
            if(BossChildren.Count < 1)
            {
                Boss.transform.position = Vector3.Lerp(Boss.transform.position, Boss.transform.position.ToZeroZ(), BossSpeed * Time.deltaTime);
                if (Vector3.Distance(Boss.transform.position, new Vector3(Boss.transform.position.x, Boss.transform.position.y, 0)) <= 1f)
                {
                    var bossCollider = Boss.GetComponent<Collider2D>();
                    bossCollider.enabled = true;
                    StartCoroutine(ColliderSwitch(false, bossCollider, TimeToHit));
                    begun = false;
                }
            }
        }
    }


    private void Start()
    {
    }

    public void UpdateBossChildren(GameObject child)
    {
        BossChildren.Add(child);
        begun = true;
    }

    private IEnumerator ColliderSwitch(bool on, Collider2D collider, float time)
    {
        yield return new WaitForSeconds(time);
        if (on)
            collider.enabled = true;
        else
            collider.enabled = false;

        StartCoroutine(Boss.GetComponent<BossEnemy>().SpawnAfterTime());
    }
}
