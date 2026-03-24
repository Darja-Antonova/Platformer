//using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathFade : MonoBehaviour
{
    public float FadeDuration = 1f;
    public FadeType CurrentFadeType;

    private int _fadeAmount = Shader.PropertyToID("_FadeAmount");
    private int _useShutters = Shader.PropertyToID("_UseShutters");

    private int? _lastEffect;

    private Image _image;
    private Material _material;

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
        //_material.DOFade(1f, _fadeAmount, FadeDuration)
        //.SetEase(Ease.InOutSine);
    }

    private void StartFadeIn()
    {
        _material.SetFloat(_fadeAmount, 1f);
        StartCoroutine(HandleFade(0f, 1f));
        //_material.DOFade(0f, _fadeAmount, FadeDuration)
        //.SetEase(Ease.InOutSine);
    }

    private IEnumerator HandleFade(float targetAmount, float startAmount)
    {
        float elapsedTime = 0f;
        while(elapsedTime < FadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(startAmount, targetAmount, (elapsedTime / FadeDuration));
            _material.SetFloat(_fadeAmount, lerpedAmount);

            yield return null;
        }

        _material.SetFloat(_fadeAmount, targetAmount);
    }
}
