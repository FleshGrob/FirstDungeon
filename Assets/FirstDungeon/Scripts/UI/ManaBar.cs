using FirstDungeon.Scripts.PlayerScripts;
using UnityEngine;
using TMPro;

public class ManaBar : MonoBehaviour
{
    TextMeshProUGUI _manaText;


    void Awake()
    {
        _manaText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Player.Instance.Mana.OnManaChanged += Refresh;
        Refresh();
    }
    
    void OnDestroy()
    {
        if (Player.Instance == null) return;
        Player.Instance.Mana.OnManaChanged -= Refresh;
    }

    void Refresh()
    {
        _manaText.text = $"Mana: {Player.Instance.Mana.CurrentMana} / {Player.Instance.Mana.MaxMana}";
    }
}
