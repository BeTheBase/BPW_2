using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Deactivate : MonoBehaviour
{
    public float TimeToWait;
    public float Delay;
    public bool Activate;
    public bool FadeOut;
    public bool TextOut;

    private void Start()
    {
        if (FadeOut || TextOut)
        {
            StartCoroutine(DeOrActivateOverTime(TimeToWait + Delay));
            StartCoroutine(DelayFade(Delay));
        }
        else
            StartCoroutine(DeOrActivateOverTime(TimeToWait));
    }

    private IEnumerator DelayFade(float time)
    {
        yield return new WaitForSeconds(time);
        if (FadeOut)
            GetComponent<Image>().CrossFadeAlpha(0, TimeToWait, true);
        if (TextOut)
            GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, TimeToWait, true);
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
