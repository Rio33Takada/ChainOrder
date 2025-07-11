using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    [Header("(�J���p)�X�e�[�W�f�[�^")]
    public StageData testData;

    public BattleUIManager uiManager;
    public BattleInputManager inputManager;

    //�v���C���̃X�e�[�W�f�[�^
    private StageData stageData;

    //�v���C���[���Ґ������`�[��
    public CharacterData[] playerTeam;

    //�`�[���̐퓬���̏��
    public List<BattleCharacter> playerBattleCharacters = new List<BattleCharacter>();

    //�G�̐퓬���̏��
    public List<BattleEnemyCharacter> battleEnemyCharacters = new List<BattleEnemyCharacter>();

    //�o�߃^�[��
    private int turnCount;

    //���݂̃E�F�[�u
    private int waveCount;

    //battleEnemy�̈ʒu���Ƃ̃L���[
    private List<BattleEnemyCharacter> beQueue1 = new List<BattleEnemyCharacter>();
    private List<BattleEnemyCharacter> beQueue2 = new List<BattleEnemyCharacter>();
    private List<BattleEnemyCharacter> beQueue3 = new List<BattleEnemyCharacter>();
    private List<BattleEnemyCharacter> beQueue4 = new List<BattleEnemyCharacter>();
    private List<BattleEnemyCharacter> beQueue5 = new List<BattleEnemyCharacter>();

    void Start()
    {
        StartBattle(testData);
    }

    void Update()
    {
        
    }

    void StartBattle(StageData sData)
    {
        //�o�g���J�n
        stageData = sData;

        //�C���v�b�g�}�l�[�W���[
        inputManager.Initialize(this);


        //�o�g���L�����N�^�[�����E������
        for (int i = 0; i < playerTeam.Length; i++)
        {
            var baseData = playerTeam[i];
            if (baseData == null) continue;

            var battleChar = new BattleCharacter(baseData);
            battleChar.Position = i;
            playerBattleCharacters.Add(battleChar);
        }

        //�G�L�����N�^�[�L���[����
        CreateWaveEnemy(2);

        //�o���X�L���̏�݌��ʓK�p�i�K�v�Ȃ�j
        ApplyPassiveStartEffects();

        //UI������
        uiManager.InputManager = inputManager;
        uiManager.CreateCharacterPanels(playerBattleCharacters);
        uiManager.CreateCharacterStandUI(playerBattleCharacters);
        uiManager.SortStandUI();

        foreach (var bEnemy in battleEnemyCharacters.Where(e => e.isDeployed))
        {
            uiManager.CreateEnemyStandUI(bEnemy);
        }
        uiManager.SortEnemyStand();

        //�^�[���Ǘ�������
        turnCount = 1;
        waveCount = 1;
        //PlayerTurn();
    }

    private void EndBattle(bool isWin)
    {
        if (isWin)
        {
            // �������o�E���U���g��ʂȂ�
        }
        else
        {
            // �s�k���o�E�Q�[���I�[�o�[�����Ȃ�
        }
    }

    void CreateWaveEnemy(int wave)
    {
        List<BattleEnemyCharacter>[] beQueueList = new List<BattleEnemyCharacter>[] { beQueue1, beQueue2, beQueue3, beQueue4, beQueue5 };

        var queueList = stageData.waveDatas[wave - 1].GetWaveData();
        for (int i = 0; i < 5; i++)
        {
            if (queueList[i].Count != 0)
            {
                int count = 0;
                foreach (var be in queueList[i])
                {
                    BattleEnemyCharacter bEC = new BattleEnemyCharacter(be);
                    beQueueList[i].Add(bEC);
                    bEC.position = i;
                    battleEnemyCharacters.Add(bEC);
                    if (count == 0)
                    {
                        bEC.isDeployed = true;
                    }
                    else
                    {
                        bEC.isDeployed = false;
                    }
                    count++;
                }
            }
        }
    }


    /// <summary>
    /// �o���ʒu�̍X�V
    /// </summary>
    /// <param name="deployCharacter"></param>
    public void DeployCharacter(BattleCharacter deployCharacter)
    {
        //�o���ʒu�̍X�V(�L�����N�^�[���퓬�s�\�ŋ󂫂�����ꍇ�̏���������)
        foreach (var chara in playerBattleCharacters)
        {
            if (chara == null) continue;

            chara.Position++;
        }
        deployCharacter.Position = 0;
    }

    /// <summary>
    /// �퓬�s�\���ǂ����̊m�F
    /// </summary>
    public void CheckCharacterDeaths()
    {
        // �v���C���[�L�����̐퓬�s�\����
        foreach (var character in playerBattleCharacters.ToList())
        {
            if (!character.IsAlive)
            {
                HandlePlayerCharacterDeath(character);
            }
        }

        // �G�L�����̎��S����
        foreach (var enemy in battleEnemyCharacters.ToList())
        {
            if (!enemy.IsAlive)
            {
                HandleEnemyCharacterDeath(enemy);
            }
        }

        // �����E�s�k�̔���i�S�Ń`�F�b�N�j
        if (playerBattleCharacters.Count(c => c.IsAlive) <= 4)
        {
            EndBattle(false); // �����l����4�ȉ��Ŕs�k
        }
        else if (battleEnemyCharacters.Where(e => e.isDeployed == true).All(e => !e.IsAlive))
        {
            //�E�F�[�u���̓G��S�ł��������ƁA���̃E�F�[�u�����݂��Ȃ���Ώ���
            if (waveCount == stageData.waveDatas.Count)
            {
                EndBattle(true);  // ����
            }
            else
            {
                waveCount++;
                CreateWaveEnemy(waveCount);
            }
        }
    }


    private void ApplyPassiveStartEffects()
    {
        foreach (var chara in playerBattleCharacters)
        {
            if (chara == null) continue;
            // �����ŏo���X�L���ɉ������������ʂ�K�p
        }
    }

    private void StartPlayerTurn()
    {
        // �o�t�p��������o���X�L���Ȃ�
    }

    private void PlayerTurn()
    {
        //�J�n������
        StartPlayerTurn();

        //����ҋ@

        //���͑ҋ@�܂ōs���A���͌�͕ʊ֐��ɕ�����H

        //�^�[���ڍs
        //EnemyTurn();
    }

    private void EnemyTurn()
    {
        //�J�n����

        //�J�E���g�_�E��
        foreach (var enemy in battleEnemyCharacters)
        {
            enemy.currentCountDown--;
        }

        //�s������G
        List<BattleEnemyCharacter> actingEnemies = battleEnemyCharacters.Where(e => e.currentCountDown <= 0).ToList();

        if (actingEnemies.Count != 0)
        {
            foreach (var enemy in actingEnemies)
            {
                EnemySkill skill = enemy.nextSkill;
                EnemySkillData data = skill.GetSkill(enemy);

                var effectMap =  skill.GetSkillEffect(enemy, playerBattleCharacters, battleEnemyCharacters);

                foreach (var entry in effectMap)
                {
                    ExecuteSkill(entry);
                }
            }
        }

        PlayerTurn();
    }

    public void ExecuteSkill(SkillEffectResult skillEffect)
    {
        switch (skillEffect.EffectType)
        {
            case SkillEffectType.Damage:
                skillEffect.Target.TakeDamage(skillEffect.Value);
                break;
            case SkillEffectType.Heal:
                skillEffect.Target.Heal(skillEffect.Value);
                break;
            case SkillEffectType.Buff:
                break;
        }
    }

    private void HandlePlayerCharacterDeath(BattleCharacter character)
    {
        //playerBattleCharacters.Remove(character);//���Ȃ��ق����ǂ�
    }

    private void HandleEnemyCharacterDeath(BattleEnemyCharacter enemy)
    {
        //���X�g����폜���A���̓G���o��������
        switch (enemy.position)
        {
            case 0:
                beQueue1.Remove(enemy);
                if (beQueue1.Count != 0)beQueue1[0].isDeployed = true;
                uiManager.CreateEnemyStandUI(beQueue1[0]);
                uiManager.SortEnemyStand();
                break;
            case 1:
                beQueue2.Remove(enemy);
                if (beQueue2.Count != 0) beQueue2[0].isDeployed = true;
                uiManager.CreateEnemyStandUI(beQueue2[0]);
                uiManager.SortEnemyStand();
                break;
            case 2:
                beQueue3.Remove(enemy);
                if (beQueue3.Count != 0) beQueue3[0].isDeployed = true;
                uiManager.CreateEnemyStandUI(beQueue3[0]);
                uiManager.SortEnemyStand();
                break;
            case 3:
                beQueue4.Remove(enemy);
                if (beQueue4.Count != 0) beQueue4[0].isDeployed = true;
                uiManager.CreateEnemyStandUI(beQueue4[0]);
                uiManager.SortEnemyStand();
                break;
            case 4:
                beQueue5.Remove(enemy);
                if (beQueue5.Count != 0) beQueue5[0].isDeployed = true;
                uiManager.CreateEnemyStandUI(beQueue5[0]);
                uiManager.SortEnemyStand();
                break;
        }
        battleEnemyCharacters.Remove(enemy);
    }
}
