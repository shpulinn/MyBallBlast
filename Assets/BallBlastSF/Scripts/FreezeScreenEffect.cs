using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FreezeScreenEffect : MonoBehaviour
{
    [SerializeField] private Image frozenScreenImage;

    public void FreezeScreen()
    {
        frozenScreenImage.DOFade(1f, 2f);
    }

    public void UnfreezeScreen()
    {
        frozenScreenImage.DOFade(0f, 2f);
    }
}
