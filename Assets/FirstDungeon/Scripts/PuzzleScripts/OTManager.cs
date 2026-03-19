using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class OTManager : MonoBehaviour
    {
        [SerializeField] Color _activeColor;
        [SerializeField] GameObject _chestPrefab;
        [SerializeField] Transform _chestSpawnPos;

        OrderTarget[] _orderTargets;
        int _currentIndex;
        
        public Color ActiveColor  => _activeColor;
        public bool IsSolved { get; private set; }


        void Awake()
        {
            _orderTargets =  GetComponentsInChildren<OrderTarget>();
        }

        public void OnTargetHit(OrderTarget t)
        {
            if (IsSolved)
                return;
            if (t == _orderTargets[_currentIndex])
            {
                t.SetActive();
                _currentIndex += 1;
            }
            else
            {
                ResetAll();
                _currentIndex = 0;
            }
            if (_currentIndex >= _orderTargets.Length)
                Solve();

        }
        
        void ResetAll()
        {
            foreach (OrderTarget t in _orderTargets)
                t.SetIdle();
        }

        void Solve()
        {
            IsSolved = true;
            Instantiate(_chestPrefab, _chestSpawnPos.position, Quaternion.identity);
        }
    }
}
