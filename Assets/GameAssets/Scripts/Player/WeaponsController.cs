using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;

namespace Core.Player
{
    public class WeaponsController : MonoBehaviour
    {
        public List<PlayerWeapon> weapons;
        public PlayerWeapon currentWeapon;

        public void SetWeapon(PlayerWeapon.WeaponType weaponType)
        {
            switch (weaponType)
            {
                case PlayerWeapon.WeaponType.None:
                    DisableWeapons();
                    currentWeapon = null; 
                    break;
                case PlayerWeapon.WeaponType.Pistol:
                    DisableWeapons();
                    currentWeapon = FindWeaponWType(PlayerWeapon.WeaponType.Pistol);
                    currentWeapon.gameObject.SetActive(true);
                    break;
                case PlayerWeapon.WeaponType.Rifle:
                    DisableWeapons();
                    currentWeapon = FindWeaponWType(PlayerWeapon.WeaponType.Rifle);
                    currentWeapon.gameObject.SetActive(true);
                    break;
            }
            UIManager.Instance.UpdateWeapon(weaponType);
        }
     private void DisableWeapons()
     {
            foreach(PlayerWeapon weapon in weapons){
                weapon.gameObject.SetActive(false);
            }
     }

     public PlayerWeapon FindWeaponWType(PlayerWeapon.WeaponType weaponType)
        {

            foreach (PlayerWeapon weapon in weapons)
            {
                if (weapon.weaponType == weaponType)
                    return weapon;

            }
            return null;
        }
    }


}
