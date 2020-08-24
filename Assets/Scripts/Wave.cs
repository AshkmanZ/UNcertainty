using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Wave", order =1)]
public class Wave : ScriptableObject
{
    public List<GameObject> enemies;
    public float delay;

    public bool isBoss;
}
