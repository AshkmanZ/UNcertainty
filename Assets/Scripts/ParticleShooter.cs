using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShooter : MonoBehaviour
{
    public int damage = 1;
    public string color;
    public Material mat
    {
        set
        {
            GetComponent<ParticleSystemRenderer>().trailMaterial = value;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 8 )
        {
            if (other.gameObject.CompareTag(color)) {
                other.GetComponent<EnemyStats>().TakeDamage(damage, Vector2.zero);
            }
        }
    }
}
