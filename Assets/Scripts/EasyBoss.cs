using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyBoss : MonoBehaviour
{
    Vector2 center;
    bool canMove;
    Transform player;
    public float speed;
    bool castingSpecial, haveAdds;

    EnemyStats stats;

    public ParticleSystem specialAbility;

    public GameObject Add;

    public List <GameObject> activeAdds;

    public Material immuneMat;

    SpriteRenderer sprite;

    Image HealthBar;
    public enum BossPhases
    {
        patrol,
        adds,
        special
    }
    public BossPhases phase;

    public float specialCD;
    float CDtimer;

    public float changeCD;
    float changeTimer;

    float disVal;
    private void Start()
    {
        gameDone = GameObject.Find("Gamedone").GetComponent<Canvas>();
        HealthBar = UIManager.instance.bossHealth;
        HealthBar.GetComponentInParent<Canvas>().enabled = true;
        phase = BossPhases.patrol;
        center = GameObject.Find("StageCenter").transform.position;
        player = FindObjectOfType<PlayerMovement>().transform;
        stats = GetComponent<EnemyStats>();
        sprite = GetComponent<SpriteRenderer>();

        canMove = true;
        castingSpecial = false;
        haveAdds = false;
        CDtimer = specialCD;
        changeTimer = changeCD;


        //ChangeInfo();
    }

    
    private void Update()
    {
        if (phase == BossPhases.patrol)
        {
            if (CDtimer > 0)
            {
                CDtimer -= Time.deltaTime;
            } else
            {
                int dice = Random.Range(0, 10);
                if (dice < 5)
                {
                    StartCoroutine(SummonAdds(3));
                    phase = BossPhases.adds;
                }else
                {
                    phase = BossPhases.special;

                }
                CDtimer = specialCD;
            }
            if (Vector2.Distance(transform.position, player.position) > 0.85f)
            {
                transform.Translate((player.position - transform.position).normalized * Time.deltaTime * speed);
            }
        }
        else if (phase == BossPhases.special)
        {
            if (Vector2.Distance(transform.position, center) > 1.5f)
            {
                transform.Translate((center - (Vector2)transform.position).normalized * Time.deltaTime * speed);
            } else
            {
                StartCoroutine("centerAbility");
            }
        } else if (phase == BossPhases.adds)
        {
            if (Vector2.Distance(transform.position, player.position) > 0.85f)
            {
                transform.Translate((player.position - transform.position).normalized * Time.deltaTime * speed);
            }
        }

        if (!haveAdds)
        {
            if (changeTimer > 0)
            {
                changeTimer -= Time.deltaTime;
            } else
            {
                ChangeInfo(true);
            }
        }
    }

    void ChangeInfo(bool getValue)
    {
        if (getValue)
        {
            disVal = sprite.material.GetFloat("_Dissolve");
            Debug.Log("set");
        }
        ColorInfo color = EnemyHandler.instance.colorInfo;
        stats.SetInfo(color, disVal);
        changeTimer = changeCD;
    }

    IEnumerator SummonAdds(int addCount)
    {
        phase = BossPhases.adds;
        disVal = sprite.material.GetFloat("_Dissolve");

        sprite.material = immuneMat;
        gameObject.tag = "Untagged";
        haveAdds = true;

        for (int i = 0; i < addCount; i++)
        {
            Vector2 spawnPos = EnemyHandler.waypoint;
            GameObject add = Instantiate(Add, spawnPos, Quaternion.identity);
            add.GetComponent<BossAdd>().boss = this;
            activeAdds.Add(add);

        }
        do
        {
            stats.immune = true;

            yield return new WaitForSeconds(0.1f);
        } while (activeAdds.Count>0);
 
            stats.immune = false;
            phase = BossPhases.patrol;
            ChangeInfo(false);
            haveAdds = false;

        
    }
    IEnumerator centerAbility()
    {
        if (castingSpecial)
            yield break;

        castingSpecial = true;
        specialAbility.Play();
        yield return new WaitForSeconds(specialAbility.duration);
        phase = BossPhases.patrol;
        castingSpecial = false;

    }
    Canvas gameDone;
    private void OnDestroy()
    {
        gameDone.enabled = true;
    }

}
