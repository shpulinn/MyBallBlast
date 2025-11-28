using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static LevelBoundary Instance;

    [SerializeField] private Vector2 screenResolution;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (Application.isEditor == false && Application.isPlaying)
        {
            screenResolution = new Vector2(Screen.width, Screen.height);
        }
    }
    
    public float LeftBorder => Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
    public float RightBorder => Camera.main.ScreenToWorldPoint(new Vector3(screenResolution.x, 0, 0)).x;
    
    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(LeftBorder, -10, 0), new Vector3(LeftBorder, 10, 0));
        Gizmos.DrawLine(new Vector3(RightBorder, -10, 0), new Vector3(RightBorder, 10, 0));
    }

    #endif
}
