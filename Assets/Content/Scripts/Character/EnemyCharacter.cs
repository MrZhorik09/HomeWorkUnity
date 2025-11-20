using UnityEngine;

public class EnemyCharacter : Character
{
    [Header("AI Settings")]
    [SerializeField] private float chaseRange = 10f;  
    [SerializeField] private float attackRange = 2f;  

    private AIState currentState;
    private IInputHandler inputHandler;
    private Character target;

    public float ChaseRange => chaseRange;
    public float AttackRange => attackRange;
    public Character Target => target;
    public AIStateType CurrentStateType => currentState?.StateType ?? AIStateType.Idle;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = new AIInput(this);
    }

    private void Start()
    {
        // Стартуем с состояния Idle
        currentState = new IdleState(this);
        currentState.Enter();

        // Ищем игрока как цель
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.GetComponent<Character>();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Data.IsAlive())
        {
            inputHandler.UpdateInput();
            currentState?.Update();
        }
    }


    public void ChangeState(AIState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }


    public void SetTarget(Character newTarget)
    {
        target = newTarget;
    }

    public override void Initialize(Character character)
    {
        if (character != null)
        {
            SetTarget(character);
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
