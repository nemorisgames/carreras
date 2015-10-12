using UnityEngine;
using System.Collections;

public class EscenaTitulos : MonoBehaviour {


	public void Continuar() {
		Carrera.prefabPista = Resources.Load<Pista>( "Pistas/PistaBuena" );
		Application.LoadLevel( "Escena1" );
	}
}
