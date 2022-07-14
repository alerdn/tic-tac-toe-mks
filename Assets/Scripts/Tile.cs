using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rederer;

    private void Start()
    {
        rederer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.CurrentState != State.Playing) return;

        if (rederer.sprite != null) return;

        rederer.sprite = GameManager.instance.CurrentPlayer.Sprite;

        CheckLine(new Vector2[] { Vector2.right, Vector2.left });
        CheckLine(new Vector2[] { Vector2.up, Vector2.down });
        CheckLine(new Vector2[] { Vector2.up + Vector2.left, Vector2.down + Vector2.right });
        CheckLine(new Vector2[] { Vector2.up + Vector2.right, Vector2.down + Vector2.left });

        if (GameManager.instance.CurrentState == State.EndGame) return;

        if (GameManager.instance.CurrentPlayer == GameManager.instance.Player1)
            GameManager.instance.CurrentPlayer = GameManager.instance.Player2;
        else GameManager.instance.CurrentPlayer = GameManager.instance.Player1;

        GameManager.instance.FinishPlay();
    }

    private void CheckLine(Vector2[] directions)
    {
        int matchCount = 0;
        foreach (Vector2 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            while (hit.collider != null)
            {
                SpriteRenderer otherSprite = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                if (otherSprite != null && otherSprite.sprite == rederer.sprite) matchCount++;
                else break;

                hit = Physics2D.Raycast(hit.collider.transform.position, direction);
            }

            if (matchCount >= 2)
            {
                GameManager.instance.EndGame(GameManager.instance.CurrentPlayer);
            }
        }
    }
}
