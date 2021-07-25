using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsCoinsEnded() && other.gameObject.CompareTag("Player"))
        {
            //load Next Level
        }
    }
}
