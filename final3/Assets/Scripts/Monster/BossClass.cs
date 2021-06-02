using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BossClass : Monster
{
    #region 선언
    float cooltime;
    float noticetime;
    public GameObject skillobject;
    public GameObject noticeobject;
    public Transform noticeTr;
    #endregion

    public override IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    //nvAgent.isStopped = true;
                    break;
                case MonsterState.trace:
                    //nvAgent.destination = playerTr.position;
                    //nvAgent.isStopped = false;
                    transform.LookAt(playerTr.position);
                    BossSkill();
                    break;
                case MonsterState.attack:
                    //nvAgent.isStopped = true;
                    animator.SetTrigger("Stomp Attack");
                    BossSkill();
                    break;
            }
            yield return null;
        }
    }

    private new void Update()
    {
        if(null != GameObject.FindWithTag("notice"))
        {
            noticeTr = GameObject.FindWithTag("notice").GetComponent<Transform>();
        }
    }
    void BossSkill()
    {
        cooltime += Time.deltaTime;
        noticetime += Time.deltaTime;
        if (noticetime >= 4f)
        {
            animator.SetTrigger("Root Attack");
            Instantiate(noticeobject, playerTr.position, Quaternion.identity);
            noticetime = 0;
        }
        if (cooltime >= 4.03f)
        {
            if (null != GameObject.FindWithTag("notice"))
            {
                Instantiate(skillobject,
                 new Vector3(noticeTr.position.x,
                 noticeTr.position.y + 0.7f,
                 noticeTr.position.z),
                 Quaternion.identity);
                cooltime = 0;
            }
        }
    }
}
