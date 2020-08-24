using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    public Image [] slots;
    Image[] slotParents;

    public GameObject[] weapons;
    public static WeaponHandler instance;
    PlayerArms player;
    private void Start()
    {
        player = FindObjectOfType<PlayerArms>();
        weapons = new GameObject[slots.Length];
        slotParents = new Image[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slotParents[i] = slots[i].transform.parent.GetComponent<Image>();
        }
        instance = this;
    }
    public void PickWeapon(Weapon weapon)
    {
        int wpnIndex = Array.IndexOf(weapons, null);
        if (wpnIndex >= 0)
        {
            slots[wpnIndex].sprite = weapon.sprite;
            weapons[wpnIndex] = weapon.gameObject;
            weapon.GetComponent<Collider2D>().enabled = false;
            weapon.transform.SetParent(player.transform);
            weapon.transform.position = player.transform.position;
            weapon.gameObject.SetActive(false);
        }
    }

    void SetColorByIndex(Color color, int index)
    {
        Image parent = Array.Find(slotParents, a => a.color == color);

        if (parent != null)
        {
            parent.color = Color.white;
        }
        slotParents[index].color = color;
    }

    public void SetColorByObject(Color color, GameObject weapon)
    {
        int index = Array.IndexOf(weapons, weapon);

        if (index < 0)
            return;

        Image parent = Array.Find(slotParents, a => a.color == color);
        if (parent != null)
        {
            parent.color = Color.white;
        }

        slotParents[index].color = color;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (weapons[0] != null)
            {
                Color color;
                player.ChangeWeapon(weapons[0].transform, out color);
                SetColorByIndex(color, 0);
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (weapons[1] != null)
            {
                Color color;
                player.ChangeWeapon(weapons[1].transform, out color);
                SetColorByIndex(color, 1);
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (weapons[2] != null)
            {
                Color color;
                player.ChangeWeapon(weapons[2].transform, out color);
                SetColorByIndex(color, 2);
            }
        }
        if (Input.GetKeyDown("4"))
        {
            if (weapons[3] != null)
            {
                Color color;
                player.ChangeWeapon(weapons[3].transform, out color);
                SetColorByIndex(color, 3);
            }
        }
    }
}
