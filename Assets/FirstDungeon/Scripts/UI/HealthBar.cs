using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    TextMeshProUGUI _healthText;


    void Awake()
    {
        _healthText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Player.Instance.Health.OnHealthChanged += Refresh;
        Refresh();
    }
    
    void OnDestroy()
    {
        if (Player.Instance == null) return;
        Player.Instance.Health.OnHealthChanged -= Refresh;
    }

    void Refresh()
    {
        _healthText.text = $"Health: {Player.Instance.Health.CurrentHealth} / {Player.Instance.Health.MaxHealth}";
    }
}
