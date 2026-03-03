using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private float bottomDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<HealthBar>();
            if(player != null)
            {
                bottomDeath = GameObject.Find("Player").GetComponent<HealthBar>().health = 0;
            }
        }
    }
}
