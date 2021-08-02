using System;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform enter;
    [SerializeField] private Transform exit;
    
    [SerializeField] private GameObject exitVfx;
    [Space]
    [SerializeField] private UnityEvent onEnter;

    private Vector3 exitPosition;
    private void Awake()
    {
        exitPosition = exit.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(enter.position, 0.25f);
        Gizmos.DrawSphere(exit.position, 0.25f);
        Gizmos.DrawLine(enter.position, exit.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (exitVfx != null)
        {
            Instantiate(exitVfx, new Vector3(exitPosition.x,exitPosition.y+2,exitPosition.z), Quaternion.identity);
        }

        onEnter?.Invoke();
    }
}
