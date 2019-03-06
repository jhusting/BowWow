using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float SpeedModifier = 3f;
    private Vector3 motion;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        motion = new Vector3(0f, 0f, 0f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            motion.x = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            motion.z = Input.GetAxis("Vertical");
        }

        rb.MovePosition(transform.position + motion * SpeedModifier);
    }
}
