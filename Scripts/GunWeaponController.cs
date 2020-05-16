using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GunWeaponController : MonoBehaviour
{
    [SerializeField]
    public GunWeapon currentGunWeapon;

    protected CrossHair theCrossHair;

    protected float currentFireRate;

    //Stats Bool
    protected bool isReload = false;
    //in case ReloadTime is not accuracy.. try to use currentGun.anim.GetCurrentAnimatorStateInfo(0).IsName ("Reload")
  
    protected bool isInspectHandGun = false;

    //[SerializeField]
    //private Vector3 originPos; // original position.

    public Button fireButton;
    public Button reloadButton;
    public Button fineSightButton;

    protected bool fireButtonPressed = false;
    protected bool reloadButtonPressed = false;
    //private bool fineSightButtonPressed = false;
    
    // Fine Sight Stats
    protected bool isFineSightMode = false;
    

    [Tooltip("How much force is applied to the bullet when shooting.")]
	public float bulletForce = 400;

    protected AudioSource audioSource;

    [SerializeField]
    protected GameObject hitEffectPrefab;

    [System.Serializable]
    public class prefabs
    {
        [Header("Prefabs")]
        public Transform bulletPrefab;
        public Transform casingPrefab;
    }

    public prefabs Prefabs;

    [System.Serializable]
	public class spawnpoints
	{  
		[Header("Spawnpoints")]
		//Array holding casing spawn points 
		//Casing spawn point array
		public Transform casingSpawnPoint;
		//Bullet prefab spawn from this point
		public Transform bulletSpawnPoint;
		//Grenade prefab spawn from this point        
	}
	public spawnpoints Spawnpoints;

    [System.Serializable]
	public class soundClips
	{
		public AudioClip shootSound;
		public AudioClip reloadSoundOutOfAmmo;
		public AudioClip aimSound;
	}
	public soundClips SoundClips;

    protected RaycastHit hitInfo;
    [SerializeField]
    protected Camera theCam;

    //Add cyju
    /*
    [Tooltip("The bullet model inside the mag, not used for all weapons.")]
	public SkinnedMeshRenderer bulletInMagRenderer;
    */


    protected void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }    
    protected void TryFire()
    {
        if(fireButtonPressed && currentFireRate <= 0 && !isReload && !isInspectHandGun)
        {
            Fire();
        }
    }
    protected void Fire()
    {        
        if(!isReload && !isInspectHandGun)
        {
            if(currentGunWeapon.currentBulletCount > 0)
            {
                Shoot();
            }
            else
            {
                CancelFineSight();                
                StartCoroutine(ReloadCoroutine());
            }  
        }      
    }

    protected void TryReload()
    {
        if(reloadButtonPressed && !isReload && currentGunWeapon.currentBulletCount < currentGunWeapon.reloadBulletCount && !isInspectHandGun)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload()
    {
        if(isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }

    protected IEnumerator ReloadCoroutine()
    {
        if(currentGunWeapon.carryBulletCount > 0)
        {
            isReload = true;
            
            PlaySE(SoundClips.reloadSoundOutOfAmmo);

            currentGunWeapon.anim.SetTrigger("Reload");

            currentGunWeapon.carryBulletCount += currentGunWeapon.currentBulletCount;
            currentGunWeapon.currentBulletCount = 0;

            yield return new WaitForSeconds(currentGunWeapon.reloadTime);

            if(currentGunWeapon.carryBulletCount >= currentGunWeapon.reloadBulletCount)
            {
                currentGunWeapon.currentBulletCount = currentGunWeapon.reloadBulletCount;
                currentGunWeapon.carryBulletCount -= currentGunWeapon.reloadBulletCount;
            }
            else
            {
                currentGunWeapon.currentBulletCount = currentGunWeapon.carryBulletCount;
                currentGunWeapon.carryBulletCount = 0;
            }
            isReload = false;
        }
        else
        {
            Debug.Log("No Bullet");
        }
    }
    protected abstract void Shoot();

    protected void Hit()
    {
/* CYJU later Gun Fire Accuracy
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward + 
                new Vector3(Random.Range(-theCrossHair.GetGunAccuracy() - currentGun.accuracy, theCrossHair.GetGunAccuracy() + currentGun.accuracy),
                            Random.Range(-theCrossHair.GetGunAccuracy() - currentGun.accuracy, theCrossHair.GetGunAccuracy() + currentGun.accuracy),
                            0),
                            out hitInfo, currentGun.range))
*/
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward,
                            out hitInfo, currentGunWeapon.range))
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitEffect, 1.0f);

            if(hitInfo.transform.tag == "NPC")
                hitInfo.transform.GetComponent<FlyDragon>().Damage(currentGunWeapon.damage, theCam.transform.position);       
        }  
    }

    //Show light when shooting, then disable after set amount of time
	protected IEnumerator MuzzleFlashLight () 
	{
		currentGunWeapon.muzzleflashLight.enabled = true;
        currentGunWeapon.muzzleflash.Play();
		yield return new WaitForSeconds (currentGunWeapon.lightDuration);
		currentGunWeapon.muzzleflashLight.enabled = false;
        currentGunWeapon.muzzleflash.Stop();
	}

    protected void TryFineSight()
    {
        if(isFineSightMode && !isReload && !isInspectHandGun)
        {
            FineSight();
        }
    }
    public void CancelFineSight()
    {
        if(isFineSightMode)
        {
            isFineSightMode = false;
            PlaySE(SoundClips.aimSound);

            currentGunWeapon.anim.SetBool("FineSightMode", isFineSightMode);
            theCrossHair.FineSightAnimation(isFineSightMode);
        }
    }

    protected void ReleaseFineSight()
    {
        if(!isFineSightMode)
        {
            PlaySE(SoundClips.aimSound);

            currentGunWeapon.anim.SetBool("FineSightMode", isFineSightMode);
            theCrossHair.FineSightAnimation(isFineSightMode);
        }
    }
    protected void FineSight()
    {      
        PlaySE(SoundClips.aimSound);

        currentGunWeapon.anim.SetBool("FineSightMode", isFineSightMode);
        theCrossHair.FineSightAnimation(isFineSightMode);
    }

    protected void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    protected void AnimationCheck()
    {
        if(currentGunWeapon.anim.GetCurrentAnimatorStateInfo(0).IsName ("InspectGun"))
            isInspectHandGun = true;
        else
            isInspectHandGun = false;           
    }

    public GunWeapon GetWeaponGun()
    {
        return currentGunWeapon;
    }

    public virtual void GunWeaponChange(GunWeapon _gunWeapon)
    {
        if(WeaponManager.currentWeapon != null)
        {
            
            WeaponManager.currentWeapon.gameObject.SetActive(false);
            WeaponManager.currentWeapon.go_MainUI.SetActive(false);
        }           

        currentGunWeapon = _gunWeapon;
        //WeaponManager.currentWeapon = currentGunWeapon.GetComponent<Transform>();
        WeaponManager.currentWeapon = currentGunWeapon;        
        WeaponManager.currentWeaponAnim = currentGunWeapon.anim;

        currentGunWeapon.transform.localPosition = Vector3.zero;
        currentGunWeapon.gameObject.SetActive(true);
        currentGunWeapon.go_MainUI.SetActive(true);
    }

    public void EventOnFireButtonPressed()
    {
        fireButtonPressed = true;
    }
    public void EventOnFireButtonReleased()
    {
        fireButtonPressed = false;
    }
    public void EventOnReloadButtonPressed()
    {
        reloadButtonPressed = true;
    }
    public void EventOnReloadButtonReleased()
    {
        reloadButtonPressed = false;
    }

    protected void OnFineSightButton()
    {
        isFineSightMode = !isFineSightMode;

        if(isFineSightMode)
            TryFineSight();
        else         
            ReleaseFineSight();
    }
}
