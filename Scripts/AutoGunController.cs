using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGunController : GunWeaponController
{
    public static bool isActivate = false;

    private float lastFired;

    void Start()
    {
        currentGunWeapon.muzzleflashLight.enabled = false;

        audioSource = GetComponent<AudioSource>();
        theCrossHair = FindObjectOfType<CrossHair>();
        fineSightButton.onClick.AddListener(OnFineSightButton);
    }

    void Update()
    {
        if(isActivate)
        {
            AnimationCheck();
            GunFireRateCalc();
            TryFire();     
            TryReload();
        }
    }

    protected override void Shoot()
    {
        if(isFineSightMode)
        {
            currentGunWeapon.anim.Play("AimShoot", 0, 0.0f);
            theCrossHair.FireAnimation();
        }
        else
        {
            currentGunWeapon.anim.Play("Shoot",0,0.0f);
            theCrossHair.FireAnimation();
        }
        
        currentGunWeapon.currentBulletCount--;
        currentFireRate = currentGunWeapon.fireRate;
        
        PlaySE(SoundClips.shootSound);
        StartCoroutine(MuzzleFlashLight());

        var bullet = (Transform)Instantiate (
            Prefabs.bulletPrefab,
            Spawnpoints.bulletSpawnPoint.transform.position,
            Spawnpoints.bulletSpawnPoint.transform.rotation);

        //Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = 
        bullet.transform.forward * bulletForce;

        //Spawn casing prefab at spawnpoint
        Instantiate (Prefabs.casingPrefab, 
            Spawnpoints.casingSpawnPoint.transform.position, 
            Spawnpoints.casingSpawnPoint.transform.rotation);
        
        Hit();
	
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void GunWeaponChange(GunWeapon _gunWeapon)
    {
        base.GunWeaponChange(_gunWeapon);
        isActivate = true;
    }
}
