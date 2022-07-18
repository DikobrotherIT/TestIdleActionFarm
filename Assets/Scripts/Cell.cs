using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool _canGrowth;
    [SerializeField] private GameObject _plantPrefab;
    [SerializeField] private GameObject _blocksPrefab;
    private float _elapsedTime = 0;


    private void Update()
    {
        if (_canGrowth == true)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= GetComponentInParent<MudSpawner>().PlantTimer)
            {
                _elapsedTime = 0;
                PlantGrowth();
            }
        }
    }
    public void PlantGrowth()
    {
        GameObject newPlant = Instantiate(_plantPrefab, this.gameObject.transform.position, Quaternion.identity);
        newPlant.transform.parent = this.transform;
        _canGrowth = false;
    }

    public void ResetCell()
    {
        GameObject newBlock = Instantiate(_blocksPrefab, this.gameObject.transform.position, Quaternion.identity);
        newBlock.transform.DOJump(newBlock.transform.position, 2, 1, 0.5f).OnComplete(() =>
        {
            newBlock.GetComponent<BoxCollider>().enabled = true;
        }); ;
        _canGrowth = true;
    }

}
