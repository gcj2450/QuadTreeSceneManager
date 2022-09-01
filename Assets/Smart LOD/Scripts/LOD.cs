using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LOD : MonoBehaviour {
	
	//Camera for distance calculation
	public Transform camera;
	//The current LOD group
	public int currentgroup;
	
	//LOD RENDER GROUPS
	public List<GameObject> LodGroup0;
	public List<GameObject> LodGroup1;
	public List<GameObject> LodGroup2;
	public List<GameObject> LodGroup3;
	public List<GameObject> LodGroup4;
	public List<GameObject> LodGroup5;
	
	//LOD INSTANTIATE GROUPS
	public GameObject LodinstGroup0;
	public GameObject LodinstGroup1;
	public GameObject LodinstGroup2;
	public GameObject LodinstGroup3;
	public GameObject LodinstGroup4;
	public GameObject LodinstGroup5;
	
	//DISTANCES FOR SWITCHING
	public float Lod1Distance=20;
	public float Lod2Distance=40;
	public float Lod3Distance=60;
	public float Lod4Distance=80;
	public float Lod5Distance=100;
	
	//THE CURRENT DISTANCE
	private float distance;
	//DISTANCE DETECTION BOOL
	public bool EnableDistanceDetection=true;
	
	
	//WILL INSTANTIATE ALL GROUPS AT START
	public bool InstantiateObjectsAtStart;
	//WILL DESTROY INSTANTIATED ONCE FAR ENOUGH
	public bool DestroyInstantiated;
	//THE DISTANCE ALL THE INSTANTIATED WILL DESTROY
	public float destroydistance;
	public List<GameObject> Instantiatelist;
	
	//BOOLS FOR INSTANTIATING
	private bool inst0;
	private bool inst1;
	private bool inst2;
	private bool inst3;
	private bool inst4;
	private bool inst5;
	
	//SAVES LAST GROUP
	private int groupsave;
	//OBJECTS TO BE RENDERED OR NOT
	public List<GameObject> visible;
	public List<GameObject> notvisible;
	

	
	// Use this for initialization
	void Start () {
		//MAKE GROUP SAVE -1 SO IT ENSURES THE LOD WILL UPDATE AT START
		groupsave=-1;
		
		
		//INSTANTIATES ARE TRUE AT START
		inst0=true;
		inst1=true;
		inst2=true;
		inst3=true;
		inst4=true;
		inst5=true;
		
			
//INSTANTIATE AT START 
		if(InstantiateObjectsAtStart){
			if(LodinstGroup0){
		GameObject newguy=(GameObject)Instantiate (LodinstGroup0.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.lossyScale;
				LodGroup0.Add(newguy);
						inst0=false;
			}
		if(LodinstGroup1){
		GameObject newguy=(GameObject)Instantiate (LodinstGroup1.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.lossyScale;
				LodGroup1.Add(newguy);
						inst1=false;
			}	
		if(LodinstGroup2){
		GameObject newguy=(GameObject)Instantiate (LodinstGroup2.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.lossyScale;
				LodGroup2.Add(newguy);
						inst2=false;
			}
			if(LodinstGroup3){
		GameObject newguy=(GameObject)Instantiate (LodinstGroup3.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.lossyScale;
				LodGroup3.Add(newguy);
						inst3=false;
			}
		if(LodinstGroup4){
		GameObject newguy=(GameObject)Instantiate (LodinstGroup4.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.lossyScale;
				LodGroup4.Add(newguy);
						inst4=false;
			}
		if(LodinstGroup5){
		GameObject newguy=(GameObject)Instantiate (LodinstGroup5.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.lossyScale;
				LodGroup5.Add(newguy);
						inst5=false;
			}
		
			
		}
		
		
		//GET THE CURRENT GROUP RIGHT AT START
		if(camera){
			if(EnableDistanceDetection){
		distance=Vector3.Distance(transform.position, camera.position);
		if(distance<Lod1Distance)currentgroup=0;
		if(distance>Lod1Distance&distance<Lod2Distance&distance<Lod3Distance&distance<Lod4Distance&distance<Lod5Distance)currentgroup=1;
		if(distance>Lod2Distance&distance<Lod3Distance&distance<Lod4Distance&distance<Lod5Distance)currentgroup=2;
		if(distance>Lod3Distance&distance<Lod4Distance&distance<Lod5Distance)currentgroup=3;
		if(distance>Lod4Distance&distance<Lod5Distance)currentgroup=4;
		if(distance>Lod5Distance)currentgroup=5;
			}
			
		}
		else{
			Debug.Log("No Camera Transform Assigned!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//DISTANCE DETECTION AND LOD GROUP SWITCHING
		if(camera){
		if(EnableDistanceDetection){
		distance=Vector3.Distance(transform.position, camera.position);
		if(distance<Lod1Distance)currentgroup=0;
		if(distance>Lod1Distance&distance<Lod2Distance&distance<Lod3Distance&distance<Lod4Distance&distance<Lod5Distance)currentgroup=1;
		if(distance>Lod2Distance&distance<Lod3Distance&distance<Lod4Distance&distance<Lod5Distance)currentgroup=2;
		if(distance>Lod3Distance&distance<Lod4Distance&distance<Lod5Distance)currentgroup=3;
		if(distance>Lod4Distance&distance<Lod5Distance)currentgroup=4;
		if(distance>Lod5Distance)currentgroup=5;
			}
		}
		
		
		//DESTROY INSTANTIATED
			if(DestroyInstantiated){
			
			if(distance>=destroydistance){
				int l=Instantiatelist.Count;
			for (int k = 0; k < l; k++){
					
				Destroy(Instantiatelist[k].gameObject);
				LodGroup0.Remove(Instantiatelist[k]);
				LodGroup1.Remove(Instantiatelist[k]);
				LodGroup2.Remove(Instantiatelist[k]);
				LodGroup3.Remove(Instantiatelist[k]);
				LodGroup4.Remove(Instantiatelist[k]);
				LodGroup5.Remove(Instantiatelist[k]);		
				}
				inst0=true;
				inst1=true;
				inst2=true;
				inst3=true;
				inst4=true;
				inst5=true;
				
					Instantiatelist.Clear();
			}
			
		}
		
		
		
		////CLEARS VISIBILITY LISTS IF THE GROUP CHANGES
		if(groupsave!=currentgroup){
			notvisible.Clear();
				visible.Clear();
			
		if(currentgroup!=0){
			int l=LodGroup0.Count;
			for (int k = 0; k < l; k++){
			
			notvisible.Add(LodGroup0[k]);
					
			}
			}	
			else{
				//AND LET THE GROUP SWITCHING BEGIN!
				
				//EACH TOP SECTION IS FOR INSTANTIATING
					if(inst0&LodinstGroup0){
	GameObject newguy=(GameObject)Instantiate (LodinstGroup0.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.localScale;
				Instantiatelist.Add(newguy);
				LodGroup0.Add(newguy);
						inst0=false;
					}
				
				int l=LodGroup0.Count;
			for (int k = 0; k < l; k++){
					
		    visible.Add(LodGroup0[k]);	
					
			}
			}
			
			if(currentgroup!=1){
			int l=LodGroup1.Count;
			for (int k = 0; k < l; k++){
					
			notvisible.Add(LodGroup1[k]);
					
			}
			}	
			else{
				
				if(inst1&LodinstGroup1){
	GameObject newguy=(GameObject)Instantiate (LodinstGroup1.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.localScale;
				Instantiatelist.Add(newguy);
				LodGroup1.Add(newguy);
						inst1=false;
					}
					int l=LodGroup1.Count;
			for (int k = 0; k < l; k++){
		    visible.Add(LodGroup1[k]);
					
			}
			}
			
			if(currentgroup!=2){
			int l=LodGroup2.Count;
			for (int k = 0; k < l; k++){
					
			notvisible.Add(LodGroup2[k]);
					
			}
			}	
			else{
				
				if(inst2&LodinstGroup2){
	GameObject newguy=(GameObject)Instantiate (LodinstGroup2.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.localScale;
				Instantiatelist.Add(newguy);
				LodGroup2.Add(newguy);
						inst2=false;
					}
					int l=LodGroup2.Count;
			for (int k = 0; k < l; k++){
		    visible.Add(LodGroup2[k]);
					
			}
			}
			
			
			
			if(currentgroup!=3){
			int l=LodGroup3.Count;
			for (int k = 0; k < l; k++){
					
			notvisible.Add(LodGroup3[k]);
					
			}
			}	
			else{
				
				if(inst3&LodinstGroup3){
	GameObject newguy=(GameObject)Instantiate (LodinstGroup3.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.localScale;
				Instantiatelist.Add(newguy);
				LodGroup3.Add(newguy);
						inst3=false;
					}
					int l=LodGroup3.Count;
			for (int k = 0; k < l; k++){
		    visible.Add(LodGroup3[k]);
					
			}
			}
			
			
			if(currentgroup!=4){
			int l=LodGroup4.Count;
			for (int k = 0; k < l; k++){
					
			notvisible.Add(LodGroup4[k]);
					
			}
			}	
			else{
				
				if(inst4&LodinstGroup4){
	GameObject newguy=(GameObject)Instantiate (LodinstGroup4.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.localScale;
				Instantiatelist.Add(newguy);
				LodGroup4.Add(newguy);
						inst4=false;
					}
					int l=LodGroup4.Count;
			for (int k = 0; k < l; k++){
		    visible.Add(LodGroup4[k]);
					
			}
			}
			
			
			if(currentgroup!=5){
			int l=LodGroup5.Count;
			for (int k = 0; k < l; k++){
					
			notvisible.Add(LodGroup5[k]);
					
			}
			}	
			else{
				
				if(inst5&LodinstGroup5){
	GameObject newguy=(GameObject)Instantiate (LodinstGroup5.gameObject, transform.position, transform.rotation);	
				newguy.transform.localScale=transform.localScale;
				Instantiatelist.Add(newguy);
				LodGroup5.Add(newguy);
						inst5=false;
					}
					int l=LodGroup5.Count;
			for (int k = 0; k < l; k++){
		    visible.Add(LodGroup5[k]);
					
			}
			}
			
			
			//NOW LETS DISABLE THE RENDERERS!
		if(visible.Count>0){	
		int ls=visible.Count;
			for (int i = 0; i < ls; i++){
		Renderer[] childs =visible[i].GetComponentsInChildren<Renderer>();
				foreach(Renderer child in childs)child.enabled = true;				
		}
		}
		

		if(notvisible.Count>0){
		int lf=notvisible.Count;
			for (int k = 0; k < lf; k++){
		Renderer[] childs=notvisible[k].GetComponentsInChildren<Renderer>();
		foreach(Renderer child in childs)child.enabled = false;	
		}	
		}	
			}
			
			
		//UPDATE GROUP SAVE
			groupsave=currentgroup;
		
		

	}
	
}
