using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeysHUD : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;
    [SerializeField] TMP_Text keysText;

    void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnKeysChanged += UpdateKeysText;
            UpdateKeysText(inventory.keys); 
        }
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnKeysChanged -= UpdateKeysText;
    }

    void UpdateKeysText(int keyCount)
    {
        if (keysText != null)
            keysText.text = "Keys: " + keyCount;
    }
}
