using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int healthAmount;
    public AudioClip audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetEaten(collision.gameObject);
        }
    }

    void GetEaten(GameObject player)
    {
        if (!HealthUI.instance.canEat)
            return;

        player.GetComponent<PlayerStats>().IncreaseHealth(healthAmount);
        Destroy(gameObject);
    }
}
