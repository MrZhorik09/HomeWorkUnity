using UnityEngine;

public class AttackState : AIState
{
    private float attackCooldown = 1f;
    private float lastAttackTime;

    public AttackState(EnemyCharacter enemy) : base(enemy)
    {
        StateType = AIStateType.Attack;
    }

    public override void Enter()
    {
        Debug.Log($"{enemy.Data.CharacterName} перешел в состояние Attack");
        lastAttackTime = -attackCooldown; 
    }

    public override void Update()
    {
        
        if (enemy.Target == null || !enemy.Target.Data.IsAlive())
        {
            enemy.ChangeState(new IdleState(enemy));
            return;
        }

        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.Target.transform.position);

      
        if (distanceToTarget > enemy.AttackRange)
        {
            enemy.ChangeState(new ChaseState(enemy));
            return;
        }

    
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            
            enemy.Attack(enemy.Target);
            lastAttackTime = Time.time;
        }
    }

    public override void Exit()
    {
    }
}
