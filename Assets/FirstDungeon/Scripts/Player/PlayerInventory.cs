using System;
using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerInventory : MonoBehaviour
    {
        int _keys;
        
        public event Action<int> OnKeysChanged;

        
        void NotifyKeysChanged()
        {
            OnKeysChanged?.Invoke(_keys);
        }

        public void AddKey()
        {
            _keys++;
            NotifyKeysChanged();
        }

        public bool UseKey()
        {
            if (_keys <= 0)
                return false;

            _keys--;
            NotifyKeysChanged();
            return true;
        }
    }
}