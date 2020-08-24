using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 target;

    Vector3 dir;
    private void Start()
    {
        dir = target - (Vector2)transform.position;
    }
    private void Update()
    {
        //transform.position += dir*Time.deltaTime*speed;
        transform.Translate(target*Time.deltaTime*speed);
    }
}
