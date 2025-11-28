using UnityEngine;

public class CartInputControl : MonoBehaviour
{
    [SerializeField] private Cart cartMovement;
    [SerializeField] private Turret turret;

    private void Update()
    {
        if (cartMovement)
        {
            cartMovement.SetMovementTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (turret && Input.GetMouseButton(0))
        {
            turret.Fire();
        }
    }
}
