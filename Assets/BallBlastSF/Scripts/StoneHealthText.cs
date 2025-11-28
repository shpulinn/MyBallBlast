using TMPro;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class StoneHealthText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();
        destructible.ChangeHealth.AddListener(OnChangeHealth);
    }

    private void OnDestroy()
    {
        destructible.ChangeHealth.RemoveListener(OnChangeHealth);
    }

    private void OnChangeHealth()
    {
        int healthPoints = destructible.GetHealth();

        if (healthPoints >= 1000)
            healthText.text = healthPoints / 1000 + "k";
        else
            healthText.text = healthPoints.ToString();
    }
}