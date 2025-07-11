using UnityEngine;

public abstract class BattleUnit
{
    public virtual bool isAlly => false;
    [Tooltip("Œ»Ý‚ÌHP")]
    public int currentHP { get; protected set; }

    [Tooltip("Å‘åHP")]
    public int MaxHP { get; protected set; }

    public bool IsAlive => currentHP > 0;

    public GameObject standIcon;

    public virtual void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
    }

    public virtual void Heal(int amount)
    {
        currentHP = Mathf.Min(MaxHP, currentHP + amount);
    }
}

