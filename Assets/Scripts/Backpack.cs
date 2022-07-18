using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Backpack : MonoBehaviour
{
    [SerializeField] private Transform _backpack;
    [SerializeField] private int _maxBackpackSlots;
    [SerializeField] private List<GameObject> _blocksInBackpack;
    [SerializeField] private UIController _uIController;

    public int BlocksInBackpack => _blocksInBackpack.Count;

    public void AddBlockInBackpack(GameObject block)
    {
        if (_blocksInBackpack.Count < _maxBackpackSlots)
        {
            block.GetComponent<BoxCollider>().enabled = false;
            if (_blocksInBackpack.Count == 0)
            {
                _blocksInBackpack.Add(block);
                block.transform.DORotate(new Vector3(0, 0, 0), 0f);
                block.transform.DOMove(_backpack.position, 0f).OnComplete(() =>
                {
                    block.GetComponent<PlantBlock>().UpdateCubePosition(_backpack, true);
                });



            }
            else
            {
                Transform lastBlockPosition = _blocksInBackpack[_blocksInBackpack.Count - 1].transform;
                _blocksInBackpack.Add(block);
                block.transform.DORotate(new Vector3(0, 0, 0), 0f);
                block.transform.DOMove(new Vector3(lastBlockPosition.position.x, lastBlockPosition.position.y + 0.15f, lastBlockPosition.position.z), 0f).OnComplete(() =>
                {
                    block.GetComponent<PlantBlock>().UpdateCubePosition(lastBlockPosition, true);
                });
            }
            _uIController.UpdateBackpackStatus(_maxBackpackSlots, _blocksInBackpack.Count);
        }
    }

    public IEnumerator ClearBackpack(Transform storehouse, float coinsForStack)
    {

        for (int i = _blocksInBackpack.Count - 1; i >= 0; i--)
        {
            _blocksInBackpack[i].transform.DOMove(storehouse.position, 0.02f);
            yield return new WaitForSeconds(0.02f);
            _uIController.CoinAnimation();
            Destroy(_blocksInBackpack[i]);
        }
        _uIController.ShakeConis();
        _uIController.AddToCoins(coinsForStack);
        _blocksInBackpack.Clear();
        _uIController.UpdateBackpackStatus(_maxBackpackSlots, _blocksInBackpack.Count);
    }

}
