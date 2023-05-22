using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    int moveCount;
    [SerializeField] TextMeshProUGUI moveCountText;
    [SerializeField] TextMeshProUGUI finalMoveCountText;

    // Increments the move counter when a move is made
    public void AddMove()
    {
        moveCount++;
        moveCountText.text = "Moves: " + moveCount.ToString();
    }

    // Saves the move count after the game ends to display
    public int GetMoveCount()
    {
        moveCountText.text = "Moves: 0";
        finalMoveCountText.text = "Moves Made: " + moveCount.ToString();
        int _moveCount = moveCount;
        moveCount = 0;
        return _moveCount;
    }
}
