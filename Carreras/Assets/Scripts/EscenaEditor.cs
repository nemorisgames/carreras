using UnityEngine;
using System.Collections;

public class EscenaEditor : MonoBehaviour {

	public SeccionPista initialPrefab;

	public SeccionPista prefabSeccion1;
	public SeccionPista prefabSeccion2;

	public Pista pista;

	public Camera camaraEscena;

	void Start () {
		camaraEscena.transform.position = new Vector3( 0, camaraEscena.transform.position.y, 0 );
		AgregarSeccion( initialPrefab );
	}

	void Update () {
		if( Input.GetKeyDown( KeyCode.Keypad1 ) ) {
			MoveCamera( AgregarSeccion( prefabSeccion1 ) );
		}

		if( Input.GetKeyDown( KeyCode.Keypad2 ) ) {
			MoveCamera( AgregarSeccion( prefabSeccion2 ) );
		}

		if( Input.GetKeyDown( KeyCode.R ) ) {
			pista.RotarUltimaSeccion();
		}

		if( Input.GetKeyDown( KeyCode.D ) ) {
			pista.BorrarUltimaSeccion();
			contadorSecciones--;
			SeccionPista ultimaSeccion = pista.ObtenerUltimaSeccion();
			MoveCamera( ultimaSeccion == null ? Vector3.zero : ultimaSeccion.transform.position );
		}
	
	}

	int contadorSecciones = 0;
	Vector3 AgregarSeccion ( SeccionPista prefabSeccion ) {
		SeccionPista nuevaSeccion = Instantiate<SeccionPista>( prefabSeccion );
		nuevaSeccion.gameObject.name = "seccion_" + contadorSecciones;
		contadorSecciones++;
		if( !pista.AgregarSeccion( nuevaSeccion ) ) {
			Destroy( nuevaSeccion.gameObject );
		}
		SeccionPista ultimaSeccion = pista.ObtenerUltimaSeccion();
		if( ultimaSeccion == null ) {
			return Vector3.zero;
		}
		return ultimaSeccion.transform.position;
	}

	void MoveCamera( Vector3 position ) {
		position.y = camaraEscena.transform.position.y;
		TweenPosition.Begin( camaraEscena.gameObject, 0.8f, position ).method = UITweener.Method.EaseOut;
	}
}
