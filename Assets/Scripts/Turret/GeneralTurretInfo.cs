using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret Info")]
public class GeneralTurretInfo : ScriptableObject
{

    [field: SerializeField] public string turretName { get; private set; }
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [ContextMenu("Generate new id")]
    public void GenerateId() => Id = Guid.NewGuid().ToString();

}
