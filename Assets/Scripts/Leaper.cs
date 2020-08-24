using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaper : MonoBehaviour
{
    public float speed = 1;
    Animator anim;
    public LayerMask bullet;
    Transform Player;
    PlayerStats playerStats;

    Vector2 movePoint;
    public bool following;

    bool canMove;

    int enemyType;

    private void Start()
    {
        TryGetComponent(out anim);
        Player = FindObjectOfType<PlayerMovement>().transform;
        playerStats = Player.GetComponent<PlayerStats>();
        following = false;
        anim.SetBool("Moving", true);
        canMove = true;
        movePoint = nextPoint();
        StartCoroutine("speedUp");

    }
    IEnumerator speedUp()
    {
        speed *= 2;
        yield return new WaitForSeconds(1.4f);
        speed /= 2;
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.position);

        
        if (distance < 4)
        {
            if (distance < 0.85f)
            {
                transform.position = this.transform.position;
            }
            else
            {
                Vector2 dir = Player.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * speed*2.5f);

                anim.SetFloat("Speed", speed *1.4f);
            }
            following = true;
        } else
        {
            following = false;
            anim.SetFloat("Speed", speed / 1.3f);

            if (Vector2.Distance(transform.position, movePoint) < 0.6f)
            {
                movePoint = nextPoint();
            }

            transform.Translate((movePoint - (Vector2)transform.position).normalized * Time.deltaTime * speed);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        movePoint = nextPoint();

    }

    Vector2 lastPoint;
    Vector2 nextPoint()
    {
        Vector2 point;
        do
        {
            point = EnemyHandler.waypoint;
        } while (point == lastPoint);
        lastPoint = point;
        return point;
    }
    IEnumerator changePoint()
    {
        movePoint = nextPoint();
        yield return new WaitForSeconds(3);
        StartCoroutine(changePoint());
    }

}
