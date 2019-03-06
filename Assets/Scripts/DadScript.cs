﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadScript : MonoBehaviour
{
    public PlayerController player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 1f * Time.deltaTime));
        transform.LookAt(player.transform.position);
    }
}
