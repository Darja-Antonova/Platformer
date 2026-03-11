using DG.Tweening;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveY(transform.position.y + 0.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
