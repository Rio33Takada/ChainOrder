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
    /// �A���X�L���̔����ۂ𔻒肵�A��������X�L�������ɕԂ��B
    /// </summary>
    /// <param name="starter">�ŏ��ɃX�L���𔭓������L����</param>
    /// <param name="starterSkill">�ŏ��ɔ��������X�L��</param>
    /// <param name="deployedCharacters">�o���L�����N�^�[�iPosition���ɕ���ł���z��j</param>
    public List<PlayerSkill> GetChainSkills(BattleCharacter starter, PlayerSkill starterSkill, List<BattleCharacter> deployedCharacters)
    {
        List<PlayerSkill> chainSkills = new List<PlayerSkill>();

        // �����X�L����ǉ�
        chainSkills.Add(starterSkill);

        //�A���J�n�L��������A�����ĕ���ł���L�����̐����擾
        float charaCount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (deployedCharacters.Any(p => p.Position == i))
            {
                charaCount = i;
            }
            else break;
        }

        // Position���ɘA������istarter�̎��̈ʒu���珇�Ɂj
        for (int pos = starter.Position + 1; pos < charaCount; pos++)
        {
            var chara = deployedCharacters.FirstOrDefault(c => c.Position == pos && c.IsAlive);
            if (chara == null) continue;

            // ���O�̃X�L���̍U������
            AttackTrait lastTrait = chainSkills.Last().attackTrait;

            // chara�����A���X�L���̂����A�����Ɉ�v������̂�T��
            PlayerSkill nextSkill = GetMatchingChainSkill(chara, lastTrait);
            if (nextSkill != null)
            {
                chainSkills.Add(nextSkill);
            }
            else
            {
                // �����ł��Ȃ���ΘA���I��
                break;
            }
        }

        return chainSkills;
    }

    /// <summary>
    /// �L���������A���X�L���̂����A�w��̍U�������������ɔ����\�Ȃ��̂�Ԃ�
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
