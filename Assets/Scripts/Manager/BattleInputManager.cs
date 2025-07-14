using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    public InputState currentState = InputState.Idle;

    private BattleManager battleManager;
    private BattleUIManager uIManager;
    private PlayerSkill selectedSkill;
    private BattleCharacter skillUser;
    private List<BattleUnit> selectableUnits;

    public void Initialize(BattleManager manager)
    {
        battleManager = manager;
        uIManager = manager.uiManager;
    }

    public void OnSkillButtonPressed(BattleCharacter user, PlayerSkill skill)
    {
        if (currentState == InputState.WaitingSkillInput || currentState == InputState.SkillTargetSelect)
        {
            // スキル選択の上書き
            skillUser = user;
            selectedSkill = skill;
            currentState = InputState.SkillTargetSelect;

            // スキルの対象に応じてハイライト処理
            selectableUnits = HighlightTargets(skill);
        }

    }

    public void OnChainSkillTargetSelect(BattleCharacter user, PlayerSkill chainSkill)
    {
        currentState = InputState.ChainSkillTargetSelect;
        selectedSkill = chainSkill;
        skillUser = user;

        selectableUnits = HighlightTargets(chainSkill);
    }

    public void OnTargetSelected(BattleUnit selected)
    {
        if (!selectableUnits.Contains(selected)) return;

        if (currentState == InputState.SkillTargetSelect || currentState == InputState.ChainSkillTargetSelect)
        {
            currentState = InputState.Idle;
            //Debug.Log(selected.standIcon.GetComponent<CharacterStandUI>().Character.BaseData.characterName);
            var effectMap = selectedSkill.GetSkillEffect(skillUser, selected, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
            foreach (var effect in effectMap)
            {
                battleManager.ExecuteSkill(effect);
            }

            uIManager.HideTargetIcon();
            ResetInput();
        }
    }

    private List<BattleUnit> HighlightTargets(PlayerSkill skill)
    {
        //ハイライト対象リセット
        uIManager.HideTargetIcon();

        // 対象タイプに応じてハイライト対象を決定
        var selectableTargets = targetingRule.GetSelectableTarget(skill.TargetType, battleManager.playerBattleCharacters, battleManager.battleEnemyCharacters);
        foreach (var t in selectableTargets)
        {
            t.standIcon.GetComponent<StandUI>().ShowTargetIcon();
        }

        return selectableTargets;
    }

    //public void CancelSkillSelection()
    //{
    //    ResetInput();
    //    // ハイライト解除など
    //}

    private void ResetInput()
    {
        currentState = InputState.Idle;
        selectedSkill = null;
        skillUser = null;
        // ハイライト解除
        uIManager.HideTargetIcon();
    }
}