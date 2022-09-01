using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjectsInTile : MonoBehaviour {

	[SerializeField]
	private string objectsTag = "TileObject";

	private GameObject[] objectsToCheck;

	private Bounds tileBounds;

	private bool currentActivation = true;

	void Awake() {
		objectsToCheck = GameObject.FindGameObjectsWithTag (objectsTag);

		Terrain tileTerrain = GetComponent<Terrain> ();
		Vector3 tileDimensions = new Vector3 (tileTerrain.terrainData.heightmapResolution, 1000, tileTerrain.terrainData.heightmapResolution);
		Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + tileDimensions.x / 2f, 0, this.gameObject.transform.position.z + tileDimensions.z / 2f);
		this.tileBounds = new Bounds (tilePosition, tileDimensions);
	}

	public void ActivateTile(bool activate) {
		if (activate != currentActivation) {
			this.gameObject.SetActive (activate);
			foreach (GameObject objectInGame in objectsToCheck) {
				Vector3 objectPosition = objectInGame.transform.position;
				if (tileBounds.Contains (objectPosition)) {
					objectInGame.SetActive (activate);
				}
			}
			currentActivation = activate;
		}
	}
}
