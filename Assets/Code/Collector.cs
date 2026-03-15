using UnityEngine;

public class Collector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectibleSFX);
        }
    }
}
