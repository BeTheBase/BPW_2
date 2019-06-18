using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDeactivation : MonoBehaviour
{
    public float FadeTime = 3f;
    public float WaitDelay = 5f;

    private Component currentComponent;
    private Image currentImage;
    private TextMeshProUGUI currentTextMeshUGUI;
    private TextMeshPro currentTextMeshPro;
    private Text currentText;

    private float targetAlpha;

    private void Start()
    {
        if (GetComponent<Image>())
        {
            currentImage = GetComponent<Image>();
            targetAlpha = currentImage.color.a;
        }
        else if (GetComponent<TextMeshProUGUI>())
        {
            currentTextMeshUGUI = GetComponent<TextMeshProUGUI>();
            targetAlpha = currentTextMeshUGUI.color.a;
        }
        else if (GetComponent<Text>())
        {
            currentText = GetComponent<Text>();
            targetAlpha = currentText.color.a;
        }

        StartCoroutine(FadeIn(WaitDelay));
    }

    private IEnumerator FadeIn(float time)
    {
        if (currentImage != null)
        {
            currentImage.canvasRenderer.SetAlpha(0.1f);
            currentImage.CrossFadeAlpha(1f, FadeTime, true);

        }
        else if (currentTextMeshPro != null)
        {
            currentTextMeshPro.canvasRenderer.SetAlpha(0.1f);
            currentTextMeshPro.CrossFadeAlpha(1f, FadeTime, true);

        }
        else if (currentTextMeshUGUI != null)
        {
            currentTextMeshUGUI.canvasRenderer.SetAlpha(0.1f);
            currentTextMeshUGUI.CrossFadeAlpha(1f, FadeTime, true);

        }
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeOut(WaitDelay));
    }

    private IEnumerator FadeOut(float time)
    {
        yield return new WaitForSeconds(time);
        if (currentImage != null)
            currentImage.CrossFadeAlpha(0, FadeTime, true);
        else if (currentTextMeshPro != null)
            currentTextMeshPro.CrossFadeAlpha(0, FadeTime, true);
        else if (currentTextMeshUGUI != null)
            currentTextMeshUGUI.CrossFadeAlpha(0, FadeTime, true);
    }
}
