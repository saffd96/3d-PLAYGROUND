using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsCoinsEnded() || !other.gameObject.CompareTag("Player")) return;

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            Debug.Log($"Load Scene {SceneManager.GetActiveScene().buildIndex + 1}");
        }
        else
        {
            Debug.Log($"There is no more scenes");
        }
    }
}
