using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : BaseProjectile
{
    ObjectPooler objectPooler;
    public Transform FirePoint;
    public GameObject HitEffect;
    public float EffectTime;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    public override void Update()
    {
            if (Target == null)
            {
                gameObject.SetActive(false);
                return;
            }
            transform.LookAt(Target);
            Vector3 dir = Target.position - transform.position;
            float distanceThisFrame = Speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void HitTarget()
    {
        GameObject BloodHit = objectPooler.SpawnFromPool(HitEffect.name, FirePoint.position, transform.rotation);

        Vector3 dir = FirePoint.position - Target.position;

        BloodHit.transform.position = Target.position + dir.normalized * 0.5f;

        BloodHit.transform.rotation = Quaternion.LookRotation(dir);

        Target.GetComponent<BaseEnemy>().TakeDamage(Damage);
        
        StartCoroutine(Deactivate(BloodHit, EffectTime));

        gameObject.SetActive(false);
    }

    private IEnumerator Deactivate(GameObject activeObject, float time)
    {
        yield return new WaitForSeconds(time);
        activeObject.SetActive(false);
    }
}
