using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    public Transform player;
    public RectTransform uiElement;
    public Vector3 offset;
    private Animator animator;
    private Vector2 deathWorldPos;
    private Vector2 reviveWorldPos;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Vector2 lastPos = player.position + offset;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(lastPos);

        uiElement.position = screenPos;

    }

    public void RespawnTransition(Vector3 worldPos)
    {
        reviveWorldPos = worldPos;
        UpdateToSpecificPosition(reviveWorldPos);
        animator.SetBool("IsDead", false);
    }

    public void DeathTransition(Vector3 worldPos)
    {
        deathWorldPos = worldPos;
        UpdateToSpecificPosition(deathWorldPos);
        animator.SetBool("IsDead", true);
    }

    public void UpdateToSpecificPosition(Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos + offset);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiElement.parent as RectTransform,
            screenPos,
            null,
            out Vector2 localPoint
        );

        uiElement.anchoredPosition = localPoint;
    }
}
