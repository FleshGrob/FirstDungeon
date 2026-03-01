using System.Collections;
using UnityEngine;
using TMPro;

public class FrogStaffPopupUI : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] PlayerShooter shooter;
    [SerializeField] GameObject popupRoot;
    [SerializeField] TMP_Text popupText;

    [Header("Settings")]
    [SerializeField] float showSeconds = 1.5f;

    Coroutine hideRoutine;

    void Awake()
    {
        if (popupRoot != null)
            popupRoot.SetActive(false);
    }

    void OnEnable()
    {
        if (shooter == null) return;
        shooter.OnCanShootChanged += HandleCanShootChanged;
    }

    void OnDisable()
    {
        if (shooter == null) return;
        shooter.OnCanShootChanged -= HandleCanShootChanged;
    }

    void HandleCanShootChanged(bool canShoot)
    {
        if (canShoot)
            Show();
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
