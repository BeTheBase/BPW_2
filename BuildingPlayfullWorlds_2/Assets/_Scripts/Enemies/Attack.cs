using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Speed = 5f;
    public float Impact = 1f;
    public Transform Target;
    public float DeactivationTime = 2f;
    public float EffectTime = 1f;
    public GameObject HitEffect;
    public ObjectPooler ObjectPOoler;

    private Vector3 shootDirection;

    private void Start()
    {
        if(Target != null)
            transform.LookAt(Target);

        StartCoroutine(Deactivate(gameObject, EffectTime));

    }
    private void LateUpdate()
    {
        if (Target == null)
            return;

        if(shootDirection != null && shootDirection != Vector3.zero)
            transform.position = Vector3.Lerp(transform.position, shootDirection, Speed * Time.deltaTime);

    }

    public void SetTargetPosition(Transform target)
    {
        shootDirection = target.position;
        //StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        transform.position = Vector3.Lerp(transform.position, shootDirection, Speed);
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HitTarget(collision.transform);
        }
    }

    public void HitTarget(Transform Target)
    {
        GameObject BloodHit = ObjectPOoler.SpawnFromPool(HitEffect.name, transform.position, transform.rotation);

        Vector3 dir = transform.position - Target.position;

        BloodHit.transform.position = Target.position + dir.normalized * 0.5f;

        BloodHit.transform.rotation = Quaternion.LookRotation(dir);

        GameManager.Instance.SetLives((int)Impact, true);

        StartCoroutine(Deactivate(BloodHit, EffectTime));

    }

    private IEnumerator Deactivate(GameObject activeObject, float time)
    {
        yield return new WaitForSeconds(time);
        activeObject.SetActive(false);
        gameObject.SetActive(false);

    }



}
