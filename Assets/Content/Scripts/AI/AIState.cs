using UnityEngine;
public abstract class AIState
{
    protected EnemyCharacter enemy;
    public AIStateType StateType { get; protected set; }

    public AIState(EnemyCharacter enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
