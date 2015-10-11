﻿using UnityEngine;
using System.Collections;

public class EscenaEditor : MonoBehaviour {

	public SeccionPista prefabSeccion1;
	public SeccionPista prefabSeccion2;

	public Pista pista;

	void Update () {
		if( Input.GetKeyDown( KeyCode.Keypad1 ) ) {
			AgregarSeccion( prefabSeccion1 );
		}

		if( Input.GetKeyDown( KeyCode.Keypad2 ) ) {
			AgregarSeccion( prefabSeccion2 );
		}

		if( Input.GetKeyDown( KeyCode.R ) ) {
			pista.RotarUltimaSeccion();
		}

		if( Input.GetKeyDown( KeyCode.D ) ) {
			pista.BorrarUltimaSeccion();
		}
	
	}
	int contador = 0;
	void AgregarSeccion ( SeccionPista prefabSeccion ) {
		SeccionPista nuevaSeccion = Instantiate<SeccionPista>( prefabSeccion );
		nuevaSeccion.gameObject.name = "seccion_" + contador;
		contador++;
		if( !pista.AgregarSeccion( nuevaSeccion ) ) {
			Destroy( nuevaSeccion.gameObject );
		}
	}
}