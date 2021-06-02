using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderClass : Monster
{
    public override IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("Move Forward Fast", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("Move Forward Fast", true);
                    animator.SetBool("Back", false);
                    break;
                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetTrigger("Claw Attack");
                    animator.SetBool("Move Forward Fast", false);
                    break;
                case MonsterState.back:
                    nvAgent.destination = backpos;
                    nvAgent.isStopped = false;
                    animator.SetBool("Move Forward Slow", true);
                    animator.SetBool("Back", true);
                    break;
            }
            yield return null;
        }
    }
}
