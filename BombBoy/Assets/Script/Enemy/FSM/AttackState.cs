using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];
    }

    public override void OnUpdata(Enemy enemy)
    {
        if (enemy.hasBoom)
            return;
        if (enemy.attackList.Count == 0)
        {
            enemy.TransitionToState(enemy.patrolState);
        }
        if (enemy.attackList.Count > 1)
        {
            foreach (var item in enemy.attackList)
            {
                if (Mathf.Abs(enemy.transform.position.x - item.position.x) <
                Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                {
                    enemy.targetPoint = item;
                }
            }
        }
        if (enemy.attackList.Count == 1)
        {
            enemy.targetPoint = enemy.attackList[0];
        }
        if (enemy.targetPoint.CompareTag("Player"))
        {
            enemy.AttackAction();
        }
        if (enemy.targetPoint.CompareTag("Bomb"))
        {
            enemy.SkillAction();
        }
        enemy.MoveToTarget();
    }
}
