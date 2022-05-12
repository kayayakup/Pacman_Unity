using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Move();
        }
    }

    public void Move()
    {
        float x = Random.Range(-4.5f, 4.5f);
        float z = Random.Range(-5f, 3.5f);

        transform.position = new Vector3(x, 0f, z);
    }
}
