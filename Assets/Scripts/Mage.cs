using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public ParticleSystem particle;

    public float shootCD = 3;
    float timer;

    public float speed = 3;
    Animator anim;
    Transform Player;

    Vector2 movePoint;

    public float maxDistance,minDistance;

    Vector2 randomPoint => (Random.insideUnitCircle * (speed / 2)).normalized;


    private void Start()
    {
        TryGetComponent(out anim);
        Player = FindObjectOfType<PlayerMovement>().transform;
        StartCoroutine(changePoint());
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.position) < maxDistance)
        {
            if (Vector2.Distance(transform.position, Player.position) > minDistance)
            {
                Vector2 dir = Player.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * speed);

                anim.SetFloat("Speed", speed / 2);
                anim.SetBool("Moving", true);
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = shootCD;
                Shoot(Player.position-particle.transform.position);
            }
        }
        else 
        {
            anim.SetFloat("Speed", speed / 4);

            if (Vector2.Distance(transform.position, movePoint) < 0.6f)
            {
                movePoint = nextPoint();
            }

            transform.Translate((movePoint - (Vector2)transform.position).normalized * Time.deltaTime * speed/2);

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
        movePoint = randomPoint;
        yield return new WaitForSeconds(3);
        StartCoroutine(changePoint());
    }
    void Shoot(Vector2 dir)
    {
        particle.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir.normalized);
        particle.Emit(1);

    }
}
