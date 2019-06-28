using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ClawMovement : MonoBehaviour
{
    public float speed = 6f;
    public float retractSpeed = 24f;
    public float rotateSpeed = 120f;
    public int maxSegments = 40;
    public float distBetweenSegments = 0.75f;
    public float thickness = 0.6f;

    private LineRenderer line;
    private int segmentCount;

    public bool isRetracting { get; set; }
    public System.Action OnClawRetracted;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = line.endWidth = thickness;
        line.positionCount = maxSegments + 1;
        line.SetPosition(0, transform.position);
        transform.Translate(Vector3.forward * distBetweenSegments, Space.Self);
        for(int i = 1; i < maxSegments + 1; i++)
            line.SetPosition(i, transform.position);
        segmentCount = 1;
    }

    void Update()
    {
        if (isRetracting)
            Retarct();
        else
            Movement();
    }

    private void Retarct()
    {
        if(segmentCount < 2)
        {
            if(isRetracting)
            {
                isRetracting = false;
                if (OnClawRetracted != null)
                    OnClawRetracted();
            }
            return;
        }

        InputDirection = Vector2.zero;

        //Update positions
        Vector3 pos = Vector3.MoveTowards(transform.position, line.GetPosition(segmentCount - 1), retractSpeed * Time.deltaTime);
        transform.position = pos;
        for (int i = segmentCount; i < maxSegments + 1; i++)
            line.SetPosition(i, pos);

        //Update rotation
        Quaternion lookRotation = Quaternion.LookRotation((line.GetPosition(segmentCount - 1) - line.GetPosition(segmentCount - 2)).normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * (retractSpeed / speed) * Time.deltaTime);

        //Retract segment
        if (Vector3.Distance(line.GetPosition(segmentCount - 1), transform.position) < 0.01f)
            segmentCount--;
    }

    public Vector2 InputDirection { get; set; }

    private void Movement()
    {
        //Camera relative input
        Vector3 inputDirection = Camera.main.transform.TransformDirection(InputDirection);
        inputDirection.y = 0f;
        inputDirection.Normalize();

        if (inputDirection.magnitude > 0.0f)
        {
            //Claw movement
            transform.Translate(Vector3.forward * Time.deltaTime * speed * inputDirection.magnitude, Space.Self);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(inputDirection, Vector3.up), rotateSpeed * Time.deltaTime);
        }
        else if(segmentCount > 2)
        {
            isRetracting = true;
            return;
        }

        //Update line renderer
        for (int i = segmentCount; i < maxSegments + 1; i++)
            line.SetPosition(i, transform.position);
        if (Vector3.Distance(transform.position, line.GetPosition(segmentCount - 1)) > distBetweenSegments)
        {
            segmentCount++;
            if (segmentCount >= maxSegments)
                isRetracting = true;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (line != null && line.positionCount > 0)
    //        for (int i = 0; i < line.positionCount; i++)
    //            Handles.DrawWireDisc(line.GetPosition(i), Vector3.up, 0.1f);
    //}
}
