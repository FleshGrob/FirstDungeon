using UnityEngine;

namespace FirstDungeon.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        public Transform PlayerTransform { get; private set; }


        void Awake()
        {
            Instance = this;

            PlayerTransform = transform;
        }
    }
}



        