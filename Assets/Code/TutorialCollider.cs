using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TutorialCollider : MonoBehaviour
{
    Collider2D coll;
    public Animator StarAnimator;
    public Animator CharAnimator;
    public Animator ConAnimator;

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
            ConAnimator.SetBool("Hit", true);
            coll.enabled = false;
        }
    }
}
