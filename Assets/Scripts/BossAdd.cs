using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAdd : MonoBehaviour
{
    public float speed;
    float Speed;

    Transform Player;

    public EasyBoss boss;

    private void Awake()
    {
        ColorInfo color = EnemyHandler.instance.colorInfo;
        GetComponent<EnemyStats>().material = color.mat;
        gameObject.tag = color.color;
    }
    private void Start()
    {

        Speed = Random.Range(speed * 0.8f, speed * 1.2f);

        Player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.position) > 0.85f)
        {
            transform.Translate((Player.position - transform.position).normalized * Time.deltaTime * Speed);
        }
    }

    private void OnDestroy()
    {
        boss.activeAdds.Remove(gameObject);
    }
}
