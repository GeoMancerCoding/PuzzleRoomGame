using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{


    public TextMeshProUGUI TimerText;
    private float remainingTime = 300f;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int second = Mathf.FloorToInt(remainingTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, second);

        if (remainingTime <= 0)
        {
            //Application.Quit();
        }
    }
}
