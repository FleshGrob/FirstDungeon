using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class ChaosTargetManager : MonoBehaviour
    {
        [SerializeField] GameObject _chestPrefab;
        [SerializeField] Transform _chestSpawnPos;
        [SerializeField] Color _activeColor;
        
        ChaosTarget[] _targets;

        public bool IsSolved { get; private set; }
        public Color ActiveColor => _activeColor; 


        void Awake()
        {
            _targets = GetComponentsInChildren<ChaosTarget>();
        }

        public void OnTargetHit()
        {
            if (IsSolved)
                return;
            
            foreach (ChaosTarget t in _targets)
            {
                if (!t.IsActivated) return;
            }

            Solve();
        }

        void Solve()
        {
            IsSolved = true; 
            Instantiate(_chestPrefab, _chestSpawnPos.position, Quaternion.identity);
        }
    }
}
