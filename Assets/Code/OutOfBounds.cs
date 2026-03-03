using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private Vector3 playerRespawnPosition = new Vector3 (-2, -1, 0);
    private float bottomDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<HealthBar>();
            if(player != null)
            {
                //collision.transform.position = playerRespawnPosition;
                bottomDeath = GameObject.Find("Player").GetComponent<HealthBar>().health = 0;
            }
        }
    }
}
