using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float maxHealth;
    public float healthDrain;

    void Start()
    {
        HealthItem.OnHealthCollect += Heal;
    }
    void Update()
    {
        health -= healthDrain * Time.deltaTime;
        if (health < 0)
        {
            health = 0;
            gameObject.SetActive(false);
        }
        healthBar.fillAmount = health / maxHealth;
    }

    void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth) health = maxHealth;
    }
}
