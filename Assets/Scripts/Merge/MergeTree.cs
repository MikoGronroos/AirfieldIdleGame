using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class MergeTree
{

    [field: SerializeField] public MergeTreeBranch[] Tree { get; private set; }

    public List<int> MergeTreeIndexes = new List<int>() { 0 };

    public GameObject GetMerge()
    {
        GameObject mergeObject = null;

        MergeTreeBranch currentBranch = Tree[0];

        for (int i = 1; i < MergeTreeIndexes.Count; i++)
        {
            currentBranch = currentBranch.NextBranches[MergeTreeIndexes[i]];
            if (i >= MergeTreeIndexes.Count - 1)
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