using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeathFade : MonoBehaviour
{
    public float FadeDuration = 1f;
    public FadeType CurrentFadeType;

    private int _fadeAmount = Shader.PropertyToID("_FadeAmount");
    private int _useShutters = Shader.PropertyToID("_UseShutters");

    private int? _lastEffect;

    private Image _image;
    private Material _material;
    private Coroutine _fadeCoroutine;

    public enum FadeType
    {
        Shutters
    }


    private void Awake()
    {
        _image = GetComponent<Image>();

        Material nat = _image.material;
        _image.material = new Material(nat);
        _material = _image.material;

        _lastEffect = _useShutters;
    }

    public void FadeOut(FadeType fadeType)
    {
        ChangeFadeEffect(fadeType);
        StartFadeOut();
    }

    public void FadeIn(FadeType fadeType)
    {
        ChangeFadeEffect(fadeType);
        StartFadeIn();
    }

    private void ChangeFadeEffect(FadeType fadeType)
    {
        if (_lastEffect.HasValue)
        {
            _material.SetFloat(_lastEffect.Value, 0f);
        }

        switch (fadeType)
        {
            case FadeType.Shutters:

                SwitchEffect(_useShutters); 
                break;
        }
    }

    private void SwitchEffect(int effectToTurnOn)
    {
        _material.SetFloat(effectToTurnOn, 1f);
        _lastEffect = effectToTurnOn;
    }

    private void StartFadeOut()
    {
        _material.SetFloat(_fadeAmount, 0f);
        StartCoroutine(HandleFade(1f, 0f));
    }

    private void StartFadeIn()
    {
        _material.SetFloat(_fadeAmount, 1f);
        StartCoroutine(HandleFade(0f, 1f));
    }

    private IEnumerator HandleFade(float startAmount, float targetAmount)
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);

        float elapsedTime = 0f;
        while(elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / FadeDuration);
            float lerpedAmount = Mathf.Lerp(startAmount, targetAmount, t);
            _material.SetFloat(_fadeAmount, lerpedAmount);

            yield return null;
        }

        _material.SetFloat(_fadeAmount, targetAmount);
        _fadeCoroutine = null;
    }

    public IEnumerator PlayFullDeathSequence(Action die, Action respawn)
    {
        _material.SetFloat(_fadeAmount, 0f);
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(HandleFade(0f, 1f));

        die.Invoke();
        yield return new WaitForSeconds(0.75f);
        respawn.Invoke();

        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(HandleFade(1f, 0f));
    }
}
