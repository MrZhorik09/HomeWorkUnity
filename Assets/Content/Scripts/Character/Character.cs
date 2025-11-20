using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected string characterName = "Character";
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected int attackPower = 10;

    private CharacterData characterData;

    public CharacterData Data { get; private set; }

    protected virtual void Awake()
    {
        InitializeCharacterData();
    }

    
    public virtual void Initialize(Character character)
    {
 
    }

    private void InitializeCharacterData()
    {
        characterData = new CharacterData(characterName, maxHealth, moveSpeed, attackPower);
        Data = characterData;
    }

    public virtual void Attack(Character target)
    {
        if (target != null && target.Data.IsAlive())
        {
            target.Data.TakeDamage(Data.AttackPower);
            Debug.Log($"{Data.CharacterName} атаковал {target.Data.CharacterName} на {Data.AttackPower} урона!");
        }
    }

    public void HealSelf(int amount)
    {
        Data.Heal(amount);
        Debug.Log($"{Data.CharacterName} восстановил {amount} здоровья. Текущее здоровье: {Data.Health}/{Data.MaxHealth}");
    }

    protected virtual void Update()
    {
        if (!Data.IsAlive())
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Debug.Log($"{Data.CharacterName} погиб!");
        gameObject.SetActive(false);
    }
}
