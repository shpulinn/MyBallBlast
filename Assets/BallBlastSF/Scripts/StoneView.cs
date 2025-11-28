using UnityEngine;

public class StoneView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject destroyParticlePrefab;
    
    private Color color;

    public void SetColor(Color color)
    {
        this.color = color;
        spriteRenderer.color = color;
    }

    public void PlayDestroyParticle()
    {
        ParticleSystem particle = Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        var mainModule = particle.main;
        mainModule.startColor = this.color;
    }
}
