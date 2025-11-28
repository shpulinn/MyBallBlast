using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float movementSpeed;
    [SerializeField] private float vehicleWidth;

    [HideInInspector] public UnityEvent CollisionEvent;
    
    private Vector3 movementTarget;
    private float deltaMovement;
    private float lastPositionX;
    
    private Wallet wallet;
    private bool isShieldActive;

    private void Awake()
    {
        wallet = Wallet.Load();
    }

    private void Start()
    {
        movementTarget = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        lastPositionX = transform.position.x;
        
        transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed * Time.deltaTime);
        
        deltaMovement = transform.position.x - lastPositionX;
    }

    public void SetMovementTarget(Vector3 target)
    {
        movementTarget = ClampMovementTarget(target);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Stone stone = other.transform.root.GetComponent<Stone>();

        if (stone == null) return;
        if (isShieldActive) return;
        
        CollisionEvent?.Invoke();
    }

    public Wallet GetWallet()
    {
        return wallet;
    }

    public void AddCoins(int coins)
    {
        wallet?.Deposit(coins);
    }

    public void SpendCoins(int coins)
    {
        wallet?.TryWithdraw(coins);
    }

    public void ActivateShield()
    {
        isShieldActive = true;
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        SoundManager.PlaySound(SoundType.ShieldOff);
    }
    
    private Vector3 ClampMovementTarget(Vector3 target)
    {
        float leftBound = LevelBoundary.Instance.LeftBorder + vehicleWidth * 0.5f;
        float rightBound = LevelBoundary.Instance.RightBorder - vehicleWidth * 0.5f;

        Vector3 moveTarget = target;
        moveTarget.z = transform.position.z;
        moveTarget.y = transform.position.y;
        
        if (moveTarget.x < leftBound) moveTarget.x = leftBound;
        if (moveTarget.x > rightBound) moveTarget.x = rightBound;
        
        return moveTarget;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(
            transform.position - new Vector3(vehicleWidth * 0.5f, 0.5f, 0),
            transform.position + new Vector3(vehicleWidth * 0.5f, -0.5f, 0));
    }
    #endif
    public float GetDeltaMovement()
    {
        return deltaMovement;
    }
}
