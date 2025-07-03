using UnityEngine;

public enum CharacterRole
{
    Attacker,
    Healer,
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterID;//String‚È‚Ì‚©int‚È‚Ì‚©
    public string characterName;

    public int baseHP;
    public int baseATK;
    public int baseDEF;

    public PlayerSkill attackSkill;
    public PlayerSkill specialSkill;
    public PlayerSkill chainSkill1;
    public PlayerSkill chainSkill2;
    public PlayerSkill sortieSkill;
    public PlayerSkill passiveSkill;

    public Sprite characterIcon;
    public Sprite sdCharacter;

    public CharacterRole role; // enum: Attacker, Healer, etc.
    public bool usesDayNightSystem;
}
