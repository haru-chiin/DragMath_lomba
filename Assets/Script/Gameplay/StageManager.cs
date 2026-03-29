using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class MathQuestion
{
    public int targetValue;
    public int[] startingNumbers = new int[6];
}

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("Data Soal")]
    public MathQuestion[] questions;
    public int currentQuestionIndex = 0;

    [Header("Status Unit")]
    public int enemyHP = 3;
    public int playerHP = 3;
    public EnemyUnit enemy;
    public PlayerUnit player;

    [Header("UI Nyawa")]
    public HealthUI playerHealthUI;
    public HealthUI enemyHealthUI;

    [Header("Referensi")]
    public NumberSpawner spawner;

    [Header("Victory Sequence")]
    public UniversalAdditionalCameraData cameraData;
    public Image whiteFlash;
    public GameObject victoryCardPanel;
    public GameObject losePanel;
    public RectTransform cardTransform;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        BGMManager.Instance.PlayBattle();

        if (cameraData != null) cameraData.SetRenderer(1);
        LoadQuestion();
    }

    public void EvaluateAttack(int attackValue)
    {
        if (currentQuestionIndex >= questions.Length) return;

        bool isCorrect = (attackValue == questions[currentQuestionIndex].targetValue);

        if (isCorrect)
        {
            Debug.Log("Serangan Berhasil!");
            if (player != null) player.PlayAttack();
            StartCoroutine(CorrectAttackSequence());
        }
        else
        {
            Debug.Log("Serangan Gagal! Angka tidak sesuai target.");
            if (player != null) player.PlayHit();
            StartCoroutine(WrongAttackSequence());
        }
    }

    IEnumerator CorrectAttackSequence()
    {
        yield return new WaitForSeconds(1.5f);

        enemyHP--;
        enemyHealthUI.UpdateHealth(enemyHP);
        Debug.Log("HP Musuh sisa: " + enemyHP);

        if (enemyHP <= 0)
        {
            Debug.Log("Pemain Menang! Musuh Kalah.");
            enemy.PlayHitEffect();

            yield return new WaitForSeconds(0.5f);
            enemy.gameObject.SetActive(false);

            StartCoroutine(ShowVictoryCardSequence());
        }
        else
        {
            enemy.PlayHitEffect();

            currentQuestionIndex++;
            if (currentQuestionIndex >= questions.Length)
            {
                Debug.Log("Kehabisan soal tapi musuh belum mati.");
            }
            else
            {
                yield return new WaitForSeconds(1f);
                LoadQuestion();
            }
        }
    }

    IEnumerator WrongAttackSequence()
    {
        yield return new WaitForSeconds(1.5f);

        playerHP--;
        playerHealthUI.UpdateHealth(playerHP);
        Debug.Log("HP Player sisa: " + playerHP);

        if (playerHP <= 0)
        {
            Debug.Log("Pemain Kalah! Nyawa Raka habis.");
            StartCoroutine(LosePanel());

        }
        else
        {
            currentQuestionIndex++;
            if (currentQuestionIndex >= questions.Length && enemyHP > 0)
            {
                Debug.Log("Kehabisan soal tapi musuh belum mati.");
            }
            else
            {
                LoadQuestion();
            }
        }
    }

    void LoadQuestion()
    {
        MathQuestion q = questions[currentQuestionIndex];
        enemy.SetTarget(q.targetValue);
        spawner.SpawnQuestion(q.startingNumbers);

        if (OperatorManager.Instance != null)
            OperatorManager.Instance.ResetOperators();
    }

    IEnumerator ShowVictoryCardSequence()
    {
        if (cameraData != null)
        {
            cameraData.SetRenderer(0);
        }

        if (whiteFlash != null)
        {
            whiteFlash.gameObject.SetActive(true);
            whiteFlash.color = new Color(1, 1, 1, 1);
        }

        if (victoryCardPanel != null) victoryCardPanel.SetActive(true);
        if (cardTransform != null) cardTransform.localScale = Vector3.zero;

        float fadeDuration = 0.5f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            BGMManager.Instance.PlayWin();
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            if (whiteFlash != null) whiteFlash.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        if (whiteFlash != null) whiteFlash.gameObject.SetActive(false);
        if (cardTransform != null)
        {
            float popDuration = 0.4f;
            elapsed = 0f;
            while (elapsed < popDuration)
            {
                elapsed += Time.deltaTime;
                float scale = Mathf.Lerp(0f, 1.1f, elapsed / popDuration);
                cardTransform.localScale = new Vector3(scale, scale, 1f);
                yield return null;
            }

            cardTransform.localScale = Vector3.one;
        }
    }

    IEnumerator LosePanel() 
    {
        if (losePanel != null) losePanel.SetActive(true);
        BGMManager.Instance.PlayLose();
        yield return null;
    }
}