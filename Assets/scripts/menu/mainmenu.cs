﻿using UnityEngine;
using System.Collections;

public class mainmenu : MonoBehaviour {
	float width = 168f, height = 48f, Yoffset = 38f;
	float xcenter = 316f, ycenter = 216f, cooldown = 3f;
	
	string username, tempname = "Insert name";
	bool options = false, shop = false, saves = false, name_select = false, top10 = false;
	
	string[] score_names = new string[10];	
	double[] score_points = new double[10];	
	public GUISkin newSkin;
	
	void Start(){	
		if(!GameObject.Find("ChartBoostManager").GetComponent<CBads>().shown)
			GameObject.Find("ChartBoostManager").GetComponent<CBads>().ShowAd();

		username = GameObject.Find("gameScript").GetComponent<gameScript>().username;
		if(username == "player"){
			name_select = true;
		}
		GameObject.Find("gameScript").GetComponent<leaderboard>().GetLeaderBoard();
	}
	void Update(){
		if(cooldown > 0) cooldown -= Time.deltaTime;
		else if(!GameObject.Find("ChartBoostManager").GetComponent<CBads>().shown)			
			GameObject.Find("ChartBoostManager").GetComponent<CBads>().ShowAd();
		else
			cooldown = 3f;
	}

	// Use this for initialization
	void OnGUI(){
		AutoResize(800, 480);
		
		GUI.skin = newSkin;
		GUI.skin.button.fontSize = 12;
		if(GUI.Button (new Rect (300, 5, 50, 30), "CASH")){}
		GUI.Box(new Rect(350 ,5, 90, 30), "x"+ GameObject.Find ("gameScript").GetComponent<gameScript> ().cash);


		GUI.skin.button.fontSize = 24;
		if(name_select)
			NewName();
		else if(saves)
			Saves();
		else if(shop)
			Shop();
		else if(options)
			Options();
		else if(top10)
			Top10();
		else
			StartMenu();
		
	}
	void NewName(){
		tempname = GUI.TextField(new Rect(xcenter, ycenter, width, height), tempname, 12);
		
		if(GUI.Button(new Rect(xcenter, ycenter+1*(height/2+Yoffset), width, height), "Done")){
			if(tempname != "Insert name" && tempname != "player"){
				PlayerPrefs.SetString("name", tempname);
				PlayerPrefs.Save();
				username = GameObject.Find("gameScript").GetComponent<gameScript>().username = tempname;
				name_select = false;
			}			
		
		}
	}	
	
	void StartMenu(){
		
//		if(GUI.Button(new Rect(xcenter-width/1.5f, ycenter, width/2, height), "Multi")){
//			GameObject.Find("gameScript").GetComponent<gameScript>().MultiRestart();
//			AutoFade.LoadLevel(2,0.5f,0.5f,Color.black);
//		}
		
//		if(GUI.Button(new Rect(xcenter+width*1.2f, ycenter, width/2, height), "Top10")){
//			top10 = true;			
//		}
		
		if(GUI.Button(new Rect(xcenter, ycenter, width, height), "Play")){
			GameObject.Find("gameScript").GetComponent<gameScript>().Restart();
			AutoFade.LoadLevel(2,0.5f,0.5f,Color.black);
		}
		
		if(GUI.Button(new Rect(xcenter, ycenter+1*(height/2+Yoffset), 168, height), "Shop")){
			shop = true;
		}

		if(GUI.Button(new Rect(xcenter, ycenter+2*(height/2+Yoffset), 168, height), "Options")){
			options = true;
		}

		if(GUI.Button(new Rect(xcenter, ycenter+3*(height/2+Yoffset), width, height), "Exit") || Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		GUI.skin.label.fontSize = 18;
//		string Copyright = "\u00A9";
//		GUI.Label(new Rect(0, 450, 500, 40), "Game by jujine");
//		GUI.Label(new Rect(500, 450, 500, 60), "v1.0.0  "+Copyright+"  https://jujinesy.github.io/blog");
	
	}

	/// <summary>
	/// ////////////////////////////////////////////////////////
	/// </summary>
	void Shop(){
		GUI.Label(new Rect(0, 122, 500, 70), "100원당  1  CASH 입니다. \n게임 중 CASH를 누르면 공갯수가 올라갑니다.");


		if(GUI.Button(new Rect(xcenter-width/0.7f, ycenter, width/2, height), "Back")){
			shop = false;
		}


		if(GUI.Button(new Rect(xcenter-width*0.5f, ycenter, width, height), "100원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 1;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071653", "1", "1");
		}

		if(GUI.Button(new Rect(xcenter-width*0.5f, ycenter+1*(height/2+Yoffset), width, height), "1000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 10;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071655", "10", "10");
		}

		if(GUI.Button(new Rect(xcenter-width*0.5f, ycenter+2*(height/2+Yoffset), width, height), "10000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 100;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071657", "100", "100");
		}

		if(GUI.Button(new Rect(xcenter-width*0.5f, ycenter+3*(height/2+Yoffset), width, height), "100000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 1000;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071659", "1000", "1000");
		}


			
		if(GUI.Button(new Rect(xcenter+width*0.6f, ycenter, width, height), "500원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 5;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071654", "5", "5");
		}

		if(GUI.Button(new Rect(xcenter+width*0.6f, ycenter+1*(height/2+Yoffset), width, height), "5000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 50;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071656", "50", "50");
		}

		if(GUI.Button(new Rect(xcenter+width*0.6f, ycenter+2*(height/2+Yoffset), width, height), "50000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 500;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071658", "500", "500");
		}

		if(GUI.Button(new Rect(xcenter+width*0.6f, ycenter+3*(height/2+Yoffset), width, height), "300000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 3000;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071660", "3000", "3000");
		}



		if(GUI.Button(new Rect(xcenter+width*1.7f, ycenter, width, height), "3000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 30;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071661", "30", "30");		
		}

		if(GUI.Button(new Rect(xcenter+width*1.7f, ycenter+1*(height/2+Yoffset), width, height), "8000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 80;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071662", "80", "80");
		}

		if(GUI.Button(new Rect(xcenter+width*1.7f, ycenter+2*(height/2+Yoffset), width, height), "110000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 1100;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071663", "1100", "1100");		
		}

		if(GUI.Button(new Rect(xcenter+width*1.7f, ycenter+3*(height/2+Yoffset), width, height), "220000원")){
			GameObject.Find("gameScript").GetComponent<IapSample> ().Pay = 2200;
			GameObject.Find("gameScript").GetComponent<IapSample>().RequestPaymenet("OA00712220", "0910071664", "2200", "2200");
		}
	}


	void Options(){
		string sfx = GameObject.Find("gameScript").GetComponent<gameScript>().sfx ? "On" : "Off";
		string power = GameObject.Find("gameScript").GetComponent<gameScript>().power ? "x1.5" : "x1";
		
		if(GUI.Button(new Rect(xcenter-width/1.5f, ycenter, width/2, height), "Back")){
			if(GameObject.Find("gameScript").GetComponent<gameScript>().sfx) PlayerPrefs.SetInt("sfx", 1);
			else PlayerPrefs.SetInt("sfx", 0);
			
			
			if(GameObject.Find("gameScript").GetComponent<gameScript>().power) PlayerPrefs.SetInt("power", 1);
			else  PlayerPrefs.SetInt("power", 0);	
			
			PlayerPrefs.Save();
			
			options = false;
		}
		
		
		if(GUI.Button(new Rect(xcenter, ycenter, width, height), "Sound: "+sfx)){
			if(GameObject.Find("gameScript").GetComponent<gameScript>().sfx)
				GameObject.Find("gameScript").GetComponent<gameScript>().sfx = false;
			else
				GameObject.Find("gameScript").GetComponent<gameScript>().sfx = true;
		}
		
		if(GUI.Button(new Rect(xcenter, ycenter+1*(height/2+Yoffset), width, height), "Power: "+power)){
			if(GameObject.Find("gameScript").GetComponent<gameScript>().power)
				GameObject.Find("gameScript").GetComponent<gameScript>().power = false;
			else
				GameObject.Find("gameScript").GetComponent<gameScript>().power = true;		
		}

		
		if(GUI.Button(new Rect(xcenter, ycenter+2*(height/2+Yoffset), width, height), "Manage saves")){
			saves = true;
		}
			
		
	}
	void Saves(){		
		float lwidth = 200f;		
		GUI.skin.button.padding.left = 10;
		int achv = GameObject.Find("gameScript").GetComponent<aManager>().GetDone();
		int record = GameObject.Find("gameScript").GetComponent<gameScript>().record;
		
		if(GUI.Button(new Rect(xcenter-width/1.5f, ycenter, width/2, height), "Back")){
			saves = false;
		}
		
		GUI.skin.button.alignment = TextAnchor.MiddleLeft;
		GUI.Button(new Rect(xcenter, ycenter, lwidth, height), "Achiments: "+achv+"/21");		
		GUI.Button(new Rect(xcenter, ycenter+1*(height/2+Yoffset), lwidth, height), "Record: "+record);
		
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;
		if(GUI.Button(new Rect(xcenter, ycenter+2*(height/2+Yoffset), lwidth, height), "Reset")){
			GameObject.Find("gameScript").GetComponent<gameScript>().ResetSave();
			name_select = true;
		}
		
		
	}
	
	void Top10(){	
		if(score_points[0] == 0){
			score_names = GameObject.Find("gameScript").GetComponent<gameScript>().score_names;
			score_points = GameObject.Find("gameScript").GetComponent<gameScript>().score_points;
		}
		
		GUI.skin.button.padding.left = 10;
		
		if(GUI.Button(new Rect(150, ycenter, width/2, height), "Back")){
			top10 = false;
		}
		
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		GUI.Box(new Rect(xcenter-80, ycenter-15, 400, 270), "");	
		GUI.skin.label.fontSize = 24;

		GUI.Label(new Rect(320, ycenter-15, 150, 40), "Name");
		GUI.Label(new Rect(460, ycenter-15, 200, 40), "Points");
		
		GUI.skin.label.fontSize = 18;
		
		for(int i=1; i<=10; i++){
			if(score_points[i-1] == 0) break;
			GUI.Label(new Rect(250, ycenter+22*i, 60, 30), "#"+i);
			GUI.Label(new Rect(320, ycenter+22*i, 150, 30), score_names[i-1]);
			GUI.Label(new Rect(460, ycenter+22*i, 200, 30), ""+score_points[i-1]);
		}
		
		
		
	}
	
	static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}
