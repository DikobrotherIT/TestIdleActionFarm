using System.Collections;
using UnityEngine;
using EzySlice;

public class Scythe : MonoBehaviour
{
    [SerializeField] private Material _sliceMaterial;
    [SerializeField] private BoxCollider _scytheCollider;
    private GameObject _plant;
    private bool _isSliced;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plant") && _isSliced == false)
        {
            _scytheCollider.enabled = false;
            _plant = other.gameObject;
            StartCoroutine(SlicePlant());
        }
    }

    public IEnumerator SlicePlant()
    {
        yield return new WaitForSeconds(0.8f);
        _scytheCollider.enabled = true;
        SlicedHull Sliced = Slice(_plant, _sliceMaterial);
        if (Sliced != null)
        {
            GameObject upperSliced = Sliced.CreateUpperHull(_plant, _sliceMaterial);
            upperSliced.AddComponent<Rigidbody>();
            upperSliced.AddComponent<MeshCollider>().convex = true;
            GameObject lowerSliced = Sliced.CreateLowerHull(_plant, _sliceMaterial);
            lowerSliced.AddComponent<Rigidbody>();
            lowerSliced.AddComponent<MeshCollider>().convex = true;
            upperSliced.transform.parent = _plant.transform;
            lowerSliced.transform.parent = _plant.transform;
            _plant.GetComponent<MeshRenderer>().enabled = false;
            _isSliced = true;
        }

    }

    public void ResetScythe()
    {
        _isSliced = false;
        _scytheCollider.enabled = true;
    }

    public SlicedHull Slice(GameObject obj, Material crossSectionMaterial)
    {
        return obj.Slice(transform.position, transform.right, crossSectionMaterial);
    }
}
