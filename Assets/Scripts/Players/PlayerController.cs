using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public BoomCamera Camera;
    public float speed = 5f;
    public float _moveSpeedModifier = 500f;
         
    protected Vector3 inputVector;
    protected Rigidbody rb;
    
    protected void Start()
    {
        inputVector = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CalculateInputVector();
        Move();
    }

    protected void CalculateInputVector()
    {
        Vector3 camRotation;
        camRotation = Camera.transform.eulerAngles; //Get camera rotation

        //Start with forward vector
        Vector3 camForward = new Vector3(0, 0, 1);
        camForward = Quaternion.Euler(0, camRotation.y, 0) * camForward; //Rotate it by camera's y (yaw) rotation, this gets camera forward vector

        Vector3 camRight = Quaternion.Euler(0, 90, 0) * camForward; //rotate THAT by 90 degrees to get right vector

        camRight *= Input.GetAxis("Horizontal"); //get input
        camForward *= Input.GetAxis("Vertical");

        //inputVector = (camRight + camForward) * Time.deltaTime * speed;
        inputVector = (camRight + camForward);
    }

    protected void Move()
    {
        Vector3 newInput = inputVector.normalized;
        if (inputVector.magnitude > .01f)
        {
            rb.MovePosition((transform.position + newInput * Time.deltaTime * speed)); //move rigidbody
        }
    }
}
