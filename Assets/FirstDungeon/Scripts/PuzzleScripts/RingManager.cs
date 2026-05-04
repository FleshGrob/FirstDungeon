using FirstDungeon.Scripts.ObjectsScripts;
using FirstDungeon.Scripts.OtherScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class RingManager : MonoBehaviour
    {
        [SerializeField] Color _activeColor;
        [SerializeField] GameObject _chestPrefab;
        [SerializeField] Transform _chestSpawnPos;
        
        Projectile _projectile;
        Ring[] _rings;

        public Color ActiveColor => _activeColor;
        public int RightID { get; private set; }
        public bool IsSolved { get; private set; }


        void Awake()
        {
            _rings = GetComponentsInChildren<Ring>();
        }

        public void GetFrog(int frogID, Projectile frog)
        {
            _projectile = frog;
            RightID = frogID;
            _projectile.OnDisposed += Fail;
        }

        public void CheckHit()
        {
            foreach(Ring r in _rings)
            {
                if (!r.IsActivated)
                    return;
            }
            Solve();
        }

        public void Restart()
        {
            RightID = 0;
            foreach (Ring r in _rings)
            {
                r.Cancel();
            }
        }

        void Fail()
        {
            _projectile.OnDisposed -= Fail;
            if (!IsSolved)
            {
                Restart();
            }
        }
        
        void Solve()
        {
            IsSolved = true;
            Instantiate(_chestPrefab, _chestSpawnPos.position, Quaternion.identity);
        }
    }
}
