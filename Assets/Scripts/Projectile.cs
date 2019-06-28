using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f;
    public float damage = 1f;
    public float speed = 10f;
    public ParticleSystem hitParticles;

    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Asteroid"))
        {
            ContactPoint contact = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(hitParticles, pos, rot);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("EnemySpaceShip"))
        {
            other.gameObject.GetComponentInParent<EnemyHealth>().gotHit(damage);
            Destroy(this.gameObject);

        }
    }
}
