using TMPro;
using UnityEngine;

public class SoundsButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Start()
    {
        SoundManager.OnMute += OnMute;
    }

    private void OnMute(bool mute)
    {
        buttonText.fontStyle = mute ? FontStyles.Strikethrough : FontStyles.Normal;
    }
}
