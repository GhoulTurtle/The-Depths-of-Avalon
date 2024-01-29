using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class DropItemSystem : MonoBehaviour {
    [SerializeField] private List<DroppableItemSO> droppableItems = new();
    

    private HealthSystem healthSystem;

    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void OnEnable() {
        healthSystem.OnDie += GetItemToDrop;
    }

    private void OnDisable() {
        healthSystem.OnDie -= GetItemToDrop;
    }

    private void GetItemToDrop(object sender, EventArgs e) {
        int itemCount = droppableItems.Count;

        if(itemCount > 0) {
            int randomIndex = UnityEngine.Random.Range(0, itemCount);
            DroppableItemSO itemToDrop = droppableItems[randomIndex];
            ChanceToSpawn(itemToDrop);
        } else {
            Debug.LogError("<color=red>No Items are in the Droppable Items List!!</color>");
        }
    }

    private void ChanceToSpawn(DroppableItemSO selectedItem) {
        if(UnityEngine.Random.value <= selectedItem.dropChance) {
            DropSelectedItem(selectedItem.ItemPrefab);
        } else {
            Debug.Log("Item not dropped based on spawn chance.");
            Destroy(this.gameObject);
        }
    }

    private void DropSelectedItem(GameObject selectedItem) {
        Instantiate(selectedItem, this.transform.position, quaternion.identity);
        Debug.Log("Dropping " + selectedItem);
        Destroy(this.gameObject);
    }
}
