using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Player;
using TMPro;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {

        public static UIManager Instance { get; private set; }

        [Header("Components")]
        [SerializeField]
        private Image weaponImage;
        [SerializeField]
        private TMP_Text textBullets;

        [Header("Controllers")]
        public InventoryController inventoryController;

        [Header("Sprites")]
        [SerializeField]
        private Sprite pistolWeapon;
        [SerializeField]
        private Sprite rifleWeapon;

        private void Awake()
        {


            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            inventoryController.SetInventoryData(InventoryItem.ItemType.PistolBullets, 10);
            inventoryController.SetInventoryData(InventoryItem.ItemType.RifleBullets, 20);
        }
        public void UpdateBullets(int amount)
        {
            textBullets.text = amount.ToString();
        }
        public void UpdateWeapon(PlayerWeapon.WeaponType weaponType)
        {
            switch (weaponType)
            {
                case PlayerWeapon.WeaponType.None:
                    weaponImage.enabled = false;
                    break;
                case PlayerWeapon.WeaponType.Pistol:
                    weaponImage.enabled = true;
                    weaponImage.sprite = pistolWeapon;
                    break;
                case PlayerWeapon.WeaponType.Rifle:
                    weaponImage.enabled = true;
                    weaponImage.sprite = rifleWeapon;
                    break;
            }
        }
    }


}


