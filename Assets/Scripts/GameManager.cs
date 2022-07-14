using System;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Playing,
    EndGame
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Action OnUpdate;
    public Action<string> OnGameEnded;

    [Header("Players")]
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Player currentPlayer;

    [Header("Board")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float xOffset, yOffset;
    [SerializeField] private float startX, startY;

    [Header("Audios")]
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource tieSound;

    private State currentState;

    #region MonoBehavior Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentPlayer = player1;
    }

    private void Start()
    {
        currentState = State.Playing;
        InitBoard();
    }

    #endregion

    #region My Methods

    private void InitBoard()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Instantiate(tilePrefab, new Vector3(startX + (x * xOffset), startY - (y * yOffset)), Quaternion.identity, transform);
            }
        }
    }

    public void RestartGame()
    {
        foreach (Transform tile in transform)
        {
            tile.GetComponent<SpriteRenderer>().sprite = null;
        }

        currentState = State.Playing;
        currentPlayer = player1;
        OnUpdate?.Invoke();
    }

    public void FinishPlay()
    {
        int nullSpriteCount = 0;
        foreach (Transform tile in transform)
        {
            if (tile.GetComponent<SpriteRenderer>().sprite == null)
            {
                nullSpriteCount++;
            }
        }
        if (nullSpriteCount == 0)
        {
            EndGame();
            return;
        }

        OnUpdate?.Invoke();
    }

    public void EndGame(Player winningPlayer = null)
    {
        currentState = State.EndGame;
        if (winningPlayer)
        {
            OnGameEnded?.Invoke($"{winningPlayer.Name} Player Wins!!!");
            winSound.Play();
        }
        else
        {
            OnGameEnded?.Invoke("It's a tie!");
            tieSound.Play();
        }
    }

    #endregion

    #region Getters and Setters

    public Player Player1
    {
        get { return player1; }
    }

    public Player Player2
    {
        get { return player2; }
    }

    public Player CurrentPlayer
    {
        get { return currentPlayer; }
        set { currentPlayer = value; }
    }

    public State CurrentState
    {
        get { return currentState; }
    }

    #endregion
}
