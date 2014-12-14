using UnityEngine;
using System.Collections;

public class GunEntity : MonoBehaviour {
    
    public enum GunType
    {
        SemiAuto,
        Burst,
        Auto
    }

    public Transform shootOrigin;
    public GameObject shootObject;

    public GunType typeOfGun;
    [HideInInspector]
    public float roundPerMinute;
    public bool hasTriggerBeenRelease = true;
    protected float secondsBetweenShoots;
    protected float nextPossibleShoot;


    // Handle audio
    protected AudioSource shootAudioEntity;

    // Handling damage
    [HideInInspector]
    public int shootLifeTime = 1;
    [HideInInspector]
    public float stunDelay = 1;
    [HideInInspector]
    public float shootSpeed = 0.5f;

    // Level handling
    [HideInInspector]
    public int levelWeapon = 0;
    [HideInInspector]
    public int maxLevel = 3;

    // Use this for initialization
    public virtual void Start()
    {
        secondsBetweenShoots = 60 / roundPerMinute;
        nextPossibleShoot = 0;
        shootAudioEntity = GetComponent<AudioSource>();
    }

    public virtual void Shoot(int playerNumber)
    {
        if (CanShoot())
        {
            shootAudioEntity.Play();

            // Create Object
            ShootEntity shoot = ((GameObject)Instantiate(shootObject, shootOrigin.position, this.transform.rotation)).GetComponent<ShootEntity>();
            shoot.playerOrigin = playerNumber;
            shoot.destroyObjectAfterDelay(shootLifeTime);
            shoot.stunDelay = stunDelay;
            shoot.speed = shootSpeed;

            // Compute time for enabling next shot (Mode Auto Only)
            nextPossibleShoot = Time.time + secondsBetweenShoots;
        }
    }

    public bool CanShoot()
    {
        bool canShoot = Time.time > nextPossibleShoot && levelWeapon > 0;

        if (typeOfGun == GunType.SemiAuto)
            canShoot = canShoot && hasTriggerBeenRelease;

        return canShoot;
    }

    public void WeaponLevelUp()
    {
        if (levelWeapon < maxLevel)
        {
            levelWeapon++;
            if (levelWeapon == 1)
            {
                //Debug.Log("LEVEL 1");
                shootLifeTime = 1;
                shootSpeed = 0.5f;
                roundPerMinute = 15;
                secondsBetweenShoots = 60 / roundPerMinute;
            }
            if (levelWeapon == 2)
            {
                //Debug.Log("LEVEL 2");
                shootLifeTime = 2;
                shootSpeed = 0.6f;
                stunDelay = 1.5f;
                roundPerMinute = 30;
                secondsBetweenShoots = 60 / roundPerMinute;
            }
            if (levelWeapon == 3)
            {
                //Debug.Log("LEVEL 3");
                shootLifeTime = 3;
                shootSpeed = 0.7f;
                stunDelay = 2f;
                roundPerMinute = 45;
                secondsBetweenShoots = 60 / roundPerMinute;
                typeOfGun = GunType.Auto;
            }
        }
    }
}
