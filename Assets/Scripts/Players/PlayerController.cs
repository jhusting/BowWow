using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public BoomCamera Camera;
    public float speed = 5f;
    public float _moveSpeedModifier = 500f;
    public DangerZone BarkZone;
    public float BarkCooldown = 0.25f;
    public ParticleSystem ps;
         
    protected Vector3 inputVector;
    protected Rigidbody rb;
    private float currBarkTime = -1f;
    
    protected void Start()
    {
        inputVector = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
        ps = ps.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (currBarkTime >= 0f)
        {
            currBarkTime += Time.deltaTime;
            if (currBarkTime >= BarkCooldown)
                currBarkTime = -1f;
        }
        //if(Input.GetAxis("Bark") > 0 && currBarkTime < 0f)
        if(Input.GetButtonDown("Bark") && currBarkTime < 0f)
        {
            currBarkTime = 0f;
            //BarkZone.dangerous = true;
            BarkZone.gameObject.SetActive(true);
            BarkZone.dangTimer = 0f;
            BarkZone.bork.Play();
            ps.Play();
        }
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
            transform.LookAt(transform.position + inputVector);
        }
    }
}
