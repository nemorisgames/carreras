using UnityEngine;
using System.Collections;

public class Carrera : MonoBehaviour {

	enum Estado{
		Preparacion,
		Activo,
		Fin
	}

	public UILabel LabelPartida;
	public UILabel labelVuelta;
	public UILabel labelCronometro;

	public static Pista prefabPista;
	private Pista pista;
	private AutomovilV2 auto;

	private int numeroVueltaActual = 0;
	public int maxNumeroDeVuelta = 5;
	float tiempoPartida;
	float tiempoFinal;

	Estado estado = Estado.Preparacion;

	private TipoCheckpoint ultimoCheckpoint = TipoCheckpoint.Inicial;


	void Awake() {

		if( prefabPista != null ) {
			pista = Instantiate<Pista>( prefabPista );
			pista.transform.position = Vector3.zero;
		}
		else{
			pista = Instantiate<Pista>( Resources.Load<Pista>( "Pistas/pista1" ) );
			pista.transform.position = Vector3.zero;
		}
		
		if( pista != null ) {
			pista.accionCheckPointAlcanzado = CheckpointAlcanzado;
		}

		auto = Instantiate<AutomovilV2>( Resources.Load<AutomovilV2>( "Autos/auto" ) );

		auto.transform.position = pista.posicionInicialAuto.position;
		auto.transform.rotation = pista.posicionInicialAuto.rotation;

		labelVuelta.text = numeroVueltaActual + "/" + maxNumeroDeVuelta;
		StartCoroutine( InicioDeCarreraRutina() );

		LabelPartida.gameObject.SetActive( false );
	}

	private IEnumerator InicioDeCarreraRutina(){
		//TODO: animacion o algo asi?

		yield return new WaitForSeconds( 2f );
		LabelPartida.gameObject.SetActive( true );
		LabelPartida.text = "3";
		yield return new WaitForSeconds( 1f );
		LabelPartida.text = "2";
		yield return new WaitForSeconds( 1f );
		LabelPartida.text = "1";
		yield return new WaitForSeconds( 1f );
		LabelPartida.text = "Partida!";
		tiempoPartida = Time.time;
		estado = Estado.Activo;
		yield return new WaitForSeconds( 1f );
		LabelPartida.gameObject.SetActive( false );
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
		yield return new WaitForSeconds( 3f );
		Application.LoadLevel( "EscenaTitulo" );
		yield return null;
	}
}
