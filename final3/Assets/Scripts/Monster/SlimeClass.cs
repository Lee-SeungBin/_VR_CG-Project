using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeClass : Monster
{
    public override IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("Walk Forward", false);
                    animator.SetBool("Run Forward", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("Run Forward", true);
                    animator.SetBool("Back", false);
                    break;
                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetTrigger("Attack");
                    animator.SetBool("Run Forward", false);
                    break;
                case MonsterState.back:
                    nvAgent.destination = backpos;
                    nvAgent.isStopped = false;
                    animator.SetBool("Walk Forward", true);
                    animator.SetBool("Back", true);
                    break;
            }
            yield return null;
        }
    }

    public override void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            if(Random.Range(1,100) <= 20)
            {
                hp -= 25;
                animator.SetTrigger("Take Damage");
            }
            else
            {
                Debug.Log("물리 데미지 무효");
            }

            if (hp <= 0)
            {
                MonsterDie();
            }
        }
    }
}
