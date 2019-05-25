using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : BaseProjectile
{
    private int LookDirection = 1;
    public Transform FirePoint;
    public GameObject HitEffect;
    public float EffectTime;

    ObjectPooler objectPooler;
    Rigidbody2D rBody2D;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        rBody2D = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        //rBody2D.AddForce(new Vector2(LookDirection, 0) * Speed);
        transform.Translate((transform.right * LookDirection) * Speed * Time.deltaTime);
    }

    public void Shoot(int lookDirection)
    {
        LookDirection = lookDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HitTarget(collision.transform);
        }       
    }

    public void HitTarget(Transform Target)
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
