using DG.Tweening;
using UnityEngine;
public class ShieldVisualEffect : MonoBehaviour
{
    [SerializeField] private GameObject shieldVisualEffect;

    private void Start()
    {
        shieldVisualEffect.transform.localScale = Vector3.zero;
        shieldVisualEffect.SetActive(false);
    }

    public void Activate()
    {
        shieldVisualEffect.SetActive(true);
        shieldVisualEffect.transform.localScale = Vector3.zero;
        shieldVisualEffect.transform.DOScale(1.15f, 1f);
    }

    public void Deactivate()
    {
        shieldVisualEffect.transform.DOScale(0f, 1f).OnComplete(
            () => shieldVisualEffect.SetActive(false)
            );
    }
}