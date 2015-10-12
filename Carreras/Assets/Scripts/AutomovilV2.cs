using UnityEngine;
using System.Collections;

public class AutomovilV2 : MonoBehaviour {
	public Rigidbody rigidbody;
	public float rigidbodyMass;
	public float rigidbodyDrag;
	public float rigidbodyAngularDrag;
	public Transform centroGravedad;
	public float aceleracion = 10f;
	public float giro = 3f;
	public float friccion = 10f;
	public float multiplicadorDanio = 5f;
	public float velocidadMaxima;
	public float velocidadAngularMaxima;
	public IntegridadParte parteDelanteraIzq;
	public IntegridadParte parteDelanteraDer;
	public IntegridadParte parteTraseraIzq;
	public IntegridadParte parteTraseraDer;
	bool reparando = false;
	Vector2 test;
	public float tiempoSinInput = 0f;
	Central central;
	// Use this for initialization
	void Start () {
		central = Camera.main.GetComponent<Central> ();
		rigidbody.centerOfMass = centroGravedad.localPosition;
		rigidbody.mass = rigidbodyMass;
		rigidbody.drag = rigidbodyDrag;
		rigidbody.angularDrag = rigidbodyAngularDrag;
		rigidbody.maxAngularVelocity = velocidadAngularMaxima;
		parteDelanteraIzq.particulas.Stop(true);
		parteDelanteraDer.particulas.Stop(true);
		parteTraseraIzq.particulas.Stop(true);
		parteTraseraDer.particulas.Stop(true);
	}
	
	public void acelerar(float multiplicador){
		if (velocidadMaxima < rigidbody.velocity.sqrMagnitude)
			return;
		test.y = multiplicador;
		if(multiplicador > 0f) tiempoSinInput = 0f;
		rigidbody.AddForce ( transform.forward * multiplicador * aceleracion * Mathf.Clamp((parteTraseraIzq.integridad + parteTraseraDer.integridad) / 200f, 0.5f, 1f));
	}

	public void retroceder(float multiplicador){
		if (velocidadMaxima < rigidbody.velocity.sqrMagnitude)
			return;
		rigidbody.AddForce ( transform.forward * multiplicador * -aceleracion);
	}

	bool IsGrounded(){ 
		return (Physics.Raycast (transform.position + transform.up * 0.1f, -Vector3.up, 0.2f));
		
	}

	bool IsBloquedFront(){
		return (Physics.Raycast (transform.position + 0.1f * transform.up + 0.2f * transform.right, transform.forward, 4f) || Physics.Raycast (transform.position + 0.1f * transform.up - 0.2f * transform.right, transform.forward, 4f));
	}

	IEnumerator repararRutina(){
		while (reparando) {
			reparar ();
			yield return new WaitForSeconds(0.2f);
		}
		yield break;
	}

	public void reparar(){
		modificarIntegridad (parteDelanteraIzq, 0.5f);
		modificarIntegridad (parteDelanteraDer, 0.5f);
		modificarIntegridad (parteTraseraIzq, 0.5f);
		modificarIntegridad (parteTraseraDer, 0.5f);
	}
	
	void OnTriggerEnter(Collider other) {
		reparando = true;
		StartCoroutine (repararRutina ());
	}
	
	void OnTriggerExit(Collider other) {
		reparando = false;
	}

	void modificarIntegridad(IntegridadParte p, float valor){
		p.integridad += valor;
		p.integridad = Mathf.Clamp (p.integridad, 0f, 100f);
		if (p.integridad < 25f) {
			if (!p.particulas.isPlaying) {
				p.particulas.Play (true);
			}
		} else {
			if (p.particulas.isPlaying) {
				p.particulas.Stop (true);
			}
		}
		p.sprite.color = new Color(1f, p.integridad / 100f, p.integridad / 100f);

	}

	/*void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Obstaculo")) {
			foreach(ContactPoint c in collision.contacts){
				switch(c.thisCollider.name){
				case "ColliderDelanteroIzq":
					modificarIntegridad(parteDelanteraIzq, -rigidbody.velocity.magnitude * multiplicadorDanio);
					break;
				case "ColliderDelanteroDer":
					modificarIntegridad(parteDelanteraDer, -rigidbody.velocity.magnitude * multiplicadorDanio);
					break;
				case "ColliderTraseroIzq":
					modificarIntegridad(parteTraseraIzq, -rigidbody.velocity.magnitude * multiplicadorDanio);
					break;
				case "CollidertraseroDer":
					modificarIntegridad(parteTraseraDer, -rigidbody.velocity.magnitude * multiplicadorDanio);
					break;
				}
			}

		}
	}*/

	public void girar(float multiplicador){
		test.x = multiplicador;
		float multiplicadorDireccion = Mathf.Sign( Vector3.Dot( transform.forward, rigidbody.velocity ) );
		rigidbody.AddRelativeTorque(0f, multiplicadorDireccion * multiplicador * giro * Mathf.Clamp(parteDelanteraIzq.integridad / 100f + 0.5f, 0.2f, 1f), 0f);
	}

	void OnGUI(){
		//GUI.Box(new Rect(0, Screen.height - 100, 300, 80), "" + test);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//print (rigidbody.velocity.magnitude + " " + rigidbody.angularVelocity.magnitude);
		if (!IsGrounded ())
			return;

		tiempoSinInput += Time.deltaTime;

		if (Input.GetKey (KeyCode.A)) {
			//acelerar (1f);
			girar(-1f);
		}
		if (Input.GetKey (KeyCode.D)) {
			//acelerar (1f);
			girar(1f);
		}
		if (Input.GetKey (KeyCode.W)) {
			acelerar (1.5f);
		}
		if (Input.GetKey (KeyCode.S) || (tiempoSinInput > 2f && central.tipoControl == 0)) {
			retroceder (0.8f);
		}

		/*if (Input.touches.Length > 0) {
			for(int i = 0; i < Input.touches.Length; i++){
				if (Input.GetTouch(i).position.x < Screen.width / 2f) {
					acelerar();
					rigidbody.AddTorque(0f, -giro * Mathf.Clamp(parteDelanteraIzq.integridad / 100f + 0.5f, 0.2f, 1f), 0f);
					//transform.Rotate(0f, giro, 0f);
				}
				else {
					acelerar();
					rigidbody.AddTorque(0f, giro * Mathf.Clamp(parteDelanteraDer.integridad / 100f + 0.5f, 0.2f, 1f), 0f);
					//transform.Rotate(0f, -giro, 0f);
				}
			}
			retroceso = false;
		}*/
		Vector3 velocidadNormal = Vector3.Dot( transform.right, rigidbody.velocity ) * transform.right;
		rigidbody.AddForce ( -velocidadNormal * friccion );
		//rigidbody.AddForce (new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z) * - friccion);
	}
}
