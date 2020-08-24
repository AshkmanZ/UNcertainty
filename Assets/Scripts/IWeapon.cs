using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Attack(Vector2 force,float attackSpeed = 1);
    void SetColor(Material color, string colorString);
}
