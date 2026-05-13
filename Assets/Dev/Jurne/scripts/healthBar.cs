using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    
    [Header("Gameplay References")]
    public PlayerController player; 
    public Crystal crystal;

    [Header("Health Bar Images")]
    public Image playerHealthBar;
    public Image crystalHealthBar;
    
    float maxHealth = 100f;

    void Update()
    {
        UpdateHealthBars();
    }

    void UpdateHealthBars()
    {
        
        playerHealthBar.fillAmount =
            (float)player.hp / player.maxHp;
        
        Debug.Log("current HP: " + player.hp + "Max HP: " + player.maxHp);
        
        crystalHealthBar.fillAmount =
            (float)crystal.hitpoints / maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
}
