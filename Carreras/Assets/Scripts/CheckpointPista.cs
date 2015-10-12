using UnityEngine;
using System.Collections;

public enum TipoCheckpoint{
	Inicial,
	Intermedio,
	Final
}

public class CheckpointPista : MonoBehaviour {

	public TipoCheckpoint tipo;
	System.Action<TipoCheckpoint> accionAutoDetectado;

	public void Configurar( System.Action<TipoCheckpoint> _accionAutoDetectado ){
		accionAutoDetectado = _accionAutoDetectado;
	}

	void OnTriggerEnter(Collider other) {
		ReferenciaAuto referenciaAuto = other.GetComponent<ReferenciaAuto>();
		if( referenciaAuto != null && accionAutoDetectado != null ) {
			accionAutoDetectado( tipo );
		}

	}
}
