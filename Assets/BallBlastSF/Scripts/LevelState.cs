using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private Cart cart;
    [SerializeField] private StoneSpawner stoneSpawner;
    
    [Header("Events")]
    public UnityEvent OnLevelComplete;
    public UnityEvent OnLevelFailed;
    
    private float time;
    private bool isSpawnComplete;
    private bool isGameOver;

    private void Awake()
    {
        cart.CollisionEvent.AddListener(OnCartCollision);
        stoneSpawner.OnSpawnComplete.AddListener(OnSpawnComplete);
    }

    private void OnSpawnComplete()
    {
        if (stoneSpawner.GetStonesAmount() != 0) return;
        
        SoundManager.PlaySound(SoundType.Win);
        OnLevelComplete?.Invoke();
        isSpawnComplete = true;
    }

    private void OnCartCollision()
    {
        if(isGameOver) return;
        SoundManager.PlaySound(SoundType.Lose);
        OnLevelFailed?.Invoke();
        isGameOver = true;
    }

    private void OnDestroy()
    {
        cart.CollisionEvent.RemoveListener(OnCartCollision);
        stoneSpawner.OnSpawnComplete.RemoveListener(OnSpawnComplete);
    }
}
