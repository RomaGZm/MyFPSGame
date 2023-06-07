using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.UI
{
    public class InventoryController : MonoBehaviour
    {


        private InventoryCell selectedCell;

        [SerializeField]
        private GameObject cellsContent;
        [SerializeField]
        private List<InventoryCell> cells = new List<InventoryCell>();

        [Header("Prefs")]
        [SerializeField]
        private List<GameObject> itemPrefs;
        [Header("Datas")]
        [SerializeField]
        private InventoryData inventoryData;

        private void Start()
        {
            foreach(InventoryCell cell in cells)
                cell.selectedCellEvent += OnSelectItemEvent;
        }

        public void SetInventoryData(InventoryItem.ItemType itemType, int data)
        {
            if (itemType == InventoryItem.ItemType.PistolBullets)
                inventoryData.bulletsPistolCounter = data;
            if (itemType == InventoryItem.ItemType.RifleBullets)
                inventoryData.bulletsRifleCounter = data;
        }
        public int GetInventoryData(InventoryItem.ItemType itemType)
        {
            if (itemType == InventoryItem.ItemType.PistolBullets)
                return inventoryData.bulletsPistolCounter;
            if (itemType == InventoryItem.ItemType.RifleBullets)
                return inventoryData.bulletsRifleCounter;

            return 0;
        }
        public void Show()
        {
            ClearItems();
            UpdateRifleCells();
            UpdatePistolCells();
            transform.parent.gameObject.SetActive(true);
            print(transform.parent.name);
        }
        public void Hide()
        {
            transform.parent.gameObject.SetActive(false);

        }
        private void UpdateRifleCells()
        {
            if (inventoryData.bulletsRifleCounter == 0) return;

            int value = inventoryData.bulletsRifleCounter / 10;
            int currentCunter = inventoryData.bulletsRifleCounter;


            if (inventoryData.bulletsRifleCounter > 10)
            {

                for (int j = 0; j < value; j++)
                {
                    InventoryCell freeCell = GetFreeCell();
                    InventoryItem item = freeCell.SetItemPref(GetItemWType(InventoryItem.ItemType.RifleBullets));

                    item.GetComponent<InventoryItem>().SetDrop(10);
                    currentCunter -= 10;

                }
                if (currentCunter != 0)
                {
                    InventoryCell freeCell = GetFreeCell();
                    InventoryItem item = freeCell.SetItemPref(GetItemWType(InventoryItem.ItemType.RifleBullets));
                    item.GetComponent<InventoryItem>().SetDrop(currentCunter);
                }

            }
            else
            {
                InventoryCell freeCell = GetFreeCell();
                InventoryItem item = freeCell.SetItemPref(GetItemWType(InventoryItem.ItemType.RifleBullets));
                item.GetComponent<InventoryItem>().SetDrop(currentCunter);

            }

        }
        private void UpdatePistolCells()
        {
            if (inventoryData.bulletsPistolCounter == 0) return;

            int value = inventoryData.bulletsPistolCounter / 10;
            int currentCunter = inventoryData.bulletsPistolCounter;


            if (inventoryData.bulletsPistolCounter > 10)
            {

                for (int j = 0; j < value; j++)
                {
                    InventoryCell freeCell = GetFreeCell();
                    InventoryItem item = freeCell.SetItemPref(GetItemWType(InventoryItem.ItemType.PistolBullets));
                    item.GetComponent<InventoryItem>().SetDrop(10);
                    currentCunter -= 10;

                }
                if (currentCunter != 0)
                {
                    InventoryCell freeCell = GetFreeCell();
                    InventoryItem item = freeCell.SetItemPref(GetItemWType(InventoryItem.ItemType.PistolBullets));
                    item.GetComponent<InventoryItem>().SetDrop(currentCunter);
                }

            }
            else
            {
                InventoryCell freeCell = GetFreeCell();
                InventoryItem item = freeCell.SetItemPref(GetItemWType(InventoryItem.ItemType.PistolBullets));
                item.GetComponent<InventoryItem>().SetDrop(currentCunter);

            }

        }

        private GameObject GetItemWType(InventoryItem.ItemType itemType)
        {
            foreach (GameObject item in itemPrefs)
            {
                if (item.GetComponent<InventoryItem>().itemType == itemType)
                    return item;
            }
            return null;
        }
        private InventoryCell GetFreeCell()
        {
            foreach (InventoryCell cell in cells)
            {
                if (cell.currentItem == null)
                    return cell;
            }
            return null;
        }
        private void OnSelectItemEvent(InventoryCell cell)
        {

            if (cell.currentItem != null)
            {
                ResetSelectedCells();
                cell.isSelected = true;
                selectedCell = cell;
       
            }

        }
        private void ResetSelectedCells()
        {
            foreach (InventoryCell cell in cells)
                cell.isSelected = false;
        }
        private void ClearItems()
        {
            foreach (InventoryCell cell in cells)
            {
                cell.isSelected = false;
                if (cell.currentItem)
                {
                    Destroy(cell.currentItem.gameObject);
                    cell.currentItem = null;
                }

            }
               
        }
        private void OnDestroy()
        {
            foreach (InventoryCell cell in cells)
            {
                cell.selectedCellEvent -= OnSelectItemEvent;
            }
        }
    }

}
