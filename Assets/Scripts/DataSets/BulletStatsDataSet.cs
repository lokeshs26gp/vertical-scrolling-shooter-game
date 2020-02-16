
using UnityEngine;
[CreateAssetMenu(fileName = fileName, menuName = prefixPath + fileName)]
public class BulletStatsDataSet : EntityStatsDataSet
{
    public const string fileName = "BulletStats";

    public BulletType bulletType;
}
