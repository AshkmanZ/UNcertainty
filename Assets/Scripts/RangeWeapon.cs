 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour,IWeapon
{
    ParticleSystem shooter;
    public float attackDelay;
    bool canAttack;

    ParticleShooter particleShooter;

    public GameObject projectile;

    public int damage;

    string color;
    public SpriteRenderer sprite;

    private void Start()
    {
        particleShooter = GetComponentInChildren<ParticleShooter>();
        shooter = GetComponentInChildren<ParticleSystem>();
        particleShooter.damage = damage;
    }

    private void OnEnable()
    {
        canAttack = true;
    }

    public void Attack(Vector2 force, float attackSpeed = 1)
    {
        if (!canAttack)
            return;
        StartCoroutine(chargeAttack(attackSpeed));
        shooter.Emit(1);
    }

    IEnumerator chargeAttack(float attackSpeed)
    {
        canAttack = false;
        Debug.Log(attackDelay * attackSpeed);
        yield return new WaitForSeconds(attackDelay * attackSpeed);
        canAttack = true;
    }

    public void SetColor(Material color,string colorString)
    {
        sprite.material = color;
        this.color = colorString; //change to material
        particleShooter.color = this.color;
        particleShooter.mat = color;
    }
}
