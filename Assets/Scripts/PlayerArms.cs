using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    public WeaponSlot LeftSlot, RightSlot;
    bool leftOn;
    Camera main;

    WeaponSlot activeSlot;
    bool emptyHanded;

    public Material Blue, Red;
    SpriteRenderer sprite;

    LineRenderer line;
    private void Start()
    {
        TryGetComponent(out line);
        sprite = GetComponentInChildren<SpriteRenderer>();

        main = Camera.main;
        leftOn = false;
        if (LeftSlot.activeWeapon== null && RightSlot.activeWeapon == null)
        {
            emptyHanded = true;
        } else
        {
            emptyHanded = false;
        }
    }

    private void Update()
    {
        if (emptyHanded)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            SwitchArm();
        }

        Vector3 cursor = main.ScreenToWorldPoint(Input.mousePosition);
        line.SetPosition(1, cursor - transform.position);

        activeSlot.activeWeapon.rotation = Quaternion.LookRotation(Vector3.forward, (cursor - (Vector3)activeSlot.transform.position).normalized);

        if (Input.GetMouseButton(0))
        {
            activeSlot.activeInterface.Attack(force:(Vector2)cursor-(Vector2)transform.position);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            WeaponHandler.instance.PickWeapon(collision.GetComponent<Weapon>());

            #region StartEmpty
            emptyHanded = false;
            if (RightSlot.activeWeapon == null) //if player doesn't have any active weapons
            {
                RightSlot.activeWeapon = collision.transform;
                if (activeSlot == null)
                { //it might not be null because other slot is active
                    activeSlot = RightSlot;
                    sprite.material = Red;

                }
                else
                {
                    RightSlot.activeWeapon.gameObject.SetActive(false);
                }
                WeaponHandler.instance.SetColorByObject(Color.red, RightSlot.activeWeapon.gameObject);

            }
            else if (LeftSlot.activeWeapon == null)
            {
                LeftSlot.activeWeapon = collision.transform;
                if (activeSlot == null)
                {
                    activeSlot = LeftSlot;
                    sprite.material = Blue;

                }
                else
                {
                    LeftSlot.activeWeapon.gameObject.SetActive(false);
                }
                WeaponHandler.instance.SetColorByObject(Color.blue, LeftSlot.activeWeapon.gameObject);

            }
            #endregion
        }
    }

    void SwitchArm()
    {
        leftOn = !leftOn;
        if (leftOn)
        {
            if (LeftSlot.activeWeapon == null)
            {
                if (RightSlot.activeWeapon == null)
                    return;

                LeftSlot.activeWeapon = RightSlot.activeWeapon;
                RightSlot.activeWeapon = null;
            }
            sprite.material = Blue;
            activeSlot = LeftSlot;
            activeSlot.activeWeapon.gameObject.SetActive(true);
            if (RightSlot.activeWeapon!=null)
                RightSlot.activeWeapon.gameObject.SetActive(false);
        } else
        {
            if (RightSlot.activeWeapon == null)
            {
                if (LeftSlot.activeWeapon == null)
                    return;

                RightSlot.activeWeapon = LeftSlot.activeWeapon;
                LeftSlot.activeWeapon = null;
            }
            sprite.material = Red;
            activeSlot = RightSlot;
            activeSlot.activeWeapon.gameObject.SetActive(true);
            if (LeftSlot.activeWeapon!=null)
                LeftSlot.activeWeapon.gameObject.SetActive(false);
        }

    }
    public void ChangeWeapon(Transform weapon, out Color color)
    {
        Transform tmpWpn = weapon;
        if (leftOn)
        {
            if (LeftSlot.activeWeapon != null && RightSlot.activeWeapon == tmpWpn)
            {

                RightSlot.activeWeapon = LeftSlot.activeWeapon;
                WeaponHandler.instance.SetColorByObject(Color.red, RightSlot.activeWeapon.gameObject);
            }
            else
            {
                LeftSlot.EmptySlot();
            }
            if (RightSlot.activeWeapon != null)
            {
                RightSlot.activeWeapon.gameObject.SetActive(false);
            }
            LeftSlot.activeWeapon = weapon;
            color = Color.blue;
            sprite.material = Blue;

        }
        else
        {
            if (LeftSlot.activeWeapon == tmpWpn)
            {
                LeftSlot.activeWeapon = RightSlot.activeWeapon;
                WeaponHandler.instance.SetColorByObject(Color.blue, LeftSlot.activeWeapon.gameObject);
            }
            else
            {
                RightSlot.EmptySlot();
            }
            if (LeftSlot.activeWeapon != null)
            {
                LeftSlot.activeWeapon.gameObject.SetActive(false);
            }
            RightSlot.activeWeapon = weapon;
            color = Color.red;
            sprite.material = Red;

        }
    }
}
