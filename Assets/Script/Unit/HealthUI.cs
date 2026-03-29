using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Tooltip("Masukkan 3 GameObject hati (Image) dari kiri ke kanan")]
    public GameObject[] hearts;
    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}