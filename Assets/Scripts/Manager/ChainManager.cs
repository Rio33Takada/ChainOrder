using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChainManager : MonoBehaviour
{
    public BattleManager battleManager;

    //public Dictionary<BattleCharacter, PlayerSkill> GetActivatedChainSkill(PlayerSkill skill)
    //{

    //}

    /// <summary>
    /// 連鎖スキルの発動可否を判定し、発動するスキルを順に返す。
    /// </summary>
    /// <param name="starter">最初にスキルを発動したキャラ</param>
    /// <param name="starterSkill">最初に発動したスキル</param>
    /// <param name="deployedCharacters">出撃キャラクター（Position順に並んでいる想定）</param>
    public List<PlayerSkill> GetChainSkills(BattleCharacter starter, PlayerSkill starterSkill, List<BattleCharacter> deployedCharacters)
    {
        List<PlayerSkill> chainSkills = new List<PlayerSkill>();

        // 初撃スキルを追加
        chainSkills.Add(starterSkill);

        //連鎖開始キャラから連続して並んでいるキャラの数を取得
        float charaCount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (deployedCharacters.Any(p => p.Position == i))
            {
                charaCount = i;
            }
            else break;
        }

        // Position順に連鎖判定（starterの次の位置から順に）
        for (int pos = starter.Position + 1; pos < charaCount; pos++)
        {
            var chara = deployedCharacters.FirstOrDefault(c => c.Position == pos && c.IsAlive);
            if (chara == null) continue;

            // 直前のスキルの攻撃特性
            AttackTrait lastTrait = chainSkills.Last().attackTrait;

            // charaが持つ連鎖スキルのうち、条件に一致するものを探す
            PlayerSkill nextSkill = GetMatchingChainSkill(chara, lastTrait);
            if (nextSkill != null)
            {
                chainSkills.Add(nextSkill);
            }
            else
            {
                // 発動できなければ連鎖終了
                break;
            }
        }

        return chainSkills;
    }

    /// <summary>
    /// キャラが持つ連鎖スキルのうち、指定の攻撃特性を条件に発動可能なものを返す
    /// </summary>
    private PlayerSkill GetMatchingChainSkill(BattleCharacter chara, AttackTrait requiredTrait)
    {
        if (chara.BaseData.chainSkill1 != null && chara.BaseData.chainSkill1.targetAttackTrait == requiredTrait)
            return chara.BaseData.chainSkill1;
        if (chara.BaseData.chainSkill2 != null && chara.BaseData.chainSkill2.targetAttackTrait == requiredTrait)
            return chara.BaseData.chainSkill2;

        return null;
    }

}
