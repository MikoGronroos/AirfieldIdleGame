using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MergeTree
{

    [field: SerializeField] public MergeTreeBranch[] Tree { get; private set; }

    public List<int> MergeTreeIndexes = new List<int>() { 0 };

}

[Serializable]
public class MergeTreeBranch
{
    public GeneralTurretInfo Turret;
    public MergeTreeBranch[] NextBranches;
}