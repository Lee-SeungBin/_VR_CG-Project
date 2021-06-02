using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    #region 선언
    public enum MonsterState { idle, trace, attack, die, back, skill };
    public MonsterState monsterState = MonsterState.idle;
    public Transform monsterTr;
    public Transform playerTr;
    public UnityEngine.AI.NavMeshAgent nvAgent;
    public Animator animator;
    public bool isDie = false;
    public bool back = false;
    public Vector3 backpos;
    public float spawnDelay;
    public int sethp;
    public ItemDrop itemdrop;
    #endregion

    [SerializeField]
    public float traceDist;  //시야
    public float attackDist; //공격사거리

    [SerializeField]
    public GameObject monster;
    public int Damage;
    public int hp;       //피
    public string grade; //몬스터 등급

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        sethp = hp;
        backpos = this.gameObject.transform.position;

        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());

    }

    public void Update()
    {
        MonsterReSpawn();
    }

    IEnumerator CheckMonsterState() //몬스터 상태 체크
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);
            if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else if (dist >= traceDist)
            {
                monsterState = MonsterState.back;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }

    public abstract IEnumerator MonsterAction();

    public virtual void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            hp -= 25;

            if (hp <= 0)
            {
                MonsterDie();
            }
            else
            {
                animator.SetTrigger("Take Damage");
            }
        }
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();

        nvAgent.isStopped = true;
        animator.SetTrigger("IsPlayerDie");
    }

    public void MonsterDie()
    {
        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.isStopped = true;
        animator.SetTrigger("Die");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
        itemdrop.Drop();
    }

    public void MonsterReSpawn()
    {
        if (grade == "Normal" && isDie == true)
        {
            spawnDelay += Time.deltaTime;
            if (spawnDelay >= 5)
            {
                isDie = false;
                hp = sethp;
                gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

                foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
                {
                    coll.enabled = true;
                }
                spawnDelay = 0;
                Instantiate(monster, backpos, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }

        if (grade == "Rare" && isDie == true)
        {
            spawnDelay += Time.deltaTime;
            if (spawnDelay >= 10)
            {
                isDie = false;
                hp = sethp;
                gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

                foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
                {
                    coll.enabled = true;
                }
                spawnDelay = 0;
                Instantiate(monster, backpos, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}
