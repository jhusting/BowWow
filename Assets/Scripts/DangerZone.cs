﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public AudioSource bork;

    public float dangTimer = -1f;
    // Start is called before the first frame update
    void Start()
    {
        bork = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dangTimer += Time.deltaTime;

        if(dangTimer > .6f)
        {
            dangTimer = -1f;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            SquirrelController sq = other.GetComponent<SquirrelController>();

            if (sq)
            {
                sq.runningModifier = 6.0f;
                sq.exclamation.SetActive(true);
            }
        }
    }
}
