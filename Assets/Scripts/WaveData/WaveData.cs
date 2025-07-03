using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WaveData", menuName = "StageData/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyData> queue1 = new List<EnemyData>();
    public List<EnemyData> queue2 = new List<EnemyData>();
    public List<EnemyData> queue3 = new List<EnemyData>();
    public List<EnemyData> queue4 = new List<EnemyData>();
    public List<EnemyData> queue5 = new List<EnemyData>();

    public List<EnemyData>[] GetWaveData()
    {
        List<EnemyData>[] waveData = new List<EnemyData>[] { queue1, queue2, queue3, queue4, queue5 };
        return waveData;
    }
}
