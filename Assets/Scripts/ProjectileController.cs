// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10;
    [SerializeField] private ParticleSystem collisionParticles;

    [SerializeField] private int damageAmount = 50;
    [SerializeField] private string tagToDamage;

    private Vector3 _projectileDirection;

    public void SetDirection(Vector3 dir)
    {
        this._projectileDirection = dir;
    }
    private void Update()
    {
        transform.Translate(this._projectileDirection * this.projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == this.tagToDamage)
        {
            // Damage object with relevant tag. Note that this assumes the 
            // HealthManager component is attached to the respective object.
            var healthManager = col.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(this.damageAmount);

            // Create collision particles in opposite direction to movement.
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;
            particles.transform.rotation =
                Quaternion.LookRotation(-this._projectileDirection);

            // Destroy self.
            Destroy(gameObject);
        }
    }
}
