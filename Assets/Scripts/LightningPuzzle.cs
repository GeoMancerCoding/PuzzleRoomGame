using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPuzzle : MonoBehaviour
{
    public GameObject lightningObj;
    public Material lightningMaterial;
    public Light lightningLight;
    public bool isMorseCode;
    public int designatedRedNumber;
    public int designatedBlueNumber;
    public int designatedGreenNumber;
    public int designatedYellowNumber;
    public float lightningStrikeInbetweenTimer;
    [SerializeField] int lightningLvl;
    [SerializeField] float timer;
    [SerializeField] float maxTimer;
    private void Start()
    {
        lightningObj.SetActive(false);
        if (isMorseCode)
        {
            maxTimer = 1;
        }
        else
        {
            maxTimer = lightningStrikeInbetweenTimer;
        }

    }
    public void Update()
    {
        if (timer < maxTimer) timer += Time.deltaTime;
        if (isMorseCode)
        {
            if (timer >= maxTimer)
            {
                if (lightningLvl < 4) lightningLvl += 1;
                else lightningLvl = 1;
                switch (lightningLvl)
                {
                    case 1:
                        maxTimer = 8;
                        SetColor(1);
                        StartCoroutine(MorseCodeCreator(designatedRedNumber));
                        break;
                    case 2:
                        maxTimer = 8;
                        SetColor(2);
                        StartCoroutine(MorseCodeCreator(designatedBlueNumber));
                        break;
                    case 3:
                        maxTimer = 8;
                        SetColor(3);
                        StartCoroutine(MorseCodeCreator(designatedGreenNumber));
                        break;
                    case 4:
                        maxTimer = 8;
                        SetColor(4);
                        StartCoroutine(MorseCodeCreator(designatedYellowNumber));
                        break;
                }
                timer = 0;
            }
        }
        else
        {
            if (timer >= maxTimer)
            {
                if (lightningLvl < 4) lightningLvl += 1;
                else lightningLvl = 1;
                switch (lightningLvl)
                {
                    case 1:
                        maxTimer = lightningStrikeInbetweenTimer + designatedRedNumber;
                        SetColor(1);
                        StartCoroutine(NonMorseCodeCreator(designatedRedNumber));
                        break;
                    case 2:
                        maxTimer = lightningStrikeInbetweenTimer + designatedBlueNumber;
                        SetColor(2);
                        StartCoroutine(NonMorseCodeCreator(designatedBlueNumber));
                        break;
                    case 3:
                        maxTimer = lightningStrikeInbetweenTimer + designatedGreenNumber;
                        SetColor(3);
                        StartCoroutine(NonMorseCodeCreator(designatedGreenNumber));
                        break;
                    case 4:
                        maxTimer = lightningStrikeInbetweenTimer + designatedYellowNumber;
                        SetColor(4);
                        StartCoroutine(NonMorseCodeCreator(designatedYellowNumber));
                        break;
                }
                timer = 0;
            }
        }
/*        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetColor(1);
            StartCoroutine(MorseCodeCreator(designatedRedNumber));
        }*/
    }
    void SetColor(int color)
    {
        switch (color)
        {
            case 1:
                lightningMaterial.SetColor("_EmissionColor", Color.red);
                lightningLight.color = Color.red;
                break;
            case 2:
                lightningMaterial.SetColor("_EmissionColor", Color.blue);
                lightningLight.color = Color.blue;
                break;
            case 3:
                lightningMaterial.SetColor("_EmissionColor", Color.green);
                lightningLight.color = Color.green;
                break;
            case 4:
                lightningMaterial.SetColor("_EmissionColor", Color.yellow);
                lightningLight.color = Color.yellow;
                break;
        }
    }
    IEnumerator MorseCodeCreator(int imputNumber)
    {
        switch (imputNumber)
        {
            case 0:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                break;
            case 1:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                break;
            case 2:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                break;
            case 3:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                break;
            case 4:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                break;
            case 5:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                break;
            case 6:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                break;
            case 7:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                break;
            case 8:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                break;
            case 9:
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(1);
                lightningObj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                lightningObj.SetActive(false);
                break;
        }

    }
    IEnumerator NonMorseCodeCreator(int imputNumber)
    {
        for (int i = 0; i < imputNumber; i++)
        {
            lightningObj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            lightningObj.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
}
