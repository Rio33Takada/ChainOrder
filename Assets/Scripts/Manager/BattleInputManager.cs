using UnityEngine;
using System.Collections.Generic;

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
    private PlayerSkill selectedSkill;
    private BattleCharacter skillUser;

    public void Initialize(BattleManager manager)
    {
        battleManager = manager;
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