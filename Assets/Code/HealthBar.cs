using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float maxHealth;
    public float healthDrain;

    private float originalGravity = 1;
    public Vector2 checkpointPos;
    public bool isDead;

    public HealthOrbRespawn orbRespawn;
    private Animator animator;
    public ScreenTransition transition;

    void Start()
    {
        HealthItem.OnHealthCollect += Heal;
        checkpointPos = transform.position;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        health -= healthDrain * Time.deltaTime;
        if (health < 0)
        {
            health = 0;

            var playerMovement = GetComponent<PlayerMovement>();
            playerMovement.enabled = false;
            playerMovement.rb.linearVelocity = Vector2.zero;
            playerMovement.rb.gravityScale = 0f;
            isDead = true;

            Vector3 finalPos = transform.position;
            transition.DeathTransition(finalPos);
            animator.SetBool("IsDead", isDead);

            playerMovement.tr.emitting = false;
            playerMovement.canDash = false;
            playerMovement.isDashing = false;
            Invoke("Die", 1);
            Invoke("Respawn", 2);
        }
        healthBar.fillAmount = health / maxHealth;
    }

    public void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth) health = maxHealth;
    }

    void Die()
    {
        gameObject.SetActive(false);

    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    void Respawn()
    {
        var playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = true;
        isDead = false;
        gameObject.SetActive(true);
        health = 100;
        Vector3 finalPos = transform.position;
        transition.RespawnTransition(finalPos);

        playerMovement.tr.emitting = false;
        playerMovement.canDash = true;
        playerMovement.isDashing = false;
        playerMovement.rb.gravityScale = originalGravity;
        orbRespawn.OrbRespawn();
        transform.position = checkpointPos;
    }
}
