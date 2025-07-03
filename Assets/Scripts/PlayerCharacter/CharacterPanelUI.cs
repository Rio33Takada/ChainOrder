using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanelUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    //[SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider hpSlider;
    //[SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button specialButton;

    public BattleCharacter Character { get; private set; }
    public BattleInputManager InputManager { get; private set; }

    public void SetUp(BattleCharacter battleCharacter, BattleInputManager inputManager)
    {
        Character = battleCharacter;
        InputManager = inputManager;
        SetIcon();
        SetName();
        SetHP();
        SetButton();
        SwitchStateColor();
    }

    public void SetIcon()
    {
        if (iconImage != null)
            iconImage.sprite = Character.BaseData.characterIcon;
    }

    public void SetName()
    {
        //if (nameText != null)
        //    nameText.text = Character.BaseData.characterName;
    }

    public void SetHP()
    {
        if (hpSlider != null)
            hpSlider.value = (float)Character.currentHP / Character.MaxHP;
    }

    public void SetButton()
    {
        if (attackButton != null)
            attackButton.onClick.AddListener(() => { 
                InputManager.OnSkillButtonPressed(Character, Character.BaseData.attackSkill); });
        if (specialButton != null)
            specialButton.onClick.AddListener(() => { 
                InputManager.OnSkillButtonPressed(Character, Character.BaseData.specialSkill); });
    }

    public void SwitchStateColor()
    {
        // 状態に応じた見た目の更新（例：倒れたら灰色など）
        //spriteRenderer.color = Character.IsAlive ? Color.white : Color.gray;//透明度に変更
        iconImage.color = Character.IsAlive ? new Color32(225, 225, 225, 225) : new Color32(225, 225, 225, 204);
    }
}
