using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject PuzzleUI1;
    public GameObject PuzzleUI2;
    public GameObject PuzzleUI3;
    public GameObject Stepeschope;
    // Start is called before the first frame update
    void Start()
    {
        PuzzleUI1.SetActive(false);
        PuzzleUI2.SetActive(false);
        PuzzleUI3.SetActive(false);
        Stepeschope.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
