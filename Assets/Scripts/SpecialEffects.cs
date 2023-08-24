using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SpecialEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathEffect;
    private MeshRenderer _renderer;
    private Color _baseColor;
    private void Awake()
    {
        this._renderer = gameObject.GetComponent<MeshRenderer>();
        this._baseColor = _renderer.material.color;
    }

    // This method listens to HealthManager "onHealthChanged" events. The actual
    // event listening is set up within the editor interface. This is purely for
    // visuals currently, and takes a fractional value between 0 and 1.
    public void UpdateHealth(float frac)
    {
        this._renderer.material.color = this._baseColor * frac;
    }

    // Same as above, but listens to onDeath events.
    public void Kill()
    {
        var particles = Instantiate(this.deathEffect);
        particles.transform.position = transform.position;
    }


}
