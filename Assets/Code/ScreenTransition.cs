using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Splines;

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

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            playerHealth = playerObj.GetComponent<HealthBar>();
        }
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + offset);

        uiElement.position = screenPos;

        if (playerHealth != null)
        {
            animator.SetBool("IsAlive", playerHealth.isAlive);
            animator.SetBool("IsDead", playerHealth.isDead);
        }
    }
}
