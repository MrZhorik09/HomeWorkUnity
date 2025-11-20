using UnityEngine;

/// <summary>
/// Реализация ввода для НПС (управляется ИИ)
/// </summary>
public class AIInput : IInputHandler
{
    private Character owner;
    private Vector2 movementInput;
    private bool attackInput;

    public AIInput(Character owner)
    {
        this.owner = owner;
    }

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
        // ИИ не использует прямой ввод, управление через машину состояний
        movementInput = Vector2.zero;
        attackInput = false;
    }
}
