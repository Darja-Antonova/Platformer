using UnityEngine;

public class TutorialHealthBlock : MonoBehaviour
{
    private float healthDrainStop;
    private float healthDrainRestart;
    private float healthDrainDefault;

    private void Start()
    {
        healthDrainDefault = GameObject.Find("Player").GetComponent<HealthBar>().healthDrain;
    }
    private void OnTriggerStay2D(Collider2D collision)

    {
        if (collision.CompareTag("Player"))
        {
            healthDrainStop = GameObject.Find("Player").GetComponent<HealthBar>().healthDrain = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthDrainRestart = GameObject.Find("Player").GetComponent<HealthBar>().healthDrain = healthDrainDefault;
        }
    }
}
