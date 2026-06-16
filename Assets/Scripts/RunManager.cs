using TMPro;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultText;

    private PlayerStats stats;
    private bool runOver;

    private void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
        stats.OnClankChanged += CheckClankLose;
        stats.OnHealthChanged += CheckHealthLose;
    }

    private void CheckClankLose(float clank)
    {
        if (clank >= stats.MaxClank)
            EndRun(false);
    }

    private void CheckHealthLose(int health)
    {
        if (health <= 0)
            EndRun(false);
    }

    public void EndRun(bool escaped)
    {
        if (runOver) return;
        runOver = true;
        resultText.text = escaped ? "You Retrieved The Artifact!" : "You Got Caught!";
        resultPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        if (stats != null)
        {
            stats.OnClankChanged -= CheckClankLose;
            stats.OnHealthChanged -= CheckHealthLose;
        }
        Time.timeScale = 1f;
    }
}
