using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using UnityEngine.EventSystems;

namespace Core.Player
{
    [RequireComponent(typeof(AnimationsController), typeof(PlayerMovement), typeof(WeaponsController))]
    public class PlayerController : MonoBehaviour
    {
        public Vector3 lookAtWeight = Vector3.one;
        public Crosshair crosshair;

        private AnimationsController playerAnimation;
        private PlayerMovement playerMovement;
        private WeaponsController weaponsController;
        private CharacterController chController;
        private bool shootComplete = true;

        private void Start()
        {
            playerAnimation = GetComponent<AnimationsController>();
            playerMovement = GetComponent<PlayerMovement>();
            weaponsController = GetComponent<WeaponsController>();
            chController = GetComponent<CharacterController>();

            playerMovement.playerMove += OnMovement;
            playerMovement.playerJump += OnJump;

           
        }
        /// <summary>
        /// Pick up ammo
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "PickUp")
            {
                PickUp pickUp = other.gameObject.GetComponent<PickUp>();
                PlayerWeapon weapon = weaponsController.FindWeaponWType(pickUp.weaponType);
                weapon.amountBullet += pickUp.amount;
                if(weapon.weaponType == PlayerWeapon.WeaponType.Pistol)
                {
                    UIManager.Instance.inventoryController.SetInventoryData(InventoryItem.ItemType.PistolBullets, weapon.amountBullet);
                }
                if (weapon.weaponType == PlayerWeapon.WeaponType.Rifle)
                {
                    UIManager.Instance.inventoryController.SetInventoryData(InventoryItem.ItemType.RifleBullets, weapon.amountBullet);
                }
                if (weaponsController.currentWeapon)
                    UIManager.Instance.UpdateBullets(weaponsController.currentWeapon.amountBullet);
                Destroy(pickUp.gameObject);
            }
        }

        private void OnMovement(Vector3 dir)
        {
            playerAnimation.Move(dir.x);
            playerAnimation.Strafe(dir.y);
            playerAnimation.Run(playerMovement.isRunning);
        }

        private void OnJump()
        {
            playerAnimation.Jump();
        }
     
        private void Update()
        {
            if (Input.GetKeyDown(GameManager.Instance.playerData.inpuSettings.KeyInventory))
            {
                if (UIManager.Instance.inventoryController.transform.parent.gameObject.activeSelf)
                {
                    UIManager.Instance.inventoryController.Hide();
                    Time.timeScale = 1;
                }
                else
                {
                    UIManager.Instance.inventoryController.Show();
                    Time.timeScale = 0;
                }
                    
            }
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (weaponsController.currentWeapon && shootComplete && Input.GetMouseButton(0) && chController.isGrounded)
            {
                if(weaponsController.currentWeapon.amountBullet > 0)
                {
                    shootComplete = false;
                    StartCoroutine(Shoot());
                }

            }
            if (Input.GetKeyUp(GameManager.Instance.playerData.inpuSettings.KeyWeaponNone))
            {
                weaponsController.SetWeapon(PlayerWeapon.WeaponType.None);
               
                playerAnimation.SetWeapon(0);
            }
            if (Input.GetKeyUp(GameManager.Instance.playerData.inpuSettings.KeyWeaponPistol))
            {
                weaponsController.SetWeapon(PlayerWeapon.WeaponType.Pistol);
                UIManager.Instance.UpdateBullets(weaponsController.currentWeapon.amountBullet);
                playerAnimation.SetWeapon(1);
            }
            if (Input.GetKeyUp(GameManager.Instance.playerData.inpuSettings.KeyWeaponRifle))
            {
                weaponsController.SetWeapon(PlayerWeapon.WeaponType.Rifle);
                UIManager.Instance.UpdateBullets(weaponsController.currentWeapon.amountBullet);
                playerAnimation.SetWeapon(2);
            }
          
        }

        IEnumerator Shoot()
        {
            playerAnimation.Shoot();
            weaponsController.currentWeapon.Shoot();
            UIManager.Instance.UpdateBullets(weaponsController.currentWeapon.amountBullet);
            yield return new WaitForSeconds(weaponsController.currentWeapon.attackRate);
   
            shootComplete = true;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            // if (targetEnemy && playerAnimations.IsAim() && Vector3.Distance(transform.position, targetEnemy.transform.position) <= 3)
            //  {
         //   playerAnimation.animator.SetLookAtWeight(lookAtWeight.x, lookAtWeight.y, lookAtWeight.z);

           // playerAnimation.animator.SetLookAtPosition(aimPoint.position);
            //     
            //  }

        }
    }
}

