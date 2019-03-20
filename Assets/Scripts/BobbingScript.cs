using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingScript : MonoBehaviour
{
    public GameObject doggo;
    private float timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        Vector3 newPos = doggo.transform.position;
        newPos.y += Mathf.Sin(timePassed*2.75f)/4f;
        transform.position = newPos;
    }
}
