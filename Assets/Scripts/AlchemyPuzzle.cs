using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyPuzzle : MonoBehaviour
{
    public GameObject beakerOne;
    public GameObject beakerTwo;
    public GameObject beakerThree;
    public GameObject beakerFour;
    public int targetBeakerOne;
    public int targetBeakerTwo;
    public int targetBeakerThree;
    public int targetBeakerFour;
    [Header("Don't Touch These")]
    public float currentBeakerOne;
    public float currentBeakerTwo;
    public float currentBeakerThree;
    public float currentBeakerFour;

    private void Start()
    {
        beakerOne.transform.localScale = new Vector3(beakerOne.transform.localScale.x, currentBeakerOne / 10, beakerOne.transform.localScale.z);
        beakerTwo.transform.localScale = new Vector3(beakerTwo.transform.localScale.x, currentBeakerTwo / 10, beakerTwo.transform.localScale.z);
        beakerThree.transform.localScale = new Vector3(beakerThree.transform.localScale.x, currentBeakerThree / 10, beakerThree.transform.localScale.z);
        beakerFour.transform.localScale = new Vector3(beakerFour.transform.localScale.x, currentBeakerFour / 10, beakerFour.transform.localScale.z);
        enabled = false;
    }

    private void Update()
    {
        if (currentBeakerOne == targetBeakerOne && 
            currentBeakerTwo == targetBeakerTwo &&
            currentBeakerThree == targetBeakerThree &&
            currentBeakerFour == targetBeakerFour)
        {
            print("COMPLETE");
        }
        beakerOne.transform.localScale = new Vector3(beakerOne.transform.localScale.x, currentBeakerOne / 10, beakerOne.transform.localScale.z);
        beakerTwo.transform.localScale = new Vector3(beakerTwo.transform.localScale.x, currentBeakerTwo / 10, beakerTwo.transform.localScale.z);
        beakerThree.transform.localScale = new Vector3(beakerThree.transform.localScale.x, currentBeakerThree / 10, beakerThree.transform.localScale.z);
        beakerFour.transform.localScale = new Vector3(beakerFour.transform.localScale.x, currentBeakerFour / 10, beakerFour.transform.localScale.z);
    }
    public void UP(int beakerNumber)
    {
        switch (beakerNumber)
        {
            case 1:
                if (currentBeakerOne <= 9)currentBeakerOne += 1;
                break;
            case 2:
                if (currentBeakerTwo <= 9) currentBeakerTwo += 1;
                break;
            case 3:
                if (currentBeakerThree <= 9) currentBeakerThree += 1;
                break;
            case 4:
                if (currentBeakerFour <= 9) currentBeakerFour += 1;
                break;
        }
    }
    public void DOWN(int beakerNumber)
    {
        switch (beakerNumber)
        {
            case 1:
                if (currentBeakerOne >= 1) currentBeakerOne -= 1;
                break;
            case 2:
                if (currentBeakerTwo >= 1) currentBeakerTwo -= 1;
                break;
            case 3:
                if (currentBeakerThree >= 1) currentBeakerThree -= 1;
                break;
            case 4:
                if (currentBeakerFour >= 1) currentBeakerFour -= 1;
                break;
        }
    }
}
