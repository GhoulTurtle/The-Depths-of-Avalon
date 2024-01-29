using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Droppable Item", menuName = "Droppable Items/New Droppable Item")]
public class DroppableItemSO : ScriptableObject {
    public GameObject ItemPrefab;
    [Range(0f,1f)] public float dropChance;
}
