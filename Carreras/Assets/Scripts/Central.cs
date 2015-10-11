using UnityEngine;
using System.Collections;

public class Central : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void reset(){
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void cargarEscena1(){
		Application.LoadLevel ("Escena1");
	}
	
	public void cargarEscena2(){
		Application.LoadLevel ("Escena2");
	}
	
	public void cargarEscena3(){
		Application.LoadLevel ("Escena3");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
