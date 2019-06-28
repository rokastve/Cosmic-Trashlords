using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    public GameObject remains;
    public ShipHealth spaceShip;
    public float damage;
    public float lifetime;
    public float Health;
    private float MaxHealth;
    public float dmgWhenGettingShot;
    private Score scoreManager;
    public Slider HB;
    void Start()
    {
        MaxHealth = Health;
        HB.value = Health / MaxHealth;
        spaceShip = FindObjectOfType<ShipHealth>();
        scoreManager = FindObjectOfType<Score>();
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Spaceship"))
        {
            spaceShip.DecreaseHealth(damage);
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.gameObject.tag.Equals("Projectile"))
        {
            Health -= dmgWhenGettingShot;
            HB.value = Health / MaxHealth;
            if(Health <= 0)
            {
                scoreManager.AddScore(100);
                Instantiate(remains, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

    }
}
