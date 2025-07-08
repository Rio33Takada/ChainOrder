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
    public Transform[] standUIPositions = new Transform[5]; // �z�u�p��Transform��\�ߔz�u���Ă���
    public Transform[] enemyStandPositions = new Transform[5];
    [Tooltip("�e�����G�̊g�嗦")]
    public float[] scales = new float[5];
    public float[] enemyScales = new float[5];

    //�L����UI�p�l���쐬
    public void CreateCharacterPanels(List<BattleCharacter> characters)
    {
        int counter = 0;
        foreach (var character in characters)
        {
            if (character == null) continue;

            GameObject panelGO = Instantiate(characterPanelPrefab);

            //Canvas��e�ɐݒ�
            panelGO.transform.SetParent(panelParentTransfrom);

            CharacterPanelUI panelUI = panelGO.GetComponent<CharacterPanelUI>();

            //���X�g�ɒǉ�
            charaPanels[counter] = panelUI;

            panelUI.SetUp(character, InputManager);

            //// CharacterData ����A�C�R�����擾����UI�ɐݒ�
            //Sprite iconSprite = character.BaseData.characterIcon;
            //panelUI.SetIcon(iconSprite);

            //// �K�v�ɉ����āA���O�EHP�Ȃǂ��ݒ�
            //panelUI.SetName(character.BaseData.characterName);
            //panelUI.SetHP(character.currentHP, character.MaxHP);

            counter++;
        }
    }

    /// <summary>
    /// �L���������G�쐬
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

            //���X�g�ɒǉ�
            charaStands[counter] = standUI;

            //������
            standUI.Initialize(character, InputManager);

            counter++;
        }
    }
    /// <summary>
    /// �L�����N�^�[�̗����G�̈ʒu��position���Q�Ƃ��ĕύX
    /// </summary>
    public void SortStandUI()
    {
        foreach (var standUI in charaStands)
        {
            Transform transform = standUIPositions[standUI.Character.Position];
            float scale = scales[standUI.Character.Position]
                / standUI.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            standUI.MoveTo(transform.position, scale, standUI.Character.Position);
        }
    }

    /// <summary>
    /// �G�����G�쐬
    /// </summary>
    /// <param name="battleEnemy"></param>
    public void CreateEnemyStandUI(BattleEnemyCharacter battleEnemy)
    {
        GameObject enemyStandGO = Instantiate(enemyStandUIPrefab);

        EnemyStandUI enemyStandUI = enemyStandGO.GetComponent<EnemyStandUI>();

        enemyStandUI.Initialize(battleEnemy, InputManager);

        enemyStands[battleEnemy.position] = enemyStandUI;
    }

    public void SortEnemyStand()
    {
        foreach (var enemyStandUI in enemyStands)
        {
            Transform transform = enemyStandPositions[enemyStandUI.BattleEnemy.position];

            float scale = 0;

            if (enemyStandUI.BattleEnemy.isDeployed == true)
            {
                scale = enemyScales[enemyStandUI.BattleEnemy.position]
                    / enemyStandUI.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            }

            enemyStandUI.MoveTo(transform.position, scale, enemyStandUI.BattleEnemy.position);
        }
    }
}
