using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
     public static int scoreValue = 0;
     Text Score;

    void Start()
    {
       Score = GetComponent<Text>();

    }
    
    // Update is called once per frame
    void Update()
    {
        Score.text = scoreValue.ToString();
    }
   
}
