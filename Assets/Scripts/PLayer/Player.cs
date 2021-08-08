using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject deathVfx;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private float deathDelay = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameLoser"))
        {
            if (deathVfx != null)
            {
                Instantiate(deathVfx, transform.position, Quaternion.identity);
            }

            AudioManager.Instance.PLaySfx(SfxType.Death);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(deathDelay);

        onDeath?.Invoke();
    }
}
