using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementInfo
{
    public OpponentType opponentType;
    public SpawnLocation startLocation;
    public SpawnLocation endLocation;
    
}

[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class MovementDataSet : DataSet
{
    public const string fileName = "MovementDataSet";

    public List<MovementInfo> movementInfos;

    [ContextMenu("Init")]
    public void IntilizeInfos()
    {
        
        int length = System.Enum.GetNames(typeof(OpponentType)).Length;
        movementInfos = new List<MovementInfo>(length);
        for (int i=0;i<length;i++)
        {
            movementInfos.Add(new MovementInfo { opponentType = (OpponentType)i });
        }
    }

}
