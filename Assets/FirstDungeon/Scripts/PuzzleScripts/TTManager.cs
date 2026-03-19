using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class TTManager : MonoBehaviour
    {
        [SerializeField] float _fullTimer;
        [SerializeField] Color _activeColor;
        [SerializeField] GameObject _chestPrefab;
        [SerializeField] Transform _chestSpawnPos;
        
        TimerTarget[] _targets;

        public Color ActiveColor => _activeColor;
        public float FullTimer => _fullTimer; 
        public bool IsSolved { get; private set; }
        

        void Awake()
        {
            _targets = GetComponentsInChildren<TimerTarget>();
        }

        public void OnTargetHit()
        {
            if (IsSolved)
                return;

            foreach (TimerTarget t in _targets)
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