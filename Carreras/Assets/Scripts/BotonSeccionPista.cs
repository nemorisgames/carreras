using UnityEngine;
using System.Collections;

public class BotonSeccionPista : MonoBehaviour {

	private SeccionPista prefabSeccion;
	private System.Action<SeccionPista> accionTap;
	public UILabel nombre;

	public void Configurar( SeccionPista _prefabSeccion, System.Action<SeccionPista> _accionTap ){
		prefabSeccion = _prefabSeccion;
		accionTap = _accionTap;
		nombre.text = prefabSeccion.name;
	}

	public void Tap(){
		if( accionTap != null ) {
			accionTap( prefabSeccion );
		}
	}
}
