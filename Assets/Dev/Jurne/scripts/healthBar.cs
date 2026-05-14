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
    
    float maxHealth;

    void Update()
    {
        UpdateHealthBars();
    }

    void UpdateHealthBars()
    {
        
        Debug.Log("current HP: " + player.hp + "Max HP: " + player.maxHp);
        
        playerHealthBar.fillAmount =
            (float)player.hp / player.maxHp;
        
        
        Debug.Log("current HP crystal: " + crystal.hitpoints + "Max crystal HP: " + maxHealth);
        
        crystalHealthBar.fillAmount =
            (float)crystal.hitpoints / maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = 50f;
    }
    
}
