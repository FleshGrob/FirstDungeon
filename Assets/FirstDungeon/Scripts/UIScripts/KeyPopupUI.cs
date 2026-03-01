using System.Collections;
using UnityEngine;
using TMPro;

public class KeyPopupUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] PlayerInventory inventory;
    [SerializeField] GameObject popupRoot;
    [SerializeField] TMP_Text popupText;

    [Header("Settings")]
    [SerializeField] float showSeconds = 1.2f;

    int lastKeys;
    Coroutine hideRoutine;

    void Awake()
    {
        if (popupRoot != null)
            popupRoot.SetActive(false);
    }

    void OnEnable()
    {
        if (inventory == null) return;

        lastKeys = inventory.keys;
        inventory.OnKeysChanged += HandleKeysChanged;
    }

    void OnDisable()
    {
        if (inventory == null) return;

        inventory.OnKeysChanged -= HandleKeysChanged;
    }

    void HandleKeysChanged(int newKeys)
    {
        if (newKeys > lastKeys)
            Show();

        lastKeys = newKeys;
    }

    void Show()
    {
        if (popupRoot == null) return;

        if (popupText != null)

        popupRoot.SetActive(true);

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideAfterSeconds());
    }

    IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSecondsRealtime(showSeconds);

        if (popupRoot != null)
            popupRoot.SetActive(false);

        hideRoutine = null;
    }
}