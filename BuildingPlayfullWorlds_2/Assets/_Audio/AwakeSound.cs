using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeSound : MonoBehaviour
{
    public string LastSound;
    public string SoundName = "Beginning";
    public float TimeToWait=10f;

    private bool CheckActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().Play("Beginning");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!CheckActive)
            {
                if (LastSound != null)
                    FindObjectOfType<AudioManager>().Stop(LastSound);
                if (SoundName != null)
                    FindObjectOfType<AudioManager>().Play(SoundName);
            }

            CheckActive = true;
            StartCoroutine(SetActiveAfterTime(TimeToWait));
        }
    }

    private IEnumerator SetActiveAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        CheckActive = false;
    }
}
