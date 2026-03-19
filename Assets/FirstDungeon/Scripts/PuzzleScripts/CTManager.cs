using FirstDungeon.Scripts.ObjectsScripts;
using UnityEngine;

namespace FirstDungeon.Scripts.PuzzleScripts
{
    public class CTManager : MonoBehaviour
    {
        [SerializeField] Color[] _cycleColors;
        [SerializeField] GameObject _chestPrefab;
        [SerializeField] Transform _chestSpawnPos;
        [SerializeField] int _rightColorIndex;
        
        ColorTarget[] _targets;
        
        public bool IsSolved { get; private set; }
        public int RightColorIndex =>_rightColorIndex; 
        public Color[] CycleColors => _cycleColors; 

        void Awake()
        {
            _targets = GetComponentsInChildren<ColorTarget>();
        }

        public void OnTargetHit()
        {
            if (IsSolved)
                return;
            if (AllRightColor(_targets))
                Solve();
        }

        bool AllRightColor(ColorTarget[] targets)
        {
            foreach (ColorTarget t in targets)
            {
                if (!t.IsRightColor)
                    return false;
            }
            return true;
        }

        void Solve()
        {
            IsSolved = true;
            Instantiate(_chestPrefab, _chestSpawnPos.position, Quaternion.identity);
        }
    }
}
