using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ballshot : MonoBehaviour
{
	public float power = 1.0f;
	public float sensitivity = 0.2f;
	public float life = 2.0f, cooldown = 1.0f;
	public float dead_sens = 25f;
	
	public int dots = 20;
	public float interval = 1/30.0f;
	
	private Vector3 startPos;	
	private bool shot = false, aiming = false, hit_ground = false;	
	
	public GameObject shadow, shad;
	private GameObject Dots;
	private List<GameObject> ind;
	private scoreScript scoreScript;
	
	// Use this for initialization
	void Start (){
		shadow = (GameObject) Instantiate(shadow);
		scoreScript = GameObject.Find("scoreScript").GetComponent<scoreScript>();
		power = GameObject.Find("gameScript").GetComponent<gameScript>().power ? 2.5f : 2f;		
		
		dots = dots - GameObject.Find("gameScript").GetComponent<gameScript>().round;
		if(dots < 5)
			dots = 5;
		Dots = GameObject.Find("dots");
		transform.GetComponent<Rigidbody2D>().isKinematic = true;
		transform.GetComponent<Collider2D>().enabled = false;
		startPos = transform.position;
		ind = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
		for(int i = 0; i<dots; i++){
			ind[i].GetComponent<Renderer>().enabled = false;
		}
	}

	// Update is called once per frame
	
	void Update (){
		shadow.transform.position = new Vector3(gameObject.transform.position.x, shadow.transform.position.y, gameObject.transform.position.z);
		
		Aim();
		if(hit_ground){
			if(life<=0){
				Destroy(gameObject);
				Destroy(shadow);
				scoreScript.DestroyedBall(gameObject);
			}
			else if(life>0){
				life -= Time.deltaTime;
				Color startColor = GetComponent<Renderer>().material.GetColor("_Color");				
				GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, life));
				shadow.GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, life));
			}
		}
	}
	
	//Collisions
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Floor"){
			hit_ground = true;		
		}
	}
	
	
	void Aim(){
		if(shot) return;
		if(Input.GetAxis("Fire1") == 1){
			if(!aiming){
				aiming = true;
				startPos = Input.mousePosition;
				CalculatePath();
				ShowPath();
			}
			else{
				CalculatePath();
			}		
		}
		else if(aiming && !shot){
			if(inDeadZone(Input.mousePosition) || inReleaseZone(Input.mousePosition)){
				aiming = false;
				HidePath();
				return;
			}
			transform.GetComponent<Rigidbody2D>().isKinematic =  false;			
			transform.GetComponent<Collider2D>().enabled = true;
			transform.GetComponent<Rigidbody2D>().AddForce(GetForce(Input.mousePosition));		
			shot = true;
			aiming = false;
			scoreScript.Shot();
			HidePath();
		}
	}
	
	Vector2 GetForce(Vector3 mouse){
		return (new Vector2(startPos.x, startPos.y)- new Vector2(mouse.x, mouse.y))*power;
		
	}
	
	bool inDeadZone(Vector2 mouse){
		if(Mathf.Abs(startPos.x - mouse.x) <= dead_sens && Mathf.Abs(startPos.y - mouse.y) <= dead_sens)
			 return true;
		return false;
	}
	bool inReleaseZone(Vector2 mouse){
		if(mouse.x <= 70)
			return true;
			
		return false;
	}
	
	//
	/*  Path and dots */
	//
	//Calculate path
	void CalculatePath(){
		Vector2 vel = GetForce(Input.mousePosition) * Time.fixedDeltaTime / GetComponent<Rigidbody2D>().mass;
		for(int i = 0; i < dots; i++){			
			ind[i].GetComponent<Renderer>().enabled = true;
			float t = i/30f;
			Vector3 point = PathPoint(transform.position, vel, t);
			point.z = -1.0f;
			ind[i].transform.position = point;
		} 
	}
	
	//Get point position
	Vector2 PathPoint(Vector2 startP, Vector2 startVel, float t){
		return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
	}
	
	//Hide all used dots
	void HidePath(){
		for(int i = 0; i<dots; i++)	ind[i].GetComponent<Renderer>().enabled = false;
	}
	
	//Show all used dots
	void ShowPath(){
		for(int i = 0; i<dots; i++) ind[i].GetComponent<Renderer>().enabled = true;
	}
}
