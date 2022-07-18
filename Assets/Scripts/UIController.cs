using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _startCoinPosition;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private float _coinsTextAnimationTime;
    [SerializeField] private Image _coinsPanel;
    [SerializeField] private TMP_Text _backpackText;
    private float initialCoins;
    private float currentCoins = 0f;



    public void CoinAnimation()
    {
        GameObject coin = Instantiate(_coinPrefab, _startCoinPosition);
        coin.transform.SetParent(this.gameObject.transform);
        coin.transform.DOMove(_coinsText.transform.position, 1).OnComplete(() =>
        {
            Destroy(coin);
        });
    }

    public void AddToCoins(float value)
    {
        initialCoins = currentCoins;
        currentCoins += value;
    }
    private void Update()
    {
        if (initialCoins != currentCoins)
        {
            initialCoins += (_coinsTextAnimationTime * Time.deltaTime) * (currentCoins - initialCoins);
            if (initialCoins >= currentCoins)
            {
                initialCoins = currentCoins;
            }
            _coinsText.text = initialCoins.ToString("0");
        }
    }

    public void ShakeConis()
    {
        _coinsPanel.rectTransform.DOShakePosition(_coinsTextAnimationTime * 2, 5);
    }

    public void UpdateBackpackStatus(int maxBackpack, int currentBackpack)
    {
        _backpackText.text = currentBackpack + "/" + maxBackpack;
    }

}
