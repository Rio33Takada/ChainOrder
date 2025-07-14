using UnityEngine;
using System.Collections.Generic;

public class BattleUIManager : MonoBehaviour
{
    public BattleInputManager InputManager;

    public GameObject characterPanelPrefab;
    public GameObject characterStandUIPrefab;
    public GameObject enemyStandUIPrefab;

    private CharacterPanelUI[] charaPanels = new CharacterPanelUI[8];
    private CharacterStandUI[] charaStands = new CharacterStandUI[8];
    private EnemyStandUI[] enemyStands = new EnemyStandUI[5];

    public Transform panelParentTransfrom;
    public Transform[] standUIPositions = new Transform[5]; // 配置用のTransformを予め配置しておく
    public Transform[] enemyStandPositions = new Transform[5];
    [Tooltip("各立ち絵の拡大率")]
    public float[] scales = new float[5];
    public float[] enemyScales = new float[5];

    //キャラUIパネル作成
    public void CreateCharacterPanels(List<BattleCharacter> characters)
    {
        int counter = 0;
        foreach (var character in characters)
        {
            if (character == null) continue;

            GameObject panelGO = Instantiate(characterPanelPrefab);

            //Canvasを親に設定
            panelGO.transform.SetParent(panelParentTransfrom);

            CharacterPanelUI panelUI = panelGO.GetComponent<CharacterPanelUI>();

            //リストに追加
            charaPanels[counter] = panelUI;

            panelUI.SetUp(character, InputManager);

            counter++;
        }
    }

    /// <summary>
    /// キャラ立ち絵作成
    /// </summary>
    /// <param name="characters"></param>
    public void CreateCharacterStandUI(List<BattleCharacter> characters)
    {
        int counter = 0;
        foreach (var character in characters)
        {
            if (character == null) continue;

            GameObject standGO = Instantiate(characterStandUIPrefab);

            CharacterStandUI standUI = standGO.GetComponent<CharacterStandUI>();

            //リストに追加
            charaStands[counter] = standUI;

            //初期化
            standUI.Initialize(character, InputManager);

            //BattleUnitに登録
            character.standIcon = standGO;

            counter++;
        }
    }
    /// <summary>
    /// キャラクターの立ち絵の位置をpositionを参照して変更
    /// </summary>
    public void SortStandUI()
    {
        foreach (var standUI in charaStands)
        {
            Transform transform = standUIPositions[standUI.Character.Position];
            float scale = scales[standUI.Character.Position];
            standUI.MoveTo(transform.position, scale, standUI.Character.Position);
        }
    }

    /// <summary>
    /// 敵立ち絵作成
    /// </summary>
    /// <param name="battleEnemy"></param>
    public void CreateEnemyStandUI(BattleEnemyCharacter battleEnemy)
    {
        GameObject enemyStandGO = Instantiate(enemyStandUIPrefab);

        EnemyStandUI enemyStandUI = enemyStandGO.GetComponent<EnemyStandUI>();

        enemyStandUI.Initialize(battleEnemy, InputManager);

        enemyStands[battleEnemy.position] = enemyStandUI;

        battleEnemy.standIcon = enemyStandGO;
    }

    public void SortEnemyStand()
    {
        foreach (var enemyStandUI in enemyStands)
        {
            Transform transform = enemyStandPositions[enemyStandUI.BattleEnemy.position];

            float scale = 0;

            if (enemyStandUI.BattleEnemy.isDeployed == true)
            {
                scale = enemyScales[enemyStandUI.BattleEnemy.position];
            }

            enemyStandUI.MoveTo(transform.position, scale, enemyStandUI.BattleEnemy.position);
        }
    }

    public void HideTargetIcon()
    {
        foreach(var a in charaStands)
        {
            a.HideTargetIcon();
        }
        foreach(var e in enemyStands)
        {
            e.HideTargetIcon();
        }
    }

    public void UpdateUI()
    {
        foreach(var a in charaStands)
        {
            a.UpdateState();
        }
        foreach(var e in enemyStands)
        {
            e.UpdateState();
        }
    }
}
