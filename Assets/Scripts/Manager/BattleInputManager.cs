using UnityEngine;
using System.Collections.Generic;

public class BattleInputManager : MonoBehaviour
{
    public TargetingRule targetingRule;

    public enum InputState
    {
        //非アクティブ
        Idle,
        //スキル入力待機
        WaitingSkillInput,
        //ターゲット入力待機
        SkillTargetSelect,
        //連鎖スキルターゲット入力待機
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
        // スキル選択の上書き
        skillUser = user;
        selectedSkill = skill;
        currentState = InputState.SkillTargetSelect;

        HighlightTargets(skill); // スキルの対象に応じてハイライト処理
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
        // 対象タイプに応じてハイライト対象を決定
        var selectableTargets = targetingRule.GetSelectableTarget(skill.TargetType, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
        foreach (var t in selectableTargets)
        {
            //highlight
        }
    }

    public void CancelSkillSelection()
    {
        ResetInput();
        // ハイライト解除など
    }

    private void ResetInput()
    {
        currentState = InputState.Idle;
        selectedSkill = null;
        skillUser = null;
        // ハイライト解除
    }
}