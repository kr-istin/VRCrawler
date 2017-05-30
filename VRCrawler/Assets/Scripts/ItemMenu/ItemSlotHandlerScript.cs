using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotHandlerScript : MonoBehaviour {
	public GameObject cameraPlayer;
	public GameObject canvas;
	public GameObject canvasParent;
	public GameObject itemMenuPanel;
    public GameObject canvasWhenGamePaused;
	public GameObject [] itemSlots = new GameObject[6];
	public GameObject [] itemSlotsObjects = new GameObject[6];
	public int usedItemSlots;
//	private float lastTime;
	VRTK.VRTK_ControllerEvents controllerEvents;

	// Use this for initialization
	void Start () {
//		lastTime = Time.realtimeSinceStartup;
		cameraPlayer = VRTK.VRTK_SDKManager.instance.actualBoundaries.gameObject;
		canvas = cameraPlayer.transform.FindChild ("Camera (eye)").FindChild ("Canvas").gameObject;
		canvasParent = canvas.transform.parent.gameObject;
		//canvas = cameraPlayer.transform.FindChild("Canvas").gameObject;
		itemMenuPanel = canvas.transform.FindChild ("ItemMenuPanel").gameObject;
		itemMenuPanel.SetActive (false);

        canvasWhenGamePaused = Instantiate<GameObject>(canvas);
		canvasWhenGamePaused.transform.SetParent(canvasParent.transform);
        canvasWhenGamePaused.SetActive(false);
        canvasWhenGamePaused.transform.position = canvas.transform.position;
        canvasWhenGamePaused.transform.rotation = canvas.transform.rotation;
        
		int i = 0;
		foreach (Transform itemSlot in itemMenuPanel.transform){
			itemSlots [i] = itemSlot.gameObject;
			itemSlots [i].SetActive (false);
			i++;
		}

		usedItemSlots = 0;

		//VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_Pointer> ().enabled = false;
		VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_StraightPointerRenderer> ().enabled = false;
		controllerEvents = VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_ControllerEvents>();
		//Button Two ist oberer Menu Button
		controllerEvents.ButtonTwoPressed += ControllerEvents_StartMenuPressed;


	}

	private void ControllerEvents_StartMenuPressed(object sender, VRTK.ControllerInteractionEventArgs e){
		Debug.Log ("StartMenu pressed");
		activateGame ();
	}

	// Update is called once per frame
	void Update () {
//		float deltaTime = Time.realtimeSinceStartup - lastTime;
		//ausführen mit deltatime

//		lastTime = Time.realtimeSinceStartup;
		if (Input.GetButton("Pause")) {
			activateGame ();
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


	public void activateGame(){
		if (itemMenuPanel.activeSelf) {
			//Time.timeScale = 1;
			//VRTK.VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK.VRTK_Pointer> ().enabled = true;
			VRTK.VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK.VRTK_Pointer> ().enableTeleport = true;
			//VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_Pointer> ().enabled = false;
			VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_StraightPointerRenderer> ().enabled = false;
			canvas.transform.position = canvasWhenGamePaused.transform.position;
			canvas.transform.rotation = canvasWhenGamePaused.transform.rotation;
			canvas.transform.SetParent(canvasParent.transform);

		} else {
			//Time.timeScale = 0;
			//VRTK.VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK.VRTK_Pointer> ().enabled = false;
			VRTK.VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<VRTK.VRTK_Pointer> ().enableTeleport = false;
			//VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_Pointer> ().enabled = true;
			VRTK.VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<VRTK.VRTK_StraightPointerRenderer> ().enabled = true;
			canvasWhenGamePaused.transform.position = canvas.transform.position;
			canvasWhenGamePaused.transform.rotation = canvas.transform.rotation;
			canvas.transform.SetParent(gameObject.transform);

		}
		itemMenuPanel.SetActive(!itemMenuPanel.activeSelf);
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
