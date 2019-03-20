using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreController : MonoBehaviour
{
    public Text HighScore;
    // Start is called before the first frame update
    void Start()
    {
        HighScore = GetComponent<Text>();

        HighScore.text = "High Score: " + Mathf.FloorToInt(Statics.HighScore);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Bark"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
