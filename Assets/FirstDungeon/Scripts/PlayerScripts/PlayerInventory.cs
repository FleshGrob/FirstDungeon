using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keys = 0;
    public event Action<int> OnKeysChanged;

    void NotifyKeysChanged()
    {
        OnKeysChanged?.Invoke(keys);
    }

    public bool HasKey()
    {
        return keys > 0;
    }

    public void AddKey()
    {
        keys++;
        NotifyKeysChanged();
    }

    public bool UseKey()
    {
        if (keys <= 0)
            return false;

        keys--;
        NotifyKeysChanged();
        return true;
    }
}