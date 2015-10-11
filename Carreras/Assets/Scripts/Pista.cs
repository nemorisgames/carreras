using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pista : MonoBehaviour {

	public SeccionPista seccionInicial;

	public bool AgregarSeccion( SeccionPista seccionPista ) {
		if( seccionPista == null ) {
			return false;
		}

		if( seccionInicial == null ){
			seccionPista.transform.parent = transform;
			seccionPista.transform.localPosition = Vector3.zero;
			seccionInicial = seccionPista;
		}
		else {
			SeccionPista ultimaSeccion = ObtenerUltimaSeccion();
			Transform ultimoEnlace = ultimaSeccion.vecino1 == null ? ultimaSeccion.enlace1 : ultimaSeccion.enlace2;

			//TODO: revisar superposicion de secciones caso 1
			
			//por ahora asumimos enlace 1
			seccionPista.HacerVecino1( ultimaSeccion, ultimoEnlace );

			//TODO: en caso de falla, revisar superposicion de secciones caso 2
		}

		return true;

	}

	public SeccionPista ObtenerUltimaSeccion(){
		if( seccionInicial == null ) {
			return null;
		}
		SeccionPista ultimaSeccion = seccionInicial;
		SeccionPista seccionAnterior = null;
		bool continuarIterando = true;
		do {
			continuarIterando = false;
			if( ultimaSeccion.vecino1 != seccionAnterior && ultimaSeccion.vecino1 != null ) {
				seccionAnterior = ultimaSeccion;
				ultimaSeccion = ultimaSeccion.vecino1;
				continuarIterando = true;
				continue;
			}
			if( ultimaSeccion.vecino2 != seccionAnterior && ultimaSeccion.vecino2 != null ) {
				seccionAnterior = ultimaSeccion;
				ultimaSeccion = ultimaSeccion.vecino2;
				continuarIterando = true;
				continue;
			}
		} while( continuarIterando );

		return ultimaSeccion;
	}
}
