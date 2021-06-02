using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeClass : Monster
{
    public override IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("Fly Forward", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("Fly Forward", true);
                    animator.SetBool("Back", false);
                    break;
                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetTrigger("Sting Attack");
                    animator.SetBool("Fly Forward", false);
                    break;
                case MonsterState.back:
                    nvAgent.destination = backpos;
                    nvAgent.isStopped = false;
                    animator.SetBool("Fly Forward", true);
                    animator.SetBool("Back", true);
                    break;
            }
            yield return null;
        }
    }
}
