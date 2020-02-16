using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class OpponentStatsDataSet : EntityStatsDataSet
{
    public const string fileName = "OpponentStats";


    //public float MaxHealth;
    //public float fireCoolDownTime = 0.25f;
    public OpponentType opponentType;
}
