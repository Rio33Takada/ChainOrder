using UnityEngine;

public abstract class BattleUnit
{
    [Tooltip("���݂�HP")]
    public int currentHP { get; protected set; }

    [Tooltip("�ő�HP")]
    public int MaxHP { get; protected set; }

    public bool IsAlive => currentHP > 0;

    public virtual void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
    }

    public virtual void Heal(int amount)
    {
        currentHP = Mathf.Min(MaxHP, currentHP + amount);
    }
}

