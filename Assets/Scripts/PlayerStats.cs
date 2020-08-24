using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int Health;

    Rigidbody2D rb;
    public GameObject deathEffect;
    ParticleSystem hitEffect;
    public Material material;

    PlayerMovement move;

    public float forceZ;

    bool immune;

    public Canvas gameoverScreen;

    private void Start()
    {
        immune = false;
        Health = HealthUI.Health;
        TryGetComponent(out rb);
        hitEffect = GetComponent<ParticleSystem>();

        if (material == null)
            material = GetComponent<SpriteRenderer>().material;

        hitEffect.GetComponent<ParticleSystemRenderer>().material = material;
        move = GetComponent<PlayerMovement>();

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.parent.gameObject.layer == 8)
        {
            TakeDamage(1, Vector2.zero);
        }
    }
    public void TakeDamage(int damage, Vector2 force)
    {
        if (immune)
            return;

        StartCoroutine(immunity());
        HealthUI.instance.TakeDamage(damage, out Health);
        StartCoroutine(showEffect());
        if (Health <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystemRenderer>().material = material;

            Die();
        }
        if (force != Vector2.zero)
        {
            StartCoroutine(Knockback(force));
        }
    }

    void Die()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        move.enabled = false;
        GetComponent<PlayerArms>().enabled = false;
        gameoverScreen.enabled = true;

    }

    public void IncreaseHealth(int health)
    {
        HealthUI.instance.GetHealth(health, out Health);
    }
    IEnumerator immunity()
    {
        immune = true;
        yield return new WaitForSeconds(0.6f);
        immune = false;
    }
    IEnumerator Knockback(Vector2 force)
    {
        move.canMove = false;
         rb.AddForce(force.normalized * forceZ, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.04f);
        move.canMove = true;
    }

    IEnumerator showEffect()
    {
        yield return new WaitForSeconds(0.07f);
        hitEffect.Play();
    }

}
