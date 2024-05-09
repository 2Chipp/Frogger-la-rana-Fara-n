using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpetualRotation : MonoBehaviour
{
    public float speedRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.root.rotation.y > 0) transform.Rotate(-Vector3.up, speedRotation * Time.deltaTime);
        else transform.Rotate(Vector3.up, speedRotation * Time.deltaTime);
    }
}
