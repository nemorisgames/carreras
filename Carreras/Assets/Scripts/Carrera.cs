using UnityEngine;
using System.Collections;

public class Carrera : MonoBehaviour {

	enum Estado{
		Preparacion,
		Activo,
		Fin
	}

	public UILabel labelVuelta;
	public UILabel labelCronometro;

	public static Pista prefabPista;
	private Pista pista;

	private int numeroVueltaActual = 0;
	public int maxNumeroDeVuelta = 5;
	float tiempoPartida;
	float tiempoFinal;

	Estado estado = Estado.Preparacion;

	private TipoCheckpoint ultimoCheckpoint = TipoCheckpoint.Inicial;


	void Start() {
		pista = FindObjectOfType<Pista>();
		if( pista == null ) {
			if( prefabPista != null ) {
				pista = Instantiate<Pista>( prefabPista );
				pista.transform.position = Vector3.zero;
			}
			else{
				//TODO: load default?
			}
		}

		if( pista != null ) {
			pista.accionCheckPointAlcanzado = CheckpointAlcanzado;
		}
		labelVuelta.text = numeroVueltaActual + "/" + maxNumeroDeVuelta;
		StartCoroutine( InicioDeCarreraRutina() );
	}

	private IEnumerator InicioDeCarreraRutina(){
		//TODO: animacion o algo asi?
		tiempoPartida = Time.time;
		estado = Estado.Activo;
		yield return null;
	}

	void CheckpointAlcanzado( TipoCheckpoint nuevoCheckpoint ) {

		//En verdad deveria revisar la secuencia, pero esto funciona por ahora

		switch( nuevoCheckpoint ) {
		case TipoCheckpoint.Inicial:
			if( ultimoCheckpoint == TipoCheckpoint.Final ) {
				ultimoCheckpoint = nuevoCheckpoint;
				AumentarContadorVueltas();

			}
			break;
		case TipoCheckpoint.Intermedio:
			if( ultimoCheckpoint == TipoCheckpoint.Inicial ) {
				ultimoCheckpoint = nuevoCheckpoint;
			}
			break;
		case TipoCheckpoint.Final:
			if( ultimoCheckpoint == TipoCheckpoint.Intermedio ) {
				ultimoCheckpoint = nuevoCheckpoint;
			}
			break;
		default:
			break;
		}
	}

	void Update() {
		if( estado != Estado.Activo ) {
			return;
		}
		RefrescarCronometro();
	}

	void RefrescarCronometro(){
		float tiempoTranscurrido = Time.time - tiempoPartida;
		int minutos = ( int ) ( tiempoTranscurrido / 60f );
		tiempoTranscurrido -= minutos * 60f;
		int segundos = ( int ) (tiempoTranscurrido / 1f );
		tiempoTranscurrido -= (float) segundos;

		int centesimas = ( int )( tiempoTranscurrido * 100f );

		labelCronometro.text = minutos.ToString("D2") + ":" + segundos.ToString("D2") + ":" + centesimas.ToString("D2");
	}

	void AumentarContadorVueltas(){
		numeroVueltaActual++;
		labelVuelta.text = numeroVueltaActual + "/" + maxNumeroDeVuelta;
		if( numeroVueltaActual >= maxNumeroDeVuelta ) {
			StartCoroutine( FinDeCarreraRutina() );
		}
	}

	private IEnumerator FinDeCarreraRutina(){
		estado = Estado.Fin;
		tiempoFinal = Time.time;
		RefrescarCronometro();
		//TODO: animacion o algo asi?
		yield return null;
	}
}
