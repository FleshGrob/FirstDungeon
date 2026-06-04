using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class PlayerSafeZone : MonoBehaviour
    {
        int _hazardCount;
        
        
        void OnTriggerEnter2D(Collider2D other)
        {
            _hazardCount++;
            Player.Instance.State.SetSafe(false);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            _hazardCount--;
            if (_hazardCount == 0) 
                Player.Instance.State.SetSafe(true);
        }
    }
}
