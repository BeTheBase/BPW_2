using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWind : MonoBehaviour
{
    public List<Transform> MovePoints;
    public float Speed = 2f;
    public float DeactivationTime = 2f;
    public float EffectTime = 1f;
    public GameObject HitEffect;
    public ObjectPooler ObjectPOoler;
    public float Impact = 1f;

    private int NextPoint = 0;

    private void Start()
    {
        foreach (Transform spot in MovePoints)
        {
            spot.parent = null;
        }
    }

    private void Update()
    {
        Vector3 nextPointPosition = Vector3.zero;

        if (NextPoint < MovePoints.Count)
        {
            //Move from point to point
            nextPointPosition = new Vector3(MovePoints[NextPoint].position.x, transform.position.y, MovePoints[NextPoint].position.z);

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(MovePoints[NextPoint].position.x, 0, MovePoints[NextPoint].position.z)) < 0.1f)
            {
                NextPoint++;
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
        }
        else
        {
            NextPoint = 0;
            nextPointPosition = new Vector3(MovePoints[NextPoint].position.x, transform.position.y, MovePoints[NextPoint].position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
        }




        //Disable if fall from map
        if (transform.position.y <= -5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(HitTarget(collision.transform));
        }
    }

    public IEnumerator HitTarget(Transform Target)
    {
        GameObject BloodHit = ObjectPOoler.SpawnFromPool(HitEffect.name, transform.position, transform.rotation);

        Vector3 dir = transform.position - Target.position;

        BloodHit.transform.position = Target.position + dir.normalized * 0.5f;

        BloodHit.transform.rotation = Quaternion.LookRotation(dir);

        GameManager.Instance.SetLives((int)Impact, true);

        StartCoroutine(Deactivate(BloodHit, EffectTime));
        yield return new WaitForSeconds(EffectTime);
    }

    private IEnumerator Deactivate(GameObject activeObject, float time)
    {
        yield return new WaitForSeconds(time);
        activeObject.SetActive(false);
    }
}
