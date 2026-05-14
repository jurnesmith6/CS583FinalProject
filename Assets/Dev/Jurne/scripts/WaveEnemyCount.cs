
using TMPro;
using UnityEngine;

public class WaveEnemyCount : MonoBehaviour
{
    
    [Header("Gameplay References")]
    public Spawner spawner;
    

    [Header("Text")]
    public TMP_Text enemyCountText;
    public TMP_Text waveCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void UpdateText()
    {
        enemyCountText.text =
            "Enemies: " + spawner.enemiesRemaining;

        waveCountText.text =
            "Wave: " + spawner.wave;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }
}
