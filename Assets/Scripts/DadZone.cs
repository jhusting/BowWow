using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DadZone : MonoBehaviour
{
    private ScoreText ScoreController;
    // Start is called before the first frame update
    void Start()
    {
        ScoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(ScoreController.score > Statics.HighScore)
                Statics.HighScore = ScoreController.score;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
