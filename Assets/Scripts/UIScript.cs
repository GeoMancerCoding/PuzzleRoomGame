using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{


    public TextMeshProUGUI TimerText;
    private float remainingTime = 300f;
    public GameObject OneSnakePiece;
    public GameObject TwoSnakePiece;
    public GameObject HeartPiece;
    public GameObject Sethascope;
    // Start is called before the first frame update
    void Start()
    {
        OneSnakePiece.SetActive(false);
        TwoSnakePiece.SetActive(false);
        HeartPiece.SetActive(false);
        Sethascope.SetActive(false);
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
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
