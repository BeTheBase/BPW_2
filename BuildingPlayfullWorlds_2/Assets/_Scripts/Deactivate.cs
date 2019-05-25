using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public float TimeToWait;
    public bool Activate;

    private void Start()
    {
        StartCoroutine(DeOrActivateOverTime(TimeToWait));
    }

    private IEnumerator DeOrActivateOverTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (!Activate)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
