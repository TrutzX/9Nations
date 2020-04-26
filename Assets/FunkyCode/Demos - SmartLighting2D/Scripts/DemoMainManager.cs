using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoMainManager : MonoBehaviour {
	public RectTransform UIObject;
	public RectTransform UIBack;
	public LightingDemoScene menuScene;

	public LightingDemoScene[] demoScenes;
	public int currentSceneID = 0;

	private Vector3 startPosition;
	private GameObject currentScene = null;

	public void NextScene() {
		if (demoScenes.Length < currentSceneID + 2) {
			Destroy (currentScene);
			SetScene(0);
		} else {
			Destroy (currentScene);
			SetScene(currentSceneID+1);
		}
	}

	public void PrevScene() {
		if (currentSceneID < 1) {
			Destroy (currentScene);
			SetScene(demoScenes.Length - 1);
		} else {
			Destroy (currentScene);
			SetScene(currentSceneID-1);
		}
	}

	public void ResetScene() {
		Destroy (currentScene);
		SetScene (currentSceneID);
	}

	public void SetScene(int id) {
		currentSceneID = id;
		if (demoScenes[id].scene != null) {
			currentScene = Instantiate(demoScenes[id].scene) as GameObject;
			currentScene.transform.position = new Vector3 (0f, 0f, 0f);
		}
		
	}

	public void SetMainMenu() {
		Destroy(currentScene);

		currentScene = null;
	}

	void Start() {
		startPosition = UIObject.position;

		Application.targetFrameRate = 60;
	}

	void Update () {
		Camera mainCamera = Camera.main;
		if (currentScene != null) {
			if (currentScene.activeSelf == false) {
				currentScene.SetActive (true);
			}

			UIBack.gameObject.SetActive (true);

			menuScene.scene.SetActive(false);

			Vector3 position = mainCamera.transform.position;
			position.x = position.x * 0.95f + currentScene.transform.position.x * 0.05f;
			mainCamera.transform.position = position;

			mainCamera.orthographicSize = demoScenes[currentSceneID].cameraSize;

			position = UIObject.position;
			position.y = position.y * 0.95f + -500f * 0.05f;
			UIObject.position = position;
			if (UIObject.position.y < -350f) {
				UIObject.gameObject.SetActive (false);
			}

		} else {
			UIObject.gameObject.SetActive (true);
			UIBack.gameObject.SetActive (false);
			menuScene.scene.SetActive(true);

			Vector3 position = UIObject.position;
			position.y = position.y * 0.95f + startPosition.y * 0.05f;
			UIObject.position = position;

			mainCamera.orthographicSize = menuScene.cameraSize;
		}
	}
}