using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingDelay;

    private float shootingDelayCounter;

    private void FixedUpdate()
    {
        Shoot();
    }
    private void Shoot()
    {
        shootingDelayCounter += Time.deltaTime;

        if (!Input.GetButton("Fire1")) return;
        
        if (!(shootingDelayCounter >= shootingDelay)) return;
        
        var bullet = Instantiate(bulletPrefab, shootingPosition.position, shootingPosition.rotation);
        AudioManager.Instance.PLaySfx(SfxType.Shoot, transform); //начинаются лаги при выстреле
        shootingDelayCounter = 0;
    }
}
