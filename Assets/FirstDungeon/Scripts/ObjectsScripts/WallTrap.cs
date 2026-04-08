using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace FirstDungeon.Scripts.ObjectsScripts
{
    public class WallTrap : MonoBehaviour
    {
        [SerializeField] float _spearSpeed;
        [SerializeField] Spear _spearPrefab;
        [SerializeField] float _offset;
        
        public void ShootSpear()
        {
            Vector2 spawnPosition = transform.position - transform.up * _offset;
            Spear spear = Instantiate(_spearPrefab, spawnPosition, transform.rotation);
            spear.Launch(_spearSpeed, -transform.up);
        }
    }
}
