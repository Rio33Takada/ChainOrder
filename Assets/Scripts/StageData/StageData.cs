using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageData", menuName = "StageData/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;

    public List<WaveData> waveDatas = new List<WaveData>();
}
