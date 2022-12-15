using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class MergeTree
{

    [field: SerializeField] public MergeTreeBranch[] Tree { get; private set; }

    public GameObject GetMerge(List<int> mergeTreeIndexes)
    {
        GameObject mergeObject = null;

        MergeTreeBranch currentBranch = Tree[0];

        for (int i = 1; i < mergeTreeIndexes.Count; i++)
        {
            currentBranch = currentBranch.NextBranches[mergeTreeIndexes[i]];
            if (i >= mergeTreeIndexes.Count - 1)
            {
                mergeObject = currentBranch.Turret.Prefab;
            }
        }
        return mergeObject;
    }
}

[Serializable]
public class MergeTreeBranch
{
    public GeneralTurretInfo Turret;
    public MergeTreeBranch[] NextBranches;
}