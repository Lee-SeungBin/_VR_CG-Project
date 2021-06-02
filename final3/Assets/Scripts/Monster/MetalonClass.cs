using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalonClass : Monster
{
    public override IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
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
                    animator.SetTrigger("Smash Attack");
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
}
