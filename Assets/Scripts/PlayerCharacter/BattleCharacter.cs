using UnityEngine;
using System.Collections.Generic;

// バトル中に使用される状態データ
public class BattleCharacter : BattleUnit
{
    public override bool isAlly => true;
    public CharacterData BaseData { get; private set; }// 元データへの参照

    //出撃中かどうか
    public bool isDeployed => position < 4;

    //public List<Buff> buffs = new();
    public Dictionary<string, int> stacks = new(); // 特殊システム等（例：鏡の枚数）

    // 例: 出撃位置（0:先頭 〜 3:最後尾）
    [Tooltip("出撃位置（0:先頭 〜 3:最後尾, 4:控え）")]
    private int position;

    public int Position
    {
        get => position;
        set => position = Mathf.Clamp(value, 0, 4);
    }

    public BattleCharacter(CharacterData baseData)
    {
        BaseData = baseData;
    }

    public void SetMaxHP()
    {
        MaxHP = BaseData.baseHP;
        currentHP = MaxHP;
    }
}