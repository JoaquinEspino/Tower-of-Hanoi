using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private Timer timer;
    [SerializeField] private MovesManager moveManager;
    [SerializeField] private GameObject SolutionScreen;
    [SerializeField] private GameObject YouWin, TryAgain;

    public IEnumerator EnableWinScreen()
    {
        // Waits 1 second before goind to the end screen
        timer.EndTime();
        yield return new WaitForSeconds(1.0f);
        WinScreen.SetActive(true);
        YouWin.SetActive(true);
        TryAgain.SetActive(false);
        moveManager.GetMoveCount();
        // To set to normal time speed after exiting Solve screen
        Time.timeScale = 1;
        SolutionScreen.SetActive(false);

    }

    public void EnableGiveUpScreen()
    {
        WinScreen.SetActive(true);
        YouWin.SetActive(false);
        TryAgain.SetActive(true);
        timer.EndTime();
        moveManager.GetMoveCount();
        SolutionScreen.SetActive(false);
    }
}
