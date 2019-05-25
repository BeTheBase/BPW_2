using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour {

    public float SpawnEffectTime = 2;
    public float Pause = 1;
    public AnimationCurve FadeIn;
    public AnimationCurve FadeOut;

    [HideInInspector]
    public ParticleSystem ps;
    [HideInInspector]
    public bool SetTimer = false;
    [HideInInspector]
    public float timer = 0;
    Renderer _renderer;

    int shaderProperty;

	void Start ()
    {
        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();
        ps = GetComponentInChildren <ParticleSystem>();

        var main = ps.main;
        main.duration = SpawnEffectTime;

        ps.Play();
        StartCoroutine(Spawn());
    }

    void Update ()
    {
        
        if (timer < SpawnEffectTime + Pause && !SetTimer)
        {
            timer += Time.deltaTime;

        }
        else
        {
            //ps.Play();
            timer = 0;
        }

        _renderer.material.SetFloat(shaderProperty, FadeIn.Evaluate(Mathf.InverseLerp(0, SpawnEffectTime, timer)));

    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnEffectTime);
        FadeIn = FadeOut;
        SetTimer = true;
        //enabled = false;

    }
}
