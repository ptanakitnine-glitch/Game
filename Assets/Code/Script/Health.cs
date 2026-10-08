using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 20;
    [SerializeField] private int currencyWorth = 25;
    [SerializeField] private enemiesSound es;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            if (es != null) { es.PlaySound(); }
            Destroy(gameObject);
        }
    }

}
