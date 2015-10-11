using UnityEngine;
using System.Collections;

public class SeccionPista : MonoBehaviour {

	const float  ENLACE_DISTANCIA_GIZMO = 0.3f;
	const float  ENLACE_RADIO_GIZMO = 0.05f;

	public Transform enlace1;
	public Transform enlace2;

	[HideInInspector]
	public SeccionPista vecino1;
	[HideInInspector]
	public SeccionPista vecino2;

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		if( enlace1 != null ) {
			Gizmos.DrawSphere( enlace1.position, ENLACE_RADIO_GIZMO );
			Gizmos.DrawRay( enlace1.position, enlace1.forward * ENLACE_DISTANCIA_GIZMO );
		}
		Gizmos.color = Color.blue;
		if( enlace2 != null ) {
			Gizmos.DrawSphere( enlace2.position, ENLACE_RADIO_GIZMO );
			Gizmos.DrawRay( enlace2.position, enlace2.forward * ENLACE_DISTANCIA_GIZMO );
		}
	}

	public void HacerVecino1( SeccionPista nuevoVecino, Transform enlaceVecino ) {
		HacerVecino( enlace1, nuevoVecino, enlaceVecino );
	}

	public void HacerVecino2( SeccionPista nuevoVecino, Transform enlaceVecino ) {
		HacerVecino( enlace2, nuevoVecino, enlaceVecino );
	}

	public void EliminarVecino( SeccionPista vecino ) {
		if( vecino.vecino1 == this ) {
			vecino.vecino1 = null;
		}
		else if( vecino.vecino2 == this  ) {
			vecino.vecino2 = null;
		}

		if( vecino1 == vecino ) {
			vecino1 = null;
		}
		else if( vecino2 == vecino  ) {
			vecino2 = null;
		}
	}

	private void HacerVecino( Transform enlace, SeccionPista nuevoVecino, Transform enlaceVecino ) {
		transform.parent = nuevoVecino.transform.parent;

		if( enlace == enlace1  ) {
			vecino1 = nuevoVecino;
		}
		else {
			vecino2 = nuevoVecino;
		}

		if( enlaceVecino == nuevoVecino.enlace1  ) {
			nuevoVecino.vecino1 = this;
		}
		else {
			nuevoVecino.vecino2 = this;
		}

		Vector3 direccion = -enlaceVecino.forward;
		float anguloFinal = Mathf.Atan2( direccion.z, direccion.x ) * Mathf.Rad2Deg;
		float anguloInicial = Mathf.Atan2( enlace.forward.z, enlace.forward.x ) * Mathf.Rad2Deg;
		float deltaAngulo = anguloFinal - anguloInicial;

		//rotamos para dejar el delta angulo en 0
		transform.Rotate( new Vector3( 0f, -deltaAngulo, 0f ) );

		Vector3 deltaPos = transform.position - enlace.position;		
		transform.position = enlaceVecino.position + deltaPos;
	}
}
