using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text GemText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GemText.text = "Score : " + score;
    }

    // Update is called once per frame
    void Update()
    {  
    }

    public void AddGem(int Gem)
    {
        score += Gem;
        GemText.text = "Score : " + score;
    }
}   

