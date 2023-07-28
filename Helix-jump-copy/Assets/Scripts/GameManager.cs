using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector ]public int score;
    [Header("Score Settings")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private BallController ballController;

    private void Start()
    {
        ballController.newScore.AddListener(ShowText);
    }

    private void ShowText()
    {
        scoreText.text = score.ToString();
    }
}
