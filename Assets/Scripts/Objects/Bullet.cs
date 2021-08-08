using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed;
    [SerializeField] private float liveTime;

    private void Start()
    {
        Destroy(gameObject, liveTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {     
            Destroy(gameObject);
            return;
        }
        var contactPoint = other.GetContact(0);

        other.gameObject.GetComponent<Enemy>().ApplyDamage(damage, contactPoint);
        
        Destroy(gameObject);
    }
}
