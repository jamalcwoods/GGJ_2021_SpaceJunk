using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject smallTitle;

    [SerializeField]
    private GameObject bigTitle;

    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject settingsButton;

    [SerializeField]
    private GameObject quitButton;

    [SerializeField]
    private GameObject astronaut;

    private bool titleSequenceStart = false;
    private bool smallDone = false;
    private bool bigDone = false;

    void Start()
    {
        StartCoroutine("StartTitle");
    }

    private IEnumerator StartTitle()
    {
        yield return new WaitForSeconds(5.0f);
        titleSequenceStart = true;
        StartCoroutine("StartBig");
    }

    private IEnumerator StartBig()
    {
        yield return new WaitForSeconds(9.0f);
        smallDone = true;
        StartCoroutine("StartOptions");
    }

    private IEnumerator StartOptions()
    {
        yield return new WaitForSeconds(8.0f);
        bigDone = true;
    }

    private IEnumerator PlayButton()
    {
        yield return new WaitForSeconds(2.0f);
        playButton.SetActive(true);
    }

    private IEnumerator SettingsButton()
    {
        yield return new WaitForSeconds(2.0f);
        settingsButton.SetActive(true);
    }

    private IEnumerator QuitButton()
    {
        yield return new WaitForSeconds(3.0f);
        quitButton.SetActive(true);
    }

    void FixedUpdate()
    {
        if(smallTitle.GetComponent<TextMeshProUGUI>().color.a < 1 && titleSequenceStart)
        {
            smallTitle.GetComponent<TextMeshProUGUI>().color = new Color(smallTitle.GetComponent<TextMeshProUGUI>().color.r, smallTitle.GetComponent<TextMeshProUGUI>().color.g, smallTitle.GetComponent<TextMeshProUGUI>().color.b, smallTitle.GetComponent<TextMeshProUGUI>().color.a + 0.005f);
        }

        if(smallTitle.GetComponent<TextMeshProUGUI>().color.a >= 1 && (bigTitle.GetComponent<RectTransform>().localScale.x < 1 && bigTitle.GetComponent<RectTransform>().localScale.y < 1) && smallDone)
        {
            bigTitle.GetComponent<RectTransform>().localScale = new Vector3(bigTitle.GetComponent<RectTransform>().localScale.x + 0.01f, bigTitle.GetComponent<RectTransform>().localScale.y + 0.01f, bigTitle.GetComponent<RectTransform>().localScale.z);
        }

        if((bigTitle.GetComponent<RectTransform>().localScale.x >= 1 && bigTitle.GetComponent<RectTransform>().localScale.y >= 1) && bigDone && !playButton.active)
        {
            StartCoroutine("PlayButton");
        }

        if ((bigTitle.GetComponent<RectTransform>().localScale.x >= 1 && bigTitle.GetComponent<RectTransform>().localScale.y >= 1) && bigDone && playButton.active && !settingsButton.active)
        {
            StartCoroutine("SettingsButton");
        }

        if ((bigTitle.GetComponent<RectTransform>().localScale.x >= 1 && bigTitle.GetComponent<RectTransform>().localScale.y >= 1) && bigDone && playButton.active && settingsButton.active && !quitButton.active)
        {
            StartCoroutine("QuitButton");
        }
    }
}
