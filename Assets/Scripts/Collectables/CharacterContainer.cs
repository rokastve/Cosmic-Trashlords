using UnityEngine;

public class CharacterContainer : Container<Scrap>
{
    public float dropForce = 5;
    public ContainerFillIndicator indicator;

    private CharacterVisuals characterVisuals;
    private float pickupCooldown = 0.5f;
    private float pickupCooldownTimer = 0.0f;

    protected override void Start()
    {
        base.Start();
        characterVisuals = GetComponentInParent<CharacterVisuals>();
        GetComponentInParent<CharacterInput>().BackPressed += PressedBack;
        if (characterVisuals == null)
            Debug.LogError("Visuals is null");
    }

    private void PressedBack(int id)
    {
        if (IsEmpty)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f, LayerMask.GetMask("Container"));
        for(int i = 0; i < colliders.Length; i++)
        {
            Container<Scrap> containerInRange = colliders[i].GetComponent<Container<Scrap>>();
            if(containerInRange != null)
            {
                TransferToContainer(containerInRange);
                return;
            }
        }

        //TODO: play drop animation?
        DropAllItems();
    }

    private void Update()
    {
        if (pickupCooldownTimer < pickupCooldown)
            pickupCooldownTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickupCooldownTimer < pickupCooldown)
            return;
        if (items == null)
            return;
        Scrap item = other.GetComponent<Scrap>();
        if (item != null)
        {
            if (Put(item))
            {
                item.Pickup();
                indicator.Pickup(this);
                characterVisuals.Carry(1.0f);
            }
        }
    }

    private void TransferToContainer(Container<Scrap> container)
    {
        Debug.Log("Transfering scrap to another container");
        while(!this.IsEmpty)
        {
            Scrap item = Take();
            if (!container.IsFull)
            {
                container.Put(item);
                indicator.Drop(this);
            }
            else
                Put(item);
        }
        if (IsEmpty)
            characterVisuals.Carry(0f);
        GetComponent<AudioSource>().Play();
    }

    [ContextMenu("Drop")]
    public void DropAllItems()
    {
        int count = items.Count;
        for(int i = 0; i < count; i++)
        {            
            Scrap item = Take();
            Vector3 randomForce = Quaternion.Euler(0, Random.Range(-30f, 30f), 0f) * transform.forward * dropForce;
            item.Drop(meshFilter.transform.position, randomForce);
        }
        pickupCooldownTimer = 0.0f;

        characterVisuals.Carry(0f);
        indicator.Drop(this);
        GetComponent<AudioSource>().Play();
    }
}
