using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    HealthBar checkpointPos;
    public Transform respawnPoint;
    Collider2D coll;
    public GameObject spotlight;

    private void Awake()
    {
        checkpointPos = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            spotlight.SetActive(true);
            checkpointPos.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
        }
    }
}
