using UnityEngine;

public class TutorialColliderWOCon : MonoBehaviour
{
    Collider2D coll;
    public Animator StarAnimator;
    public Animator CharAnimator;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StarAnimator.SetBool("Hit", true);
            CharAnimator.SetBool("Hit", true);
            coll.enabled = false;
        }
    }
}
