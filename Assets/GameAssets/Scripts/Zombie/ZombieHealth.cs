using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieHealth : MonoBehaviour {
  
	public int health 
	{
		get { return _health; }
		set { 
			_health = value;
			if (_health < 0)
				_health = 0;
			health_Text.text = maxHealth + "/" + _health;
		}
	}
    public int maxHealth = 100;

	private int _health = 100;

	[SerializeField]
	private Transform headInfoPanel;
	[SerializeField]
	private TMP_Text health_Text;

	public void DisableHealtInfo()
    {
		headInfoPanel.gameObject.SetActive(false);

	}
	
	void Update () {
		headInfoPanel.transform.LookAt(headInfoPanel.transform.position + Camera.main.transform.rotation * Vector3.forward,
		Camera.main.transform.rotation * Vector3.up);
	}
}
