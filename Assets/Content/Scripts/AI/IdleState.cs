using UnityEngine;
public class IdleState : AIState
{
    public IdleState(EnemyCharacter enemy) : base(enemy)
    {
        StateType = AIStateType.Idle;
    }

    public override void Enter()
    {
        Debug.Log($"{enemy.Data.CharacterName} перешел в состояние Idle");
    }

    public override void Update()
    {
        
        if (enemy.Target != null && enemy.Target.Data.IsAlive())
        {
            float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.Target.transform.position);

            
            if (distanceToTarget <= enemy.ChaseRange)
            {
                enemy.ChangeState(new ChaseState(enemy));
            }
        }
    }

    public override void Exit()
    {
    }
}
