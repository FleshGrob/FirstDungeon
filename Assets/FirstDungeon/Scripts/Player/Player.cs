using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        public Transform Transform { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerHealth Health { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerShooter Shooter { get; private set; }
        public PlayerState State { get; private set; }
        public PlayerVisual Visual { get; private set; }
        public FrogShapeshift FrogShape { get; private set; }


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            Transform = transform;
            Movement = GetComponent<PlayerMovement>();
            Health = GetComponent<PlayerHealth>();
            Inventory = GetComponent<PlayerInventory>();
            Shooter = GetComponent<PlayerShooter>();
            State = GetComponent<PlayerState>();
            Visual = GetComponent<PlayerVisual>();
            FrogShape = GetComponent<FrogShapeshift>();
        }
    }
}



        