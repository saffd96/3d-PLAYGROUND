using UnityEngine;

public class Coin : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //play sound;
            GameManager.Instance.maxCoins++;
            Destroy(gameObject);
        }
    }
}
