using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Container<ItemType> : MonoBehaviour where ItemType : ICollectable
{
    [SerializeField] public LevelOfFill[] levelOfFill;

    public MeshFilter meshFilter;
    public int maxCapacity = 5;

    public List<ItemType> items { get; private set; }
    public int Amount { get { return items.Count; } }
    public float FillPercentage { get { return (float)Amount / (float)maxCapacity; } }
    public bool IsFull { get { return items.Count >= maxCapacity; } }
    public bool IsEmpty { get { return items.Count == 0; } }

    public event System.Action<int, int> OnAmountChanged = delegate { };

    protected virtual void Start()
    {
        items = new List<ItemType>(maxCapacity);
        UpdateLevelOfFill();
    }

    public bool Put(ItemType item)
    {
        if (IsFull)
            return false;
        items.Add(item);
        UpdateLevelOfFill();
        return true;
    }

    public ItemType Take()
    {
        ItemType item = items[0];
        items.RemoveAt(0);
        UpdateLevelOfFill();
        return item;
    }

    public List<ItemType> Take(int amount)
    {
        int take = Mathf.Min(amount, items.Count);
        List<ItemType> takeItems = items.Take(take).ToList();
        items.RemoveRange(0, take);
        UpdateLevelOfFill();
        return takeItems;
    }

    private void UpdateLevelOfFill()
    {
        if(IsEmpty)
            meshFilter.GetComponent<MeshRenderer>().enabled = false;
        else
        {
            meshFilter.GetComponent<MeshRenderer>().enabled = true;
            int index = GetMeshIndex();
            meshFilter.mesh = levelOfFill[index].mesh;
        }
        OnAmountChanged(Amount, maxCapacity);
    }

    private int GetMeshIndex()
    {
        int index = 0;
        while (items.Count > levelOfFill[index].amountOfItems)
            index++;
        return index;
    }
}

[System.Serializable]
public class LevelOfFill
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public int amountOfItems;
}

public interface ICollectable
{
    void Pickup();
    void Drop(Vector3 pos, Vector3 force);
}
