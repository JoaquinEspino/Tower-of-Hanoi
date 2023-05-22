using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SetRingCount : MonoBehaviour
{
    public int ringCount;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private GameObject StartScreen, EndScreen, GameScreen, SolutionScreen;
    [SerializeField] private PoleManager LeftPole;
    [SerializeField] private AudioSource BGAudio;
    [SerializeField] private AudioSource ButtonAudio;
    void Start()
    {
        // Gets slider value
        slider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString();
            ringCount = (int)v;
            ButtonAudio.Play();
        });
    }

    // Sets the game scene based on the number of rings chosen
    public void SetRings()
    {
        GameScreen.SetActive(true);
        SetPolesRingCount();
        ButtonAudio.Play();
        
        LeftPole.GenerateRings(ringCount);
        BGAudio.volume = 0.1f;
        StartScreen.SetActive(false);
    }

    // Empties the ring stacks in every pole object
    public void SetPolesRingCount()
    {
        GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");

        foreach(GameObject obj in poles)
        {
            obj.GetComponent<PoleManager>().EmptyStack();
            obj.GetComponent<PoleManager>().SetRingCount(ringCount);
        }
    }

    // Sets the solution scene
    public void LoadSolution()
    {
        SetPolesRingCount();
        ButtonAudio.Play();
        LeftPole.GenerateRings(ringCount);
        EndScreen.SetActive(false);
        GameScreen.SetActive(false);
        SolutionScreen.SetActive(true);

    }
}
