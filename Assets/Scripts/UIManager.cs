using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour {
    public Canvas heartCanvas;
    public GameObject heartPrefab;
    public float heartCanvasOffset = -90;
    public int maxHearts = 3;
    public List<GameObject> hearts;
    public int CurrentHearts {
        get { return hearts.Count; }
    }
    public CanvasGroup gameLostCanvas;
    public Player player;

    public float fadeDuration = 1f;

    void Start() {
        if (heartCanvas == null) {
            Debug.LogError("Heart Canvas is not assigned in UIManager.");
            return;
        }

        if (heartPrefab == null) {
            Debug.LogError("Heart Prefab is not assigned in UIManager.");
            return;
        }

        if (player == null) {
            Debug.LogError("Player script is not assigned in UIManager.");
            return;
        }

        maxHearts = player.life;
        hearts = new(maxHearts);

        for (int i = 0; i < maxHearts; i++) {
            Vector3 heartPosition = new(0, (i+1) * heartCanvasOffset, 0);
            GameObject heart = Instantiate(heartPrefab, heartPosition, Quaternion.identity, heartCanvas.transform);

            heartPosition.y += heartCanvas.transform.position.y;
            heart.transform.localPosition = heartPosition;
            heart.name = "Heart" + (i + 1);
            hearts.Add(heart);
        }

        if (gameLostCanvas == null) {
            Debug.LogError("Game Lost Canvas is not assigned in UIManager.");
            return;
        }

        gameLostCanvas.alpha = 0f;
    }

    public void DeleteHeart() {
        if (hearts.Count > 0) {
            GameObject heartToDelete = hearts[^1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(heartToDelete);
        }

        if (hearts.Count == 0) {
            ShowGameLostUI();
        }
    }

    public void ShowGameLostUI() {
        if (gameLostCanvas != null) {
            Time.timeScale = 0f;

            gameLostCanvas.enabled = true;

            StartCoroutine(GameLostUICoroutine());
        } else {
            Debug.LogError("Game Lost Canvas is not assigned in UIManager.");
        }
    }

    public IEnumerator GameLostUICoroutine() {
        if (gameLostCanvas != null) {
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration) {
                elapsedTime += Time.unscaledDeltaTime;
                gameLostCanvas.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }
        } else {
            Debug.LogError("Game Lost Canvas is not assigned in UIManager.");
        }
    }
}