using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    HealthBar checkpointPos;
    public Transform respawnPoint;
    Collider2D coll;
    Animator spotlight;
    public GameObject spotlightObject;

    private void Awake()
    {
        checkpointPos = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        coll = GetComponent<Collider2D>();
        spotlight = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            spotlightObject.SetActive(true);
            spotlight.Play("Spotlight Animation");
            checkpointPos.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
        }
    }
}
