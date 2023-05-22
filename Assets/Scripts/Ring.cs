using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    // Getter and Setter of the ring game objects;
    [SerializeField] private int ringNumber;
    [SerializeField] private int currentPole;

    public int GetRingNumber()
    {
        return ringNumber;
    }
    public void SetRingNumber(int _ringNumber)
    {
        ringNumber = _ringNumber;
    }

    public int GetCurrentPole()
    {
        return currentPole;
    }

    public void SetCurrentPole(int _currentPole)
    {
        currentPole = _currentPole;
    }


}
