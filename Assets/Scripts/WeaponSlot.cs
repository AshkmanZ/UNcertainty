using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public Material color;
    public string colorName;

    public IWeapon activeInterface;

    Transform ActiveWeapon;
    public Transform activeWeapon { 
        get { return ActiveWeapon; }
        set { 
            ActiveWeapon = value;
            if (value != null)
            {
                ActiveWeapon.position = transform.position;
                ActiveWeapon.SetParent(transform);
                ActiveWeapon.gameObject.SetActive(true);
                activeInterface = ActiveWeapon.GetComponent<IWeapon>();
                activeInterface.SetColor(color, colorName);
            }
        }
    }

    public void EmptySlot()
    {
        ActiveWeapon.parent = transform.parent;
        ActiveWeapon.gameObject.SetActive(false);
    }
}
