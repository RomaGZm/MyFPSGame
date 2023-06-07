using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullController : MonoBehaviour
{
    private List<GameObject> bulletPullList = new List<GameObject>();
    [SerializeField]
    private GameObject bulletPullPref;
    public int bulletPullSize = 10;

    public void Start()
    {
        for (int i = 0; i < bulletPullSize; i++)
            bulletPullList.Add(Instantiate(bulletPullPref, transform));
    }

    public void ShowDamagePopup(Transform target, string text)
    {
        //GetPullObject().GetComponent<DamagePopup>().SetEnable(true, target, text);
    }

    public GameObject GetPullBullet()
    {
        foreach (GameObject bullet in bulletPullList)
            if (!bullet.gameObject.activeSelf)
                return bullet;

        GameObject newBullet = Instantiate(bulletPullPref, transform); 
        bulletPullList.Add(newBullet);
        return newBullet;

    }
}
