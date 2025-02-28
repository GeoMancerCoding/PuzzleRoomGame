using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyPuzzle : MonoBehaviour
{
    public Animation Anim;
    public GameObject beakerOne;
    public GameObject beakerTwo;
    public GameObject beakerThree;
    public GameObject beakerFour;
    public int targetBeakerOne;
    public int targetBeakerTwo;
    public int targetBeakerThree;
    public int targetBeakerFour;
    bool finishedPuzzle;
    bool drawerHasBeenOpened;
    public Pickup Stethoscope;
    public UIScript sethaUi;

    [Header("Don't Touch These")]
    [SerializeField] float currentBeakerOne;
    [SerializeField] float currentBeakerTwo;
    [SerializeField] float currentBeakerThree;
    [SerializeField] float currentBeakerFour;

    public AudioSource drawer;

    public AudioSource chemical;

    public AudioSource whisper;

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
        if (finishedPuzzle == false)
        {
            if (currentBeakerOne == targetBeakerOne &&
                currentBeakerTwo == targetBeakerTwo &&
                currentBeakerThree == targetBeakerThree &&
                currentBeakerFour == targetBeakerFour)
            {
                finishedPuzzle = true;
            }
            beakerOne.transform.localScale = new Vector3(beakerOne.transform.localScale.x, currentBeakerOne / 10, beakerOne.transform.localScale.z);
            beakerTwo.transform.localScale = new Vector3(beakerTwo.transform.localScale.x, currentBeakerTwo / 10, beakerTwo.transform.localScale.z);
            beakerThree.transform.localScale = new Vector3(beakerThree.transform.localScale.x, currentBeakerThree / 10, beakerThree.transform.localScale.z);
            beakerFour.transform.localScale = new Vector3(beakerFour.transform.localScale.x, currentBeakerFour / 10, beakerFour.transform.localScale.z);
        }
        if (finishedPuzzle == true && drawerHasBeenOpened == false)
        {
            Anim.Play("OpenSecretCompartment");
            GetComponent<Interactable>().LerpCamToOrigPos(true);
            drawer.Play();
            enabled = false;
            drawerHasBeenOpened = true;
        }
    }
    public void UP(int beakerNumber)
    {
        switch (beakerNumber)
        {
            case 1:
                if (currentBeakerOne <= 8)currentBeakerOne += 1;
                break;
            case 2:
                if (currentBeakerTwo <= 8) currentBeakerTwo += 1;
                break;
            case 3:
                if (currentBeakerThree <= 8) currentBeakerThree += 1;
                break;
            case 4:
                if (currentBeakerFour <= 8) currentBeakerFour += 1;
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

    public void OnFinishOpeningDrawer()
    {
        sethaUi.enabled = true;
        Stethoscope.Enable();
        whisper.Play();
    }

    public void playSpill()
    {
        chemical.Play();
    }
}
