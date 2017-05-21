using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotHandlerScript : MonoBehaviour {
	public GameObject itemMenuPanel;
    public GameObject canvas;
    public GameObject canvasWhenGamePaused;
    public GameObject cameraPlayer;
	public GameObject [] itemSlots = new GameObject[6];
	public GameObject [] itemSlotsObjects = new GameObject[6];
	public int usedItemSlots;
	// Use this for initialization
	void Start () {
        canvas = itemMenuPanel.transform.parent.gameObject;
        cameraPlayer = canvas.transform.parent.gameObject;
        canvasWhenGamePaused.transform.SetParent(cameraPlayer.transform);
        itemMenuPanel.SetActive (false);
		usedItemSlots = 0;

	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetButton("Pause")) {
			if (itemMenuPanel.activeSelf) {
                canvas.transform.SetParent(cameraPlayer.transform);
                
				Time.timeScale = 1;
			} else {
                canvas.transform.SetParent(gameObject.transform);
                Time.timeScale = 0;
			}
			itemMenuPanel.SetActive(!itemMenuPanel.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			addItemToISM ("Key1.1");
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			addItemToISM ("Key1.2");
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			addItemToISM ("Key1.3");
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			removeItemFromISM (1);
		}
	}

	//add Item to ItemSlotMenu
	public void addItemToISM(string itemname){
		Debug.Log ("putIntoItemMenu " + itemname);
		itemSlots [usedItemSlots].SetActive (true);
		itemSlots [usedItemSlots].transform.FindChild ("Text").GetComponent<Text>().text = itemname;
		itemSlotsObjects [usedItemSlots] = GameObject.Find (itemname);
		itemSlotsObjects [usedItemSlots].SetActive (false);
		usedItemSlots++;
	}

	public void removeItemFromISM(int slotNumber){
		Debug.Log ("removeItemFromISM " + slotNumber);
		Debug.Log ("itemSlotsObjects [1] " + itemSlotsObjects [slotNumber]);
		itemSlotsObjects [slotNumber].SetActive (true);
		itemSlots [usedItemSlots-1].SetActive (false);
		for (int i = slotNumber; i < usedItemSlots; i++) {
			itemSlots [i].transform.FindChild ("Text").GetComponent<Text> ().text = itemSlots [i+1].transform.FindChild ("Text").GetComponent<Text> ().text;
			itemSlotsObjects [i] = itemSlotsObjects [i + 1];
		}
		usedItemSlots--;
	}
}
