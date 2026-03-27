using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    public Transform player;
    public RectTransform uiElement;
    public Vector3 offset;
    private Animator animator;
    private HealthBar playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthBar>();
        }
    }

    private void Start()
    {
        Vector3 lastPos = player.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(lastPos);

        uiElement.position = screenPos;

    }

    private void Update()
    {
        if (playerHealth != null)
        {
            animator.SetBool("IsAlive", playerHealth.isDead == false);
            animator.SetBool("IsDead", playerHealth.isDead);
        }
    }

    public void RespawnTransition()
    {
        animator.SetBool("IsAlive", playerHealth.isDead == false);
    }

    public void DeathTransition()
    {
        animator.SetBool("IsDead", playerHealth.isDead);
    }
}
