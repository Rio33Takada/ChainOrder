using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleInputManager : MonoBehaviour
{
    public TargetingRule targetingRule;

    public enum InputState
    {
        //��A�N�e�B�u
        Idle,
        //�X�L�����͑ҋ@
        WaitingSkillInput,
        //�^�[�Q�b�g���͑ҋ@
        SkillTargetSelect,
        //�A���X�L���^�[�Q�b�g���͑ҋ@
        ChainSkillTargetSelect,
    }

    private InputState currentState = InputState.Idle;

    private BattleManager battleManager;
    private BattleUIManager uIManager;
    private PlayerSkill selectedSkill;
    private BattleCharacter skillUser;

    public void Initialize(BattleManager manager)
    {
        battleManager = manager;
        uIManager = manager.uiManager;
    }

    public void OnSkillButtonPressed(BattleCharacter user, PlayerSkill skill)
    {
        if (currentState == InputState.WaitingSkillInput || currentState == InputState.SkillTargetSelect)
        {

        }
        // �X�L���I���̏㏑��
        skillUser = user;
        selectedSkill = skill;
        currentState = InputState.SkillTargetSelect;

        HighlightTargets(skill); // �X�L���̑Ώۂɉ����ăn�C���C�g����
    }

    public void OnChainSkillTargetSelect(BattleCharacter user, PlayerSkill chainSkill)
    {
        currentState = InputState.ChainSkillTargetSelect;
        selectedSkill = chainSkill;
        skillUser = user;

        HighlightTargets(chainSkill);
    }

    public void OnTargetSelected(BattleUnit selected)
    {
        if (currentState == InputState.SkillTargetSelect || currentState == InputState.ChainSkillTargetSelect)
        {
            Debug.Log(selected.standIcon.GetComponent<CharacterStandUI>().Character.BaseData.characterName);
            var effectMap = selectedSkill.GetSkillEffect(skillUser, selected, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
            foreach (var effect in effectMap)
            {
                battleManager.ExecuteSkill(effect);
            }
            

            ResetInput();
        }
    }

    private void HighlightTargets(PlayerSkill skill)
    {
        //�n�C���C�g�Ώۃ��Z�b�g
        uIManager.HideTargetIcon();

        // �Ώۃ^�C�v�ɉ����ăn�C���C�g�Ώۂ�����
        var selectableTargets = targetingRule.GetSelectableTarget(skill.TargetType, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
        foreach (var t in selectableTargets.Where(a => a.isAlly).ToList())
        {
            t.standIcon.GetComponent<CharacterStandUI>().ShowTargetIcon();
        }
        foreach (var t in selectableTargets.Where(e => !e.isAlly).ToList())
        {
            t.standIcon.GetComponent<EnemyStandUI>().ShowTargetIcon();
        }
    }

    //public void CancelSkillSelection()
    //{
    //    ResetInput();
    //    // �n�C���C�g�����Ȃ�
    //}

    private void ResetInput()
    {
        currentState = InputState.Idle;
        selectedSkill = null;
        skillUser = null;
        // �n�C���C�g����
    }
}