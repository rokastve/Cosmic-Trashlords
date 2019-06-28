using System.Collections.Generic;
using UnityEngine;

public class HydraulicPress : MonoBehaviour
{
    public Block blockPrefab;
    public int amountToMakeBlock = 5;
    public float pressSpeed = 1f;
    public float releaseSpeed = 4f;

    [Header("Block Forming Visuals")]
    public Transform iron;
    public MeshRenderer blockVisual;
    public MeshRenderer scrapVisual;

    [Header("Containers")]
    public ScrapContainer inputContainer;
    public OutputPallet outputContainer;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool paused = true;
    public bool Paused {
        get { return paused; }
        set {
            if (value == false)
            {
                waitingForReset = false;
                CheckIfEnoughScrap();
            }
            paused = value;
        }
    }

    private bool waitingForReset = false;

    private void Start()
    {
        startPos = Vector3.up;
        endPos = Vector3.zero;
        blockVisual.gameObject.SetActive(false);
        scrapVisual.gameObject.SetActive(false);
    }

    public float PressDirection { get; set; }

    private void Update()
    {
        if (paused)
            return;

        if (!waitingForReset)
        {
            Vector3 targetPos = PressDirection > 0.0f ? endPos : startPos;
            float speed = PressDirection > 0.0f ? pressSpeed : releaseSpeed;
            iron.localPosition = Vector3.MoveTowards(iron.localPosition, targetPos, speed * Time.deltaTime);
            if (Mathf.Abs(iron.localPosition.y - endPos.y) < 0.01f)
            {
                if(CanCreateBlock())
                {
                    blockVisual.gameObject.SetActive(true);
                    scrapVisual.gameObject.SetActive(false);
                    waitingForReset = true;
                }
            }
        }
        else
        {
            iron.localPosition = Vector3.MoveTowards(iron.localPosition, startPos, releaseSpeed * 0.5f * Time.deltaTime);
            if (Mathf.Abs(iron.localPosition.y - startPos.y) < 0.01f)
            {
                waitingForReset = false;
                CreateBlock();
                CheckIfEnoughScrap();
            }
            return;
        }
    }

    private bool CanCreateBlock()
    {
        return inputContainer.Amount >= amountToMakeBlock;
    }

    private void CreateBlock()
    {
        List<Scrap> scraps = inputContainer.Take(amountToMakeBlock);
        Block newBlock = Instantiate(blockPrefab.gameObject).GetComponent<Block>();
        newBlock.Pickup();
        if(outputContainer.IsFull)
            newBlock.Drop(outputContainer.transform.position + Vector3.up, Vector3.zero);
        else
            outputContainer.Put(newBlock);
        blockVisual.gameObject.SetActive(false);
    }

    private void CheckIfEnoughScrap()
    {
        if (CanCreateBlock() == false)
        {
            waitingForReset = false;
            scrapVisual.gameObject.SetActive(false);
        }
        else
        {
            scrapVisual.gameObject.SetActive(true);
        }
    }
}
