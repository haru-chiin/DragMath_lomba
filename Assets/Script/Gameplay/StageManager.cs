using UnityEngine;

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

    [Header("Status Musuh")]
    public int enemyHP = 3;
    public EnemyUnit enemy;

    [Header("Referensi")]
    public NumberSpawner spawner;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadQuestion();
    }

    public void EvaluateAttack(int attackValue)
    {
        if (currentQuestionIndex >= questions.Length) return;

        bool isCorrect = (attackValue == questions[currentQuestionIndex].targetValue);

        if (isCorrect)
        {
            enemyHP--;
            Debug.Log("Serangan Berhasil! HP Musuh sisa: " + enemyHP);

            if (enemyHP <= 0)
            {
                Debug.Log("Pemain Menang! Musuh Kalah.");
                enemy.gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            Debug.Log("Serangan Gagal! Angka tidak sesuai target.");
        }

        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length && enemyHP > 0)
        {
            Debug.Log("Pemain Kalah! Kehabisan soal tapi musuh belum mati.");
        }
        else
        {
            LoadQuestion();
        }
    }

    void LoadQuestion()
    {
        MathQuestion q = questions[currentQuestionIndex];

        enemy.SetTarget(q.targetValue);

        spawner.SpawnQuestion(q.startingNumbers);

        OperatorManager.Instance.ResetOperators();
    }
}