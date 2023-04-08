using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridUI : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private Image tileSlotPrefab;
    [SerializeField] private Button restartBtn;

    private void Start()
    {
        GameManager.instance.OnUpdate += UpdateUI;
        GameManager.instance.OnGameEnded += EndGame;

        restartBtn.onClick.AddListener(RestartGame);

        InitUI();
        UpdateUI();
    }

    private void InitUI()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(tileSlotPrefab, transform);
        }
    }

    private void UpdateUI()
    {
        Player currentPlayer = GameManager.instance.CurrentPlayer;
        logText.text = $"{currentPlayer.Name} Player Turn";
        logText.color = currentPlayer.PrimaryColor;
        logText.fontSize = 135f;
    }

    private void RestartGame()
    {
        restartBtn.gameObject.SetActive(false);
        GameManager.instance.RestartGame();
    }

    private void EndGame(string text)
    {
        restartBtn.gameObject.SetActive(true);
        logText.text = text;
        logText.color = new Color32(135, 114, 218, 255);
        logText.fontSize = 150f;
    }
}