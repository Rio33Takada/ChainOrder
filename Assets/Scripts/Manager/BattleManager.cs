using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    [Header("(開発用)ステージデータ")]
    public StageData testData;

    public BattleUIManager uiManager;
    public BattleInputManager inputManager;

    //プレイ中のステージデータ
    private StageData stageData;

    //プレイヤーが編成したチーム
    public CharacterData[] playerTeam;

    //チームの戦闘中の情報
    public List<BattleCharacter> playerBattleCharacters = new List<BattleCharacter>();

    //敵の戦闘中の情報
    public List<BattleEnemyCharacter> battleEnemyCharacters = new List<BattleEnemyCharacter>();

    //経過ターン
    private int turnCount;

    //現在のウェーブ
    private int waveCount;

    //battleEnemyの位置ごとのキュー
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
        //バトル開始
        stageData = sData;

        //インプットマネージャー
        inputManager.Initialize(this);


        //バトルキャラクター生成・初期化
        for (int i = 0; i < playerTeam.Length; i++)
        {
            var baseData = playerTeam[i];
            if (baseData == null) continue;

            var battleChar = new BattleCharacter(baseData);
            battleChar.Position = i;
            playerBattleCharacters.Add(battleChar);
        }

        //敵キャラクターキュー生成
        CreateWaveEnemy(2);

        //出撃スキルの常在効果適用（必要なら）
        ApplyPassiveStartEffects();

        //UI初期化
        uiManager.InputManager = inputManager;
        uiManager.CreateCharacterPanels(playerBattleCharacters);
        uiManager.CreateCharacterStandUI(playerBattleCharacters);
        uiManager.SortStandUI();

        foreach (var bEnemy in battleEnemyCharacters.Where(e => e.isDeployed))
        {
            uiManager.CreateEnemyStandUI(bEnemy);
        }
        uiManager.SortEnemyStand();

        //ターン管理初期化
        turnCount = 1;
        waveCount = 1;
        //PlayerTurn();
    }

    private void EndBattle(bool isWin)
    {
        if (isWin)
        {
            // 勝利演出・リザルト画面など
        }
        else
        {
            // 敗北演出・ゲームオーバー処理など
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
    /// 出撃位置の更新
    /// </summary>
    /// <param name="deployCharacter"></param>
    public void DeployCharacter(BattleCharacter deployCharacter)
    {
        //出撃位置の更新(キャラクターが戦闘不能で空きがある場合の処理未実装)
        foreach (var chara in playerBattleCharacters)
        {
            if (chara == null) continue;

            chara.Position++;
        }
        deployCharacter.Position = 0;
    }

    /// <summary>
    /// 戦闘不能かどうかの確認
    /// </summary>
    public void CheckCharacterDeaths()
    {
        // プレイヤーキャラの戦闘不能処理
        foreach (var character in playerBattleCharacters.ToList())
        {
            if (!character.IsAlive)
            {
                HandlePlayerCharacterDeath(character);
            }
        }

        // 敵キャラの死亡処理
        foreach (var enemy in battleEnemyCharacters.ToList())
        {
            if (!enemy.IsAlive)
            {
                HandleEnemyCharacterDeath(enemy);
            }
        }

        // 勝利・敗北の判定（全滅チェック）
        if (playerBattleCharacters.Count(c => c.IsAlive) <= 4)
        {
            EndBattle(false); // 生存人数が4以下で敗北
        }
        else if (battleEnemyCharacters.Where(e => e.isDeployed == true).All(e => !e.IsAlive))
        {
            //ウェーブ内の敵を全滅させたあと、次のウェーブが存在しなければ勝ち
            if (waveCount == stageData.waveDatas.Count)
            {
                EndBattle(true);  // 勝利
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
            // ここで出撃スキルに応じた初期効果を適用
        }
    }

    private void StartPlayerTurn()
    {
        // バフ継続処理や出撃スキルなど
    }

    private void PlayerTurn()
    {
        //開始時処理
        StartPlayerTurn();

        //操作待機

        //入力待機まで行い、入力後は別関数に分ける？

        //ターン移行
        //EnemyTurn();
    }

    private void EnemyTurn()
    {
        //開始処理

        //カウントダウン
        foreach (var enemy in battleEnemyCharacters)
        {
            enemy.currentCountDown--;
        }

        //行動する敵
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
        //playerBattleCharacters.Remove(character);//しないほうが良い
    }

    private void HandleEnemyCharacterDeath(BattleEnemyCharacter enemy)
    {
        //リストから削除し、次の敵を出撃させる
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
