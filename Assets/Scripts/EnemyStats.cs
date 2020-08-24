using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float startHealth;
    public float Health;

    Rigidbody2D rb;
    public GameObject deathEffect;
    ParticleSystem hitEffect;
    public Material material;
    Material spriteMat;
    SpriteRenderer sprite;
    WaveHandler waveHandler;

    public bool immune;
    public int damage = 1;

    public bool isBoss = false;
    Image HealthBar;
    AudioSource audioZ;
    private void Start()
    {
        audioZ = GetComponent<AudioSource>();
        if (isBoss)
        {
            HealthBar = UIManager.instance.bossHealth;
        }
        immune = false;

        waveHandler = FindObjectOfType<WaveHandler>();

        Health = startHealth;
        TryGetComponent(out rb);
        
        sprite = GetComponent<SpriteRenderer>();

        sprite.material = material;
        spriteMat = sprite.material;

        SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();
        if (childrenSprites.Length > 0)
        {
            foreach (var chSpr in childrenSprites)
            {
                chSpr.material = spriteMat;
            }
        }

        hitEffect = GetComponent<ParticleSystem>();
        hitEffect.GetComponent<ParticleSystemRenderer>().material = material;
    }
    
    public void SetInfo(ColorInfo info, float disValue)
    {
        sprite.material = info.mat;
        spriteMat = sprite.material;
        sprite.material.SetFloat("_Dissolve", disValue);

        material = info.mat;
        hitEffect.GetComponent<ParticleSystemRenderer>().material = material;

        gameObject.tag = info.color;
    }
    public void TakeDamage(int damage, Vector2 force)
    {
        if (immune)
            return;

        audioZ.Play();
        Health -= damage;
        if (isBoss)
        {
            HealthBar.fillAmount = Health / startHealth;
        }
        if (spriteMat.GetFloat("_Dissolve") >= 0.3f)
        {
            spriteMat.SetFloat("_Dissolve", Health / startHealth);
        }

        StartCoroutine(showEffect());
        if (Health <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystemRenderer>().material = material;
            waveHandler.EnemyDeath(gameObject);
            Destroy(gameObject);
        }
        if (force != Vector2.zero)
        {
            rb.AddForce((force).normalized * 10000, ForceMode2D.Impulse);
        }
    }
    IEnumerator showEffect()
    {
        yield return new WaitForSeconds(0.07f);
        hitEffect.Play();

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage, (collision.transform.position - transform.position));
        }
    }
}
