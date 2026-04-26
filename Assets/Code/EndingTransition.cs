using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingTransition : MonoBehaviour
{
    public GameObject EndTransition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndTransition.SetActive(true);
            StartCoroutine("LoadScene");
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(2);
    }
}
