using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TicTacToe/Player")]
public class Player : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Sprite _sprite;
    [SerializeField] Color _primaryColor;

    public Player(string name, Sprite sprite, Color primaryColor)
    {
        _name = name;
        _sprite = sprite;
        _primaryColor = primaryColor;
    }

    public string Name { get { return _name; } }

    public Sprite Sprite { get { return _sprite; } }

    public Color PrimaryColor { get { return _primaryColor; } }
}
