using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {
	public Transform objetivo;

	public Rect limites;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		objetivo = FindObjectOfType<AutomovilV2>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, offset + new Vector3 (Mathf.Clamp(objetivo.position.x, limites.x, limites.x + limites.width), transform.position.y, Mathf.Clamp(objetivo.position.z, limites.y, limites.y + limites.height)), Time.deltaTime * 3f);
	}
}
