using UnityEngine;

public class ExplodeAtClick : MonoBehaviour
{
    public float force = 500f;
    public float radius = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Rigidbody[] cubes = transform.GetComponentsInChildren<Rigidbody>();
                for(int i = 0; i < cubes.Length; i++)
                {
                    cubes[i].AddExplosionForce(force, hit.point, radius);
                }
            }
        }
    }
}
