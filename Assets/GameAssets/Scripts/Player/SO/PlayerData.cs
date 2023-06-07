using UnityEngine;
using Core.Player;

namespace Core.Player{


    [System.Serializable]
    public class MoveSettings
    {
        public float walkSpeed = 7.5f;
        public float runSpeed = 11.5f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;
    }
    [System.Serializable]
    public class InpuSettings
    {
        public KeyCode KeyMoveForward = KeyCode.W;
        public KeyCode KeyMoveBackward = KeyCode.S;
        public KeyCode KeySidewaysLeft = KeyCode.A;
        public KeyCode KeySidewaysRight = KeyCode.D;
        public KeyCode KeyJump = KeyCode.Space;
        public KeyCode KeyRun = KeyCode.LeftShift;
        public KeyCode KeyReload = KeyCode.R;
        public KeyCode KeyWeaponNone = KeyCode.Alpha1;
        public KeyCode KeyWeaponPistol = KeyCode.Alpha2;
        public KeyCode KeyWeaponRifle = KeyCode.Alpha3;
        public KeyCode KeyInventory = KeyCode.Tab;

    }
    [CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public InpuSettings inpuSettings;
        public MoveSettings moveSettings;

    }
}


