using System;
using UnityEngine;
using DG.Tweening;

public class MenuPanelUI : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition = new Vector3(0, -1100, 0);
        transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.InOutSine);
    }

    public void StartGame()
    {
        transform.DOLocalMoveY(-1100, 0.5f).SetEase(Ease.InOutSine)
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.InOutSine);
    }

    public void ExitGame() => Application.Quit();
}