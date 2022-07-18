using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHouse : MonoBehaviour
{
    [SerializeField] private int _blockPrice;
    [SerializeField] private Transform _storeDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            float totalMoney = _blockPrice * player.gameObject.GetComponent<Backpack>().BlocksInBackpack;
            StartCoroutine(player.gameObject.GetComponent<Backpack>().ClearBackpack(_storeDoor, totalMoney));
        }
    }
}
