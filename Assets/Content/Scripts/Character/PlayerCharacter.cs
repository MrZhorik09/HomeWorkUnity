using UnityEngine;


public class PlayerCharacter : Character
{
    private IInputHandler inputHandler;
    private Character attackTarget;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = new PlayerInput();
    }

    protected override void Update()
    {
        base.Update();

        if (Data.IsAlive())
        {
            inputHandler.UpdateInput();
            HandleMovement();
            HandleAttack();
        }
    }

    private void HandleMovement()
    {
        Vector2 movement = inputHandler.GetMovementInput();
        if (movement.magnitude > 0)
        {
            Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
            transform.position += moveDirection * Data.MoveSpeed * Time.deltaTime;
        }
    }

    private void HandleAttack()
    {
        if (inputHandler.GetAttackInput() && attackTarget != null)
        {
            Attack(attackTarget);
        }
    }

    public void SetAttackTarget(Character target)
    {
        attackTarget = target;
    }

    public override void Initialize(Character character)
    {

        if (character != null)
        {
            SetAttackTarget(character);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null && character != this)
        {
            attackTarget = character;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character == attackTarget)
        {
            attackTarget = null;
        }
    }
}

