using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Zombie;
using Core.UI;
namespace Core.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private ParticleSystem particleShoot;
        [SerializeField]
        
        private Transform shootPoint;
        [Header("Shoot")]
        public float bulletForce = 1;
        public int damage = 10;
        public float attackRate = 1;
        public int amountBullet = 10;

        private void Start()
        {
            if(weaponType == WeaponType.Pistol)
            {
                amountBullet = UIManager.Instance.inventoryController.GetInventoryData(InventoryItem.ItemType.PistolBullets);
                
            }
               
            if (weaponType == WeaponType.Rifle)
            {
                amountBullet = UIManager.Instance.inventoryController.GetInventoryData(InventoryItem.ItemType.RifleBullets);
            }
              
        }

        private void UpdateBulletsData()
        {
            if (weaponType == WeaponType.Pistol)
                UIManager.Instance.inventoryController.SetInventoryData(InventoryItem.ItemType.PistolBullets, amountBullet);
            if (weaponType == WeaponType.Rifle)
                UIManager.Instance.inventoryController.SetInventoryData(InventoryItem.ItemType.RifleBullets, amountBullet);
        }

        public enum WeaponType
        {
            None, Pistol, Rifle
        }

        public WeaponType weaponType;


        public void Shoot()
        {

            GameObject bulletGo = GameManager.Instance.pullController.GetPullBullet();
            Rigidbody bulletRb = bulletGo.GetComponent<Rigidbody>();
            bulletGo.transform.position = shootPoint.position;
            bulletGo.SetActive(true);
            bulletRb.AddForce(bulletForce * GameManager.Instance.playerController.crosshair.transform.forward, ForceMode.VelocityChange);

            Ray ray = new Ray(shootPoint.position, GameManager.Instance.playerController.crosshair.transform.forward);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData))
            {
                if(hitData.transform.tag == "Zombie")
                {
                    hitData.transform.GetComponent<ZombieController>().TakeDamage(damage);
                }
            }
            amountBullet--;
            UpdateBulletsData();
            particleShoot.Play();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red; 
            Gizmos.DrawRay(transform.position, transform.forward * 10000);
        }
    }
}

