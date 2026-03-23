using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float maxHealth;
    public float healthDrain;

    private bool dashFromRespawn;
    private float moveFromRespawn;
    private float originalGravity = 1;
    public Vector2 checkpointPos;
    private bool freezeDash;
    private Vector2 freezeMovement;
    private float freezeGravity;
    private bool isDead;

    public HealthOrbRespawn orbRespawn;
    private Animator animator;

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

            gameObject.GetComponent<PlayerMovement>().enabled = false;
            freezeMovement = GameObject.Find("Player").GetComponent<PlayerMovement>().rb.linearVelocity = Vector2.zero;
            freezeGravity = GameObject.Find("Player").GetComponent<PlayerMovement>().rb.gravityScale = 0f;
            isDead = true;
            animator.SetBool("IsDead", isDead);
            freezeDash = GameObject.Find("Player").GetComponent<PlayerMovement>().tr.emitting = false;
            freezeDash = GameObject.Find("Player").GetComponent<PlayerMovement>().canDash = false;
            freezeDash = GameObject.Find("Player").GetComponent<PlayerMovement>().isDashing = false;
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
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        isDead = false;
        gameObject.SetActive(true);
        health = 100;
        dashFromRespawn = GameObject.Find("Player").GetComponent<PlayerMovement>().tr.emitting = false;
        dashFromRespawn = GameObject.Find("Player").GetComponent<PlayerMovement>().canDash = true;
        dashFromRespawn = GameObject.Find("Player").GetComponent<PlayerMovement>().isDashing = false;
        moveFromRespawn = GameObject.Find("Player").GetComponent<PlayerMovement>().rb.gravityScale = originalGravity;
        orbRespawn.OrbRespawn();
        transform.position = checkpointPos;

    }
}
