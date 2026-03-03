using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float maxHealth;
    public float healthDrain;

    private bool dashFromRespawn;

    [SerializeField] private Vector3 playerRespawnPosition = new Vector3 (-2, -1, 0);

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
            Die();
            Invoke("Respawn", 1);
        }
        healthBar.fillAmount = health / maxHealth;
    }

    void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth) health = maxHealth;
    }

    void Die()
    {
        gameObject.SetActive(false);

    }

    void Respawn()
    {
        /*transform.position = playerRespawnPosition;
        gameObject.SetActive(true);
        health = 100;
        dashFromRespawn = GameObject.Find("Player").GetComponent<PlayerMovement>().canDash = true;
        */
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
