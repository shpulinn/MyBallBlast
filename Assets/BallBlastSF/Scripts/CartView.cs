using System;
using UnityEngine;

public class CartView : MonoBehaviour
{
    [SerializeField] private GameObject leftWheel;
    [SerializeField] private GameObject rightWheel;
    [SerializeField] private float wheelRadius;
    [SerializeField] private Cart cart;
    [SerializeField] private Turret turret;
    [SerializeField] private ParticleSystem shootParticles;

    private void Start()
    {
        turret.OnFire.AddListener(PlayParticles);
    }

    void Update()
    {
        float deltaMovement = cart.GetDeltaMovement();
        float angle = (180 * deltaMovement) / (Mathf.PI * wheelRadius * 2);
        leftWheel.transform.Rotate(Vector3.forward, -angle);
        rightWheel.transform.Rotate(Vector3.forward, -angle);
    }
    
    private void PlayParticles() => shootParticles.Play();

    private void OnDestroy()
    {
        turret.OnFire.RemoveListener(PlayParticles);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(leftWheel.transform.position, wheelRadius);
        Gizmos.DrawWireSphere(rightWheel.transform.position, wheelRadius);
    }
    #endif
}
