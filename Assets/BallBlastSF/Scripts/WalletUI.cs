using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [Space] [SerializeField] private Cart cart;
    
    private Wallet wallet;

    private void Start()
    {
        wallet = cart.GetWallet();
        
        wallet.OnBalanceChanged += OnBalanceChanged;
        
        UpdateCoinsText();
    }

    private void OnDisable()
    {
        wallet.OnBalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged(int newBalance)
    {
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        coinsText.text = wallet.Balance.ToString();
    }
}
