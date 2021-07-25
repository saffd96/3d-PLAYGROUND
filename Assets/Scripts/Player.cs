using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField] private GameObject deathVfx;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private float deathDelay = 01f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameLoser"))
        {
            if (deathVfx != null)
            {
                Instantiate(deathVfx, transform.position, Quaternion.identity);
            }

            //play sound;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(deathDelay);

        onDeath?.Invoke();
    }
}