using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image HealthFill;
    public float halfValue;
    public static int Health;
    public int startHealth;
    public static HealthUI instance;

    public bool canEat => Health < startHealth;

    private void Start()
    {
        instance = this;
        Health = startHealth;
    }

    public void TakeDamage(int damage, out int HP)
    {
        HealthFill.fillAmount -= halfValue * damage;
        Health -= damage;
        HP = Health;
    }

    public void GetHealth(int health, out int HP)
    {
        HealthFill.fillAmount += halfValue * health;
        Health += health;
        HP = Health;
    }
}
