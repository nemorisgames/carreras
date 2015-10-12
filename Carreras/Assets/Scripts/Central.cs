using UnityEngine;
using System.Collections;

public class Central : MonoBehaviour {
	public AutomovilV2 automovilV2;
	public UISprite manubrio;
	public UISprite aceleracion;
	Vector2 deltaPos1;
	Vector2 deltaPos2;
	public int tipoControl = 0;
	float angulo = 0f;
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
	
	public void cargarEscena4(){
		Application.LoadLevel ("Escena4");
	}

	public void repeatButtonPress(string tipo){
		switch (tipo) {
		case "izquierda":
			automovilV2.girar (-1f);
			break;
		case "derecha":
			automovilV2.girar (1f);
			break;
		case "arriba":
			automovilV2.acelerar (1.5f);
			break;
		case "abajo":
			automovilV2.retroceder (1f);
			break;
		}
	}

	void OnGUI(){
		GUI.Box (new Rect (0, 0, 300, 100), angulo + " " + (Mathf.Clamp((angulo) / 60f, -2f, 2f)));
		if (tipoControl == 1) {
			/*if(GUI.RepeatButton(new Rect(50, Screen.height - 100, 50, 50), "<")){
				automovilV2.girar (-1f);//Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
			}
			if(GUI.RepeatButton(new Rect(100, Screen.height - 100, 50, 50), ">")){
				automovilV2.girar (1f);//Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
			}
			if(GUI.RepeatButton(new Rect(Screen.width - 100, Screen.height - 150, 50, 50), "!")){
				automovilV2.acelerar (1.5f); //Mathf.Clamp(deltaPos2.y, -60f, 60f) / 60f);
			}
			if(GUI.RepeatButton(new Rect(Screen.width - 100, Screen.height - 100, 50, 50), "¡")){
				automovilV2.retroceder (1f); //Mathf.Clamp(deltaPos2.y, -60f, 60f) / 60f);
			}*/
		}
	}

	// Update is called once per frame
	void Update () {
		if (tipoControl == 0) {
			bool toqueDerecho = false;
			bool toqueIzquierdo = false;
			if (Input.touches.Length > 0) {
				foreach (Touch t in Input.touches) {
					if (t.position.x < Screen.width / 2f) {
						if (deltaPos1 == Vector2.zero)
							manubrio.transform.localPosition = new Vector3 (t.position.x - Screen.width / 2f, t.position.y - Screen.height / 2f, 0f);
						deltaPos1 += t.deltaPosition;
						toqueIzquierdo = true;
					} else {
						//if(deltaPos2 == Vector2.zero)
						//	aceleracion.transform.localPosition = new Vector3(t.position.x - Screen.width / 2f, t.position.y - Screen.height / 2f, 0f);
						//deltaPos2 += t.deltaPosition;
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
			automovilV2.girar (1f * (deltaPos1.x != 0f ? (deltaPos1.x < 0f ? -1f : 1f) : 0f));//Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
			automovilV2.acelerar (1.5f * (toqueDerecho ? 1f : 0f)); //Mathf.Clamp(deltaPos2.y, -60f, 60f) / 60f);
		}
		if(tipoControl == 2){
			bool toqueDerecho = false;
			//bool toqueIzquierdo = false;
			if (Input.touches.Length > 0) {
				foreach (Touch t in Input.touches) {
					if (t.position.x < Screen.width / 2f) {
					/*	if (deltaPos1 == Vector2.zero)
							manubrio.transform.localPosition = new Vector3 (t.position.x - Screen.width / 2f, t.position.y - Screen.height / 2f, 0f);
						deltaPos1 += t.deltaPosition;
						toqueIzquierdo = true;*/
					} else {
						if(deltaPos2 == Vector2.zero)
							aceleracion.transform.localPosition = new Vector3(t.position.x - Screen.width / 2f, t.position.y - Screen.height / 2f, 0f);
						deltaPos2 += t.deltaPosition;
						toqueDerecho = true;
					}
				}
				//if (!toqueIzquierdo)
				//	deltaPos1 = Vector2.zero;
				if (!toqueDerecho)
					deltaPos2 = Vector2.zero;
			} else {
				//deltaPos1 = Vector2.zero;
				deltaPos2 = Vector2.zero;
			}
			automovilV2.girar (1f * Mathf.Clamp(deltaPos2.x, -60f, 60f) / 60f);//Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
			automovilV2.acelerar (1.5f * Mathf.Clamp(deltaPos2.y, -30f, 60f) / 60f); //Mathf.Clamp(deltaPos2.y, -60f, 60f) / 60f);
		}
		if(tipoControl == 3){
			bool toqueDerecho = false;
			//bool toqueIzquierdo = false;
			if (Input.touches.Length > 0) {
				foreach (Touch t in Input.touches) {
					//if (t.position.x < Screen.width / 2f) {
						/*	if (deltaPos1 == Vector2.zero)
							manubrio.transform.localPosition = new Vector3 (t.position.x - Screen.width / 2f, t.position.y - Screen.height / 2f, 0f);
						deltaPos1 += t.deltaPosition;
						toqueIzquierdo = true;*/
					//} else {
						if(deltaPos2 == Vector2.zero)
							aceleracion.transform.localPosition = new Vector3(t.position.x - Screen.width / 2f, t.position.y - Screen.height / 2f, 0f);
						deltaPos2 += t.deltaPosition;
						toqueDerecho = true;
					//}
				}
				//if (!toqueIzquierdo)
				//	deltaPos1 = Vector2.zero;
				if (!toqueDerecho)
					deltaPos2 = Vector2.zero;
			} else {
				//deltaPos1 = Vector2.zero;
				deltaPos2 = Vector2.zero;
			}
			//automovilV2.girar (1f * Mathf.Clamp(deltaPos2.x, -60f, 60f) / 60f);//Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
			//automovilV2.acelerar (1.5f * Mathf.Clamp(deltaPos2.y, -30f, 60f) / 60f); //Mathf.Clamp(deltaPos2.y, -60f, 60f) / 60f);
			if(toqueDerecho){ 
				automovilV2.acelerar (1.5f * Mathf.Clamp(deltaPos2.sqrMagnitude / 60f, 0f, 1f));
				int sign = Vector3.Cross(automovilV2.transform.forward, new Vector3(deltaPos2.x, 0f, deltaPos2.y)).y < 0 ? -1 : 1;
				angulo = (sign * Vector3.Angle(automovilV2.transform.forward, new Vector3(deltaPos2.x, 0f, deltaPos2.y)));
				automovilV2.girar (Mathf.Clamp((angulo) / 60f, -2f, 2f));//Mathf.Clamp(deltaPos1.x, -30f, 30f) / 30f);
			}
		}
	}
}
