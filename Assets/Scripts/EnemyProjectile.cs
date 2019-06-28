using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Debug.Log("Hit spaceship");
            other.gameObject.GetComponentInParent<ShipHealth>().DecreaseHealth(damage);
            Destroy(this.gameObject);

        }
    }
}
