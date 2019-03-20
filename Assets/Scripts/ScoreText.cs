using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public Text multText;
    public Text wowText;
    public string[] wows;

    public float score = 0f;
    public int multiplier = 1;
    public bool wowActive = false;
    public float moveTime = .7f;

    private float currSize = 1f;
    private float startSize = 1f;
    private float wowStartHeight = 105;
    private float wowEndHeight = 150;
    private float rMoveTime = .7f;
    private float currMoveTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wowActive)
        {
            float lerpY = Mathf.Lerp(wowStartHeight, wowEndHeight, currMoveTime / rMoveTime);
            wowText.rectTransform.anchoredPosition = new Vector2(-75, lerpY);

            float alpha = 1 - (-1 * (1 - Mathf.Pow(currMoveTime / rMoveTime, 6)) + 1);
            //wowText.color.a

            Color newC = wowText.color;
            newC.a = alpha;
            wowText.color = newC;

            currMoveTime += Time.deltaTime;
            if(currMoveTime >= rMoveTime)
            {
                wowActive = false;
                currMoveTime = 0f;
            }
        }

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
        wowText.rectTransform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-15, 15));
        wowActive = true;
        currMoveTime = 0f;
        wowText.text = wows[Mathf.FloorToInt(Random.Range(0, wows.Length))];


        multiplier += i;
        currSize = 1.7f;
    }
}
