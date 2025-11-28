using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float reboundSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravityOffset;
    
    private bool useGravity;
    private Vector3 velocity;
    private bool isFrozen = false;

    private void Awake()
    {
        velocity.x = -Mathf.Sign(transform.position.x) * horizontalSpeed;
    }

    void Update()
    {
        if (isFrozen) return; // Полная остановка
        
        TryEnableGravity();
        Move();
    }

    private void Move()
    {
        if (useGravity)
        {
            velocity.y -= gravity * Time.deltaTime;
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime * Mathf.Sign(velocity.x));
        }

        velocity.x = Mathf.Sign(velocity.x) * horizontalSpeed;
        
        transform.position += velocity * Time.deltaTime;
    }

    public void AddVerticalVelocity(float velocity)
    {
        this.velocity.y += velocity;
    }

    public void SetHorizontalDirection(float direction)
    {
        velocity.x = Mathf.Sign(direction) * horizontalSpeed;
    }

    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelEdge edge = other.GetComponent<LevelEdge>();

        if (edge == null) return;
        
        switch (edge.EdgeType)
        {
            case EdgeType.Bottom:
                velocity.y = reboundSpeed;
                break;
            case EdgeType.Left when velocity.x < 0:
            case EdgeType.Right when velocity.x > 0:
                velocity.x *= -1;
                break;
        }
    }

    private void TryEnableGravity()
    {
        if (Mathf.Abs(transform.position.x) <= Mathf.Abs(LevelBoundary.Instance.LeftBorder) - gravityOffset)
        {
            useGravity = true;
        }
    }
    
    public void Freeze()
    {
        isFrozen = true;
        // Опционально: визуальный эффект льда
        // stoneView.SetFrozen(true);
    }

    public void Unfreeze()
    {
        isFrozen = false;
        // stoneView.SetFrozen(false);
    }
}
