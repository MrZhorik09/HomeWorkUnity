using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [SerializeField] private string characterName;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int attackPower;

    public string CharacterName => characterName;
    public int Health => health;
    public int MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public int AttackPower => attackPower;

    public CharacterData(string name, int maxHp, float speed, int attack)
    {
        characterName = name;
        maxHealth = maxHp;
        health = maxHp;
        moveSpeed = speed;
        attackPower = attack;
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(maxHealth, health + amount);
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
