using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGameObject : MonoBehaviour
{
    [SerializeField] private GameObject Screen;
    [SerializeField] AudioSource ButtonAudio;
    public void EnableScreen()
    {
        ButtonAudio.Play();
        Screen.SetActive(true);
    }

    public void DisableScreen()
    {
        ButtonAudio.Play();
        Screen.SetActive(false);
    }
}
