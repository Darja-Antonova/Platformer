using DG.Tweening;
using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveY(transform.position.y + 10f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
