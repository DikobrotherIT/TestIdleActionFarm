using UnityEngine;

public class Plant : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {

            StartCoroutine(player.CutPlants(this.gameObject, GetComponentInParent<Cell>()));
        }
    }
}
