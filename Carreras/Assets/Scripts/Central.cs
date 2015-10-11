using UnityEngine;
using System.Collections;

public class Central : MonoBehaviour {
	public AutomovilV2 automovilV2;
	public UISprite manubrio;
	public UISprite aceleracion;
	Vector2 deltaPos1;
	Vector2 deltaPos2;
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

	void OnGUI(){
		//GUI.Box (new Rect (0, 0, 300, 100), deltaPos1 + " " + deltaPos2);
	}

	// Update is called once per frame
	void Update () {
		if (Input.touches.Length > 0) {
			bool toqueDerecho = false;
			bool toqueIzquierdo = false;
			foreach (Touch t in Input.touches) {
				if (t.position.x < Screen.width / 2f) {
					if(deltaPos1 == Vector2.zero)
						manubrio.transform.localPosition = new Vector3(t.position.x, t.position.y, 0f);
					deltaPos1 += t.deltaPosition;
					toqueIzquierdo = true;
				} else {
					if(deltaPos2 == Vector2.zero)
						aceleracion.transform.localPosition = new Vector3(t.position.x, t.position.y, 0f);
					deltaPos2 += t.deltaPosition;
					toqueDerecho = true;
				}
			}
			if (!toqueIzquierdo)
				deltaPos1 = Vector2.zero;
			if (!toqueDerecho)
				deltaPos2 = Vector2.zero;
		} else {
			deltaPos1 = Vector2.zero;
			deltaPos2 = Vector2.zero;
		}
		automovilV2.girar (1f * Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
		automovilV2.acelerar (1.9f * Mathf.Clamp(deltaPos2.y, -30f, 30f) / 30f);
	}
}
