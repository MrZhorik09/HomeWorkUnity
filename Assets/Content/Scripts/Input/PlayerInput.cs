using UnityEngine;
public class PlayerInput : IInputHandler
{
    private Vector2 movementInput;
    private bool attackInput;

    public Vector2 GetMovementInput()
    {
        return movementInput;
    }

    public bool GetAttackInput()
    {
        return attackInput;
    }

    public void UpdateInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementInput = new Vector2(horizontal, vertical);
        attackInput = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }
}
