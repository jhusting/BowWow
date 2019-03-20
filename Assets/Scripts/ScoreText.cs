using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public Text multText;

    public float score = 0f;
    public int multiplier = 1;

    private float currSize = 1f;
    private float startSize = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currSize = Mathf.Lerp(currSize, startSize, Time.deltaTime * 2.5f);

        scoreText.text = Mathf.FloorToInt(score).ToString();
        multText.text = multiplier + "x";

        //multText.transform.localScale = new Vector3(currSize, currSize, currSize);
        multText.rectTransform.localScale = new Vector3(currSize, currSize, currSize);
    }

    public void AddScore(float x)
    {
        score += multiplier * x;
    }

    public void AddMultiplier(int i)
    {
        multiplier += i;
        currSize = 1.7f;
    }
}
