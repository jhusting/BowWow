using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelController : MonoBehaviour
{
    public PlayerController player;
    public bool bRunning = false;
    public float runningModifier = 1f;
    public GameObject exclamation;

    private Rigidbody rb;
    private Animator an;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bRunning && player)
        {
            Vector3 direction = transform.position - player.transform.position;
            direction = direction.normalized * 2f * runningModifier * Time.deltaTime;
            direction.y = 0;
            rb.MovePosition(transform.position + direction);
            transform.LookAt(transform.position + direction);
            an.SetBool("Moving", true);
        }
        else
            an.SetBool("Moving", false);

        if (runningModifier != 1f)
        {
            runningModifier = Mathf.Lerp(runningModifier, 1f, 1.3f * Time.deltaTime);
            if (runningModifier < 1.4f)
                runningModifier = 1f;
        }
        else
            exclamation.SetActive(false);
    }
}
