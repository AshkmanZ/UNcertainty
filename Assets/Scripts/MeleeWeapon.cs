using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public Transform hitArea;
    public float attackDelay = 1;
    public float attackRange = 1;
    Animator anim;

    bool canAttack;
    public int damage = 1;
    string color;
    AudioSource audio;
    public SpriteRenderer sprite;

    private void OnEnable() //fudge me
    {
        canAttack = true;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void Attack(Vector2 force,float attackSpeed = 1)
    {
        if (!canAttack)
            return;

        audio.Play();
        StartCoroutine(chargeAttack(attackSpeed));

        anim.SetTrigger("Attack");

        if (hitEnemies().Length > 0)
        {
            foreach (var enemy in hitEnemies())
            {
                if (enemy.CompareTag(color))
                    enemy.GetComponent<EnemyStats>().TakeDamage(damage, force);
            }
        }

    }

    Collider2D[] hitEnemies()
    {
        return Physics2D.OverlapCircleAll(hitArea.position, attackRange, 1<<8);
    }
    IEnumerator chargeAttack(float attackSpeed)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay * attackSpeed);
        canAttack = true;
    }
    public void SetColor(Material color, string colorString)
    {
        sprite.material = color;
        this.color = colorString;
    }
}
