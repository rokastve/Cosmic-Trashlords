using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    public float maxAngle = 120f;
    public float rotationSpeed = 90f;
    public float fireCooldown = 1f;
    public float radius = 40f;
    public float reticleMoveSpeed = 5f;

    public Transform[] spawnPoint;
    public Transform barrel;
    public Transform reticle;
    public GameObject projectilePrefab;

    private float timeSinceLastShot = 0f;

    public Vector2 InputDirection { get; set; }
    private bool paused = true;
    public bool Paused
    {
        get { return paused; }
        set {
            reticle.gameObject.SetActive(!value);
            if (value)
                InputDirection = Vector2.zero;
            paused = value;
        }
    }

    void Update()
    {
        if (paused)
            return;
        ReticleMovement();
        TurretRotation();
        FireLasers();
    }

    private void FireLasers()
    {
        if(timeSinceLastShot >= fireCooldown)
        {
            timeSinceLastShot = 0.0f;
            //TODO: projectile pooling?
            for(int i = 0; i < spawnPoint.Length; i++)
                Instantiate(projectilePrefab, spawnPoint[i].position, Quaternion.LookRotation(barrel.TransformDirection(Vector3.forward), Vector3.up));
            GetComponent<AudioSource>().Play();
        }
        timeSinceLastShot += Time.deltaTime;
    }

    private void TurretRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation((reticle.transform.position - transform.position).normalized, Vector3.up);
        barrel.transform.rotation = Quaternion.RotateTowards(barrel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ReticleMovement()
    {
        //Camera relative reticle movement
        if (InputDirection.magnitude == 0.0f)
            return;

        Vector3 camRelative = Camera.main.transform.TransformDirection(InputDirection);
        camRelative.y = 0f;
        camRelative.Normalize();
        reticle.transform.Translate(camRelative * Time.deltaTime * reticleMoveSpeed, Space.World);

        //Clamp within arc
        Vector3 clamped = Vector3.ClampMagnitude(reticle.transform.localPosition, radius);
        float angle = Vector3.SignedAngle(transform.forward, (transform.TransformPoint(reticle.localPosition) - transform.position).normalized, Vector2.up);
        if (Mathf.Abs(angle) > maxAngle / 2f)
        {
            clamped = Quaternion.Euler(0f, maxAngle * Mathf.Sign(angle) / 2f, 0f) * transform.forward * clamped.magnitude;
            clamped = reticle.InverseTransformDirection(clamped);
        }
        reticle.transform.localPosition = clamped;
    }

    /*
    private void OnDrawGizmos()
    {
        Handles.color = new Color(0.0f, 0.0f, 1.0f, 0.025f);
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(Vector3.up * -maxAngle / 2f) * transform.forward, maxAngle, radius);
    }
    */
}
