using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadScript : MonoBehaviour
{
    public PlayerController player;
    public float speed = 1f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        float speedMult = 1f;

        if(dist > 10f)
            speedMult += 4f * Mathf.Clamp(dist / 35f, 0f, 1f);

        rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speedMult * speed * Time.deltaTime));
        transform.LookAt(player.transform.position);
    }
}
