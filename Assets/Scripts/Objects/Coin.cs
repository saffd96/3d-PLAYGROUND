using UnityEngine;

public class Coin : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PLaySfx(SfxType.Coin);
            GameManager.Instance.MAXCoins++;
            Destroy(gameObject);
        }
    }
}
