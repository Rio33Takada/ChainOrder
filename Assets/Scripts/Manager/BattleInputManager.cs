using UnityEngine;
using System.Collections.Generic;

public class BattleInputManager : MonoBehaviour
{
    public TargetingRule targetingRule;

    public enum InputState
    {
        Idle,
        SkillSelected,
        ChainSkillTargetSelect
    }

    private InputState currentState = InputState.Idle;

    private BattleManager battleManager;
    private PlayerSkill selectedSkill;
    private BattleCharacter skillUser;

    public void Initialize(BattleManager manager)
    {
        battleManager = manager;
    }

    public void OnSkillButtonPressed(BattleCharacter user, PlayerSkill skill)
    {
        // �X�L���I���̏㏑��
        skillUser = user;
        selectedSkill = skill;
        currentState = InputState.SkillSelected;

        HighlightTargets(skill); // �X�L���̑Ώۂɉ����ăn�C���C�g����
    }

    public void OnChainSkillTargetSelect(BattleCharacter user, PlayerSkill chainSkill)
    {
        currentState = InputState.ChainSkillTargetSelect;
        selectedSkill = chainSkill;
        skillUser = user;

        HighlightTargets(chainSkill);
    }

    public void OnTargetSelected()
    {
        if (currentState == InputState.SkillSelected || currentState == InputState.ChainSkillTargetSelect)
        {
            //var effects = selectedSkill.GetSkillEffect(skillUser, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
            //foreach (var effect in effects)
            //{
            //    battleManager.ExecuteSkill(effect);
            //}

            ResetInput();
        }
    }

    private void HighlightTargets(PlayerSkill skill)
    {
        // �Ώۃ^�C�v�ɉ����ăn�C���C�g�Ώۂ�����
        var selectableTargets = targetingRule.GetSelectableTarget(skill.TargetType, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
        foreach (var t in selectableTargets)
        {
            //highlight
        }
    }

    public void CancelSkillSelection()
    {
        ResetInput();
        // �n�C���C�g�����Ȃ�
    }

    private void ResetInput()
    {
        currentState = InputState.Idle;
        selectedSkill = null;
        skillUser = null;
        // �n�C���C�g����
    }
}