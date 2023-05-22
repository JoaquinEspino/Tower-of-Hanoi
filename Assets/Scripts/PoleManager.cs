using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoleManager : MonoBehaviour
{
    [SerializeField] private Ring ring;
    [SerializeField] private Stack<Ring> ringStack = new Stack<Ring>();
    [SerializeField] private Transform poleBase;
    [SerializeField] private int poleNumber;
    [SerializeField] private int ringCount;
    [SerializeField] private WinGame WinScript;
    [SerializeField] private AudioSource StackAudio;
    [SerializeField] private MovesManager MovesManager;

    // Generates the initial rings in the game and solution scenes
    public void GenerateRings(int ringCount)
    {
        for (int i = 0; ringCount > i; i++)
        {
            Ring tempRing = Instantiate(ring, poleBase);
            tempRing.SetRingNumber(ringCount - i);
            tempRing.SetCurrentPole(0);
            ringStack.Push(tempRing);
            Destroy(tempRing.gameObject);
        }
        RefreshRings();
    }

    // Empties out the stack in the pole
    public void EmptyStack()
    {
        if(poleBase.childCount > 0)
        {
            foreach(Transform obj in poleBase)
            {
                Destroy(obj.gameObject);
            }
        }
        ringStack.Clear();
        RefreshRings();
    }

    // Adds a ring to the stack
    public void AddRing(Ring _ring)
    {
        MovesManager.AddMove();
        StackAudio.Play();
        ringStack.Push(_ring);
        RefreshRings();
        CheckWinCondition();
    }

    // Removes the top ring of the stack
    public void RemoveRing()
    {
        ringStack.Pop();
        RefreshRings();
    }

    // Instanciates every ring on the stack into the game scene
    public void RefreshRings()
    {
        int i = 0;
        foreach (Ring ring in ringStack)
        {
            if(ring!=null)
            {
                Ring tempRing = Instantiate(ring, poleBase);
                tempRing.name = "Ring" + tempRing.GetRingNumber();
                tempRing.GetComponent<DragRing>().enabled = true;
                tempRing.GetComponent<Ring>().enabled = true;
                tempRing.GetComponent<CapsuleCollider2D>().enabled = true;
                tempRing.GetComponent<AudioSource>().enabled = true;
                tempRing.GetComponent<CapsuleCollider2D>().size = new Vector2(1.0f, 2.0f) + new Vector2(0.0f, 0.5f * (tempRing.GetRingNumber()));
                tempRing.GetComponent<SpriteRenderer>().size = new Vector2(1.0f, 2.0f) + new Vector2(0.0f, 0.5f * (tempRing.GetRingNumber())); //sets the length based on the ring number
                float offset = (ringStack.Count - i) * 0.5f;
                tempRing.GetComponent<Transform>().localPosition = new Vector3(0.0f, offset, -(ringStack.Count - i)); // stacks the rings on top of each other
                i++;
            }
            
        }

    }

    // Checks the ring number at the top of the stack
    public int CheckTopRingNumber()
    {
        if (ringStack.Count != 0)
        {
            return ringStack.Peek().GetRingNumber();
        }
        else
        {
            // in cases where there is no rings on the stack, every ring number can go in
            return 11;
        }

    }

    public Transform GetPoleBase()
    {
        return poleBase;
    }


    public int GetPoleNumber()
    {
        return poleNumber;
    }

    public void SetRingCount(int _ringCount)
    {
        ringCount = _ringCount;
    }

    void CheckWinCondition()
    {
        // checks if a pole other than the left pole has a stack thats complete
        if (poleNumber != 0 && ringCount == ringStack.Count)
        {
            StartCoroutine(WinScript.EnableWinScreen());
        }
    }
    
    /* COMMENT OUT BEFORE BUILD
    // Adds a custom editor display to display the stack inventory of the pole
    [CustomEditor(typeof(PoleManager))]
    public class StackPreview : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var ts = (PoleManager)target;
            Stack<Ring> stack = ts.ringStack;

            var bold = new GUIStyle();
            bold.fontStyle = FontStyle.Bold;
            GUILayout.Label("Items in my stack", bold);

            // Displays the ring number of each ring in the stack
            foreach (Ring item in stack)
            {
                GUILayout.Label(item.GetRingNumber().ToString());
            }
        }

    }*/

}
