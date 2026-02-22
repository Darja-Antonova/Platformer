using System;
using UnityEngine;

public class HealthItem : MonoBehaviour, IItem
{
    public int healAmount = 20;
    public static event Action<int> OnHealthCollect;

    public void Collect()
    {
        OnHealthCollect.Invoke(healAmount);
        Destroy(gameObject);
    }
}
