using UnityEngine;

[RequireComponent(typeof(ClawMovement))]
public class Claw : Container<Scrap>
{
    public float openAngle = 30f;
    public float closedAngle = 3f;
    public float wobbleSpeed = 10f;
    public Transform left;
    public Transform right;
    public Transform dropPosition;
    public float dropForce;

    public AudioClip strech;
    public AudioClip retract;

    private AudioSource audioSource;
    private ClawMovement movement;

    private void OnEnable()
    {
        if (movement == null)
            movement = GetComponent<ClawMovement>();
        audioSource = GetComponent<AudioSource>();
        movement.OnClawRetracted += TransferScrap;
        right.localEulerAngles = Vector3.up * closedAngle;
        left.localEulerAngles = Vector3.up * -closedAngle;
    }

    private void OnDisable()
    {
        movement.OnClawRetracted -= TransferScrap;
    }

    private void TransferScrap()
    {
        if(Amount > 0)
        {
            while(!IsEmpty)
            {
                Scrap scrap = Take();
                scrap.Pickup();
                Vector3 randomForce = dropPosition.TransformDirection(Vector3.forward) * dropForce;
                scrap.EnterGravity(dropPosition.position, randomForce);
            }
        }
        audioSource.Stop();
    }

    private void DropScrap()
    {
        if(Amount > 0)
        {
            while(!IsEmpty)
            {
                Scrap scrap = Take();
                scrap.Drop(transform.position, Vector3.zero);
            }
        }
    }

    private void LateUpdate()
    {
        if(movement.isRetracting)
        {
            right.localEulerAngles = Vector3.up * closedAngle;
            left.localEulerAngles = Vector3.up * -closedAngle;
            if (!audioSource.isPlaying || audioSource.clip != retract)
            {
                audioSource.clip = retract;
                audioSource.Play();
            }
        }
        else
        {
            if (movement.InputDirection.magnitude > 0.0f)
            {
                if (!audioSource.isPlaying || audioSource.clip != strech)
                {
                    audioSource.clip = strech;
                    audioSource.Play();
                }
            }
            else
                audioSource.Stop();
            float angle = Mathf.Sin(Time.time * wobbleSpeed) * 6f + openAngle;
            right.localEulerAngles = Vector3.up * angle;
            left.localEulerAngles = Vector3.up * -angle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (movement.isRetracting)
            return;

        if(other.CompareTag("Trash"))
        {
            Scrap scrap = other.GetComponent<Scrap>();
            if(scrap != null)
            {
                if (IsFull)
                    movement.isRetracting = true;
                else
                {
                    Put(scrap);
                    scrap.Pickup();
                }
            }
        }

        if(other.CompareTag("Spaceship"))
        {
            other.GetComponentInParent<ShipHealth>().DecreaseHealth(0.1f + Random.Range(0.01f, 0.05f));
            movement.isRetracting = true;
            DropScrap();
        }
    }
}
