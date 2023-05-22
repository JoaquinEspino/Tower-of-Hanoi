using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRing : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 initialPosition;
    private bool onDifferentPole = false;
    private Collider2D lastCollision, currentPole;
    private int ringNumber;
    [SerializeField] private AudioSource ErrorAudio;

    void Start()
    {
        initialPosition = transform.position;
        ringNumber = gameObject.GetComponent<Ring>().GetRingNumber();
    }

    void Update()
    {
        if(dragging)
        {
            //Traslate the position of the ring by the mouseposition
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        if(ringNumber == currentPole.gameObject.GetComponent<PoleManager>().CheckTopRingNumber()) //Can only be able to move the top ring of the stack
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
        
    }

    private void OnMouseUp()
    {
        // You can only place the ring on a different pole when the top ring of the pole is greater than the ring you are holding
        if(onDifferentPole && ringNumber < lastCollision.gameObject.GetComponent<PoleManager>().CheckTopRingNumber()) 
        {
            MakeMove();
        }
        // Only play error sound when attempting to put the ring above a smaller ring
        else if(onDifferentPole && ringNumber > lastCollision.gameObject.GetComponent<PoleManager>().CheckTopRingNumber())
        {
            ErrorAudio.Play();
            transform.position = initialPosition;
        }
        else
        {
            // Return the dragged object to its initial position
            transform.position = initialPosition;
        }
        
        dragging = false;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pole")
        {
            // If the ring is being hovered on a different pole
            if (gameObject.GetComponent<Ring>().GetCurrentPole() != collision.gameObject.GetComponent<PoleManager>().GetPoleNumber())
            {
                // Saves the pole being hovered on
                lastCollision = collision;
                onDifferentPole = true;
                
            }
            else
            {
                currentPole = collision;
            }
        }
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pole")
        {
            // exiting the hovered different pole
            if (gameObject.GetComponent<Ring>().GetCurrentPole() != collision.gameObject.GetComponent<PoleManager>().GetPoleNumber())
            {
                lastCollision = null;
                onDifferentPole = false;
            }
        }
    }

    void MakeMove()
    {
        //Adds the ring to the different pole, removes the ring from the previous pole
        gameObject.GetComponent<Ring>().SetCurrentPole(lastCollision.gameObject.GetComponent<PoleManager>().GetPoleNumber());
        lastCollision.gameObject.GetComponent<PoleManager>().AddRing(gameObject.GetComponent<Ring>());
        onDifferentPole = false;
        dragging = false;
        currentPole.gameObject.GetComponent<PoleManager>().RemoveRing();
        Destroy(gameObject);
    }
}
