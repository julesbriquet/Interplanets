﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // HANDLING UI
    public GameObject playerUIGameObj;
    private UIPlayer playerUI;


    // HANDLING PLAYER CONTROL
    [HideInInspector]
    public PlayerController playerControl;
    public GameObject[] ActiveControlGameObj;

    // ENERGY LEVEL HANDLING
    public float energyLevel;
    public float energyToLightSpeed = 100;

    // WEAPON HANDLING
    public GameObject ActiveWeaponGameObj;

    // DAMAGE HANDLING
    public float delayDamage;
    private float timeUntilNextDamage;

    // ARMOR HANDLING
    [HideInInspector]
    public int armorLevel;
    [HideInInspector]
    public float armorResistance;

    private int maxArmorLevel = 3;
    public GameObject[] ActiveArmorGameObj;

	// Use this for initialization
	void Start () {
        GameObject UIobj = (GameObject)Instantiate(playerUIGameObj, playerUIGameObj.transform.position, playerUIGameObj.transform.rotation);
        playerUI = UIobj.GetComponent<UIPlayer>();
        playerUI.transform.SetParent(GameObject.FindGameObjectWithTag("UI").GetComponent<Transform>(), false);

        //UIobj.GetComponent<RectTransform>().rect.position = playerUIGameObj.GetComponent<RectTransform>().rect.position;
        

        energyLevel = 0;

        playerControl = GetComponent<PlayerController>();
        playerControl.controlLevel = 1;

        // Armor
        armorLevel = 1;
        armorResistance = 1;

        timeUntilNextDamage = Time.time;
	}

    // Update is called once per frame
    void Update()
    {
        energyLevel += Time.deltaTime;

        // Energy UI Change
        playerUI.SetEnergy((int)(100 / energyToLightSpeed * energyLevel));
    }

    public void LevelUpControl()
    {
        if (playerControl.controlLevel < playerControl.maxControlLevel)
        {

            if (playerControl.controlLevel - 1 < ActiveControlGameObj.Length)
                ActiveControlGameObj[playerControl.controlLevel - 1].SetActive(true);


            playerControl.controlLevel++;


            if (playerControl.controlLevel == 2)
            {
                rigidbody2D.drag = 4;
                playerControl.speed = new Vector2(14, 14);
                playerControl.rotationSpeed = 4;
            }
            if (playerControl.controlLevel == 3)
            {
                rigidbody2D.drag = 0;
                playerControl.speed = new Vector2(6, 6);
                playerControl.rotationSpeed = 50;
            }


            // UI Modif
            playerUI.SetEngine(100 / (playerControl.maxControlLevel - 1) * (playerControl.controlLevel - 1));
        }
    }

    public void LevelUpArmor()
    {
        if (armorLevel < maxArmorLevel)
        {

            if (armorLevel - 1 < ActiveControlGameObj.Length)
                ActiveArmorGameObj[armorLevel - 1].SetActive(true);

            armorLevel += 1;
            if (armorLevel == 2)
            {
                armorResistance = 1.5f;
            }
            if (armorLevel == 3)
            {
                armorResistance = 2f;
            }

            // UI Modif
            playerUI.SetShield(100 / (maxArmorLevel - 1) * (armorLevel - 1));
        }
    }

    public void TakeDamage(int damage)
    {
        if (timeUntilNextDamage < Time.time)
        {
            energyLevel -= damage / armorResistance;
            if (energyLevel < 0) energyLevel = 0;
            timeUntilNextDamage = delayDamage + Time.time;
        }
    }

    public void LevelUpWeapon()
    {
        // Activate weapon
        if (!playerControl.weapon)
        {
            ActiveWeaponGameObj.SetActive(true);
            playerControl.weapon = GetComponentInChildren<GunEntity>();

            playerControl.weapon.WeaponLevelUp();
        }
        else
            playerControl.weapon.WeaponLevelUp();

        // UI Modif
        playerUI.SetWeapon(100 / (playerControl.weapon.maxLevel) * (playerControl.weapon.levelWeapon ));
    }

    

    public void GetEnergy(int energy)
    {
        this.energyLevel += energy;
    }

    public int GetPlayerNumber()
    {
        return this.playerControl.playerNumber;
    }

    public void GetStun(float secondsInStun)
    {
        playerControl.stunTime = Time.time + secondsInStun;
    }
}
