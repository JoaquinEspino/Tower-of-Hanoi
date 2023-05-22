using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Solve : MonoBehaviour
{
    [SerializeField] PoleManager Left, Middle, Right;
    [SerializeField] SetRingCount setsRingCount;
    [SerializeField] TextMeshProUGUI moveText, timeSpeedText;
    int ringCount;
    Transform topRing;

    private void Update()
    {
        timeSpeedText.text = (int)Time.timeScale + "x";
    }
    // Recursive solution for the tower of hanoi
    IEnumerator Solution(int _ringCount, PoleManager source, PoleManager destination, PoleManager helper)
    {
        //buffer before making a move
        yield return new WaitForSeconds(1f);
        // Base case
        if (_ringCount == 1)
        {
            foreach(Transform obj in source.GetPoleBase())
            {
                if(obj.gameObject.GetComponent<Ring>().GetRingNumber() == source.CheckTopRingNumber())
                {
                    topRing = obj;
                }
            }
            //Add the top ring of the source stack to the destination stack
            moveText.text = "Move disk " + topRing.gameObject.GetComponent<Ring>().GetRingNumber() + " from tower " + (source.GetPoleNumber() + 1) + " to tower " + (destination.GetPoleNumber() + 1);
            topRing.gameObject.GetComponent<Ring>().SetCurrentPole(destination.GetPoleNumber());
            destination.AddRing(topRing.gameObject.GetComponent<Ring>());
            source.RemoveRing();
            foreach (Transform obj in source.GetPoleBase())
            {
                if (obj.gameObject.GetComponent<Ring>().GetRingNumber() == topRing.gameObject.GetComponent<Ring>().GetRingNumber())
                {
                    Destroy(obj.gameObject);
                }
            }

        }
        else
        {
            yield return Solution(_ringCount - 1, source, helper, destination);

            //Add the top ring of the source stack to the destination stack
            foreach (Transform obj in source.GetPoleBase())
            {
                if (obj.gameObject.GetComponent<Ring>().GetRingNumber() == source.CheckTopRingNumber())
                {
                    topRing = obj;
                }
            }
            moveText.text = "Move disk " + topRing.gameObject.GetComponent<Ring>().GetRingNumber() + " from tower " + (source.GetPoleNumber() + 1) + " to tower " + (destination.GetPoleNumber() + 1);
            topRing.gameObject.GetComponent<Ring>().SetCurrentPole(destination.GetPoleNumber());
            destination.AddRing(topRing.gameObject.GetComponent<Ring>());
            source.RemoveRing();
            foreach (Transform obj in source.GetPoleBase())
            {
                if (obj.gameObject.GetComponent<Ring>().GetRingNumber() == topRing.gameObject.GetComponent<Ring>().GetRingNumber())
                {
                    Destroy(obj.gameObject);
                }
            }

            yield return Solution(_ringCount - 1, helper, destination, source);

        }
        //buffer before making a move
        yield return new WaitForSeconds(1.0f);
        

    }

    // Loads the solution scene
    public void ShowSolution()
    {
        moveText.text = "Loading Solution...";
        setsRingCount.LoadSolution();
        ringCount = setsRingCount.ringCount;
        StartCoroutine(Solution(ringCount, Left, Right, Middle));
    }

    public void FastForward()
    {
        if(Time.timeScale <= 10)
        {
            Time.timeScale++;
        }
    }

    public void SlowMo()
    {
        if (Time.timeScale > 1)
        {
            Time.timeScale--;
        }
    }




}
