using UnityEngine;
using System.Collections;
//Example Script for motion (Walk, jump and dying), for dying press 'k'...
public class Example_Motion_Attack_Controller : MonoBehaviour {
	private float maxspeed; //walk speed
	Animator anim;
	private bool faceright; //face side of sprite activated
	private bool jumping=false;
	private bool isdead=false;
	//public bool run=false;
	//private bool attack=false;
	//private string aux="";
	public GameObject bot_arc;
	public GameObject top_arc;
	public GameObject normal_arc;
	public GameObject area;
	private int actual_layer;
	private int d_cont=0;
	public bool hitted = false;
    private bool twinkled = false;
	private bool is_healing =false;
	//--Arc colors
	public Sprite basic;//basic arc image
	public Sprite green;//green arc image	
	public Sprite pink;//pink arc image
	//--
	//About progress-bar --
	private float aux_d=0;//Initial lifebar width
	public GameObject Lifebar;
	public GameObject Lifebar_group;
	private Vector3 initial_localscale;
	//private float Lifebar_distance_y=0;
	//private float Lifebar_back_distance_y=0;
	//--

	void Start () {
		//Lifebar_distance_y = this.transform.position.y-Lifebar.transform.position.y;
		aux_d = Lifebar.GetComponent<Renderer>().bounds.size.x;//lifebar width
		initial_localscale = new Vector3 (Lifebar.transform.localScale.x,Lifebar.transform.localScale.y,Lifebar.transform.localScale.z);
		//--
		actual_layer=0;
		maxspeed=2f;//Set walk speed
		faceright=true;//Default right side
		anim = this.gameObject.GetComponent<Animator> ();
		Set_Layer();
		anim.SetBool ("walk", false);//Walking animation is deactivated
		anim.SetBool ("down", false);//Dying animation is deactivated
		anim.SetBool ("up", false);//Jumping animation is deactivated
		anim.SetBool ("down_attack", false);
		anim.SetBool ("up_attack", false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="Mucus_Area"){hitted=true;}
	}

	void OnCollisionEnter2D(Collision2D coll) {

	}
	
	void FixedUpdate(){
		if((is_healing==false)&&(hitted==false)&&(isdead==false)){
			is_healing=true;
			Invoke("Healing_" , 8f);//Frequency healing
		}
		if (hitted==true){
			if((d_cont<8)&&(twinkled==false)){
				d_cont++;
				twinkled=true;
				Invoke("Twinkle_",0.1f);
			}
			if(d_cont==8){
				if((d_cont<8)&&(twinkled==false)){
					d_cont++;
					twinkled=true;
					Invoke("Twinkle_",0.1f);
				}
				if(d_cont==8){
					Life_Down();//Getting Damage --> in this case 1/4 of total life
					d_cont=0;
					hitted=false;
				}
			}
		}
	}

	void Twinkle_(){
		SpriteRenderer pj_renderer = this.gameObject.GetComponent<SpriteRenderer>();
		pj_renderer.enabled=!pj_renderer.enabled;
		twinkled=false;
	}

	void Update () {
		//--WALKING
		float movey = Input.GetAxis ("Vertical");
		float move = Input.GetAxis ("Horizontal");
		if(isdead==true){
			movey=0;
			move=0;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxspeed/2, movey * maxspeed/2);
		//--
		//---
		if(isdead==true){
			anim.SetBool("dead",true);
			this.gameObject.GetComponent<Collider2D>().enabled=false;
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
			isdead=true;
		}
		if(isdead==false){
			anim.SetBool("attack" , false);
		}
		//---
		//-- Attack animation off
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Down_Attack")) {
			Invoke("activate_arc_down",0.1f);
		} else {
			anim.SetBool ("down_attack", false);
			bot_arc.SetActive(false);
			area.GetComponent<Collider2D>().enabled=false;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Up_Attack")) {
			Invoke("activate_arc_up",0.1f);
		} else {
			anim.SetBool ("up_attack", false);
			top_arc.SetActive(false);
			area.GetComponent<Collider2D>().enabled=false;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Normal_Attack")) {
			Invoke("activate_arc_normal",0.2f);
		} else {
			anim.SetBool ("attack", false);
			normal_arc.SetActive(false);
			area.GetComponent<Collider2D>().enabled=false;
		}
		//--
		//Debug.Log ("+---- " + aux);

		//--
		if(isdead==false){
			if(Input.GetKeyUp ("5")){
				hitted=true;
			}
			//--Gun
			if(Input.GetKeyUp ("1")){
				actual_layer=0;
				Set_Layer();
			}
			if(Input.GetKeyUp ("2")){
				actual_layer=1;
				Set_Layer();
			}
			if(Input.GetKeyUp ("3")){
				actual_layer=2;
				Set_Layer();
			}
			//
			//--DYING
			if(Input.GetKey ("k")){//###########Change the dead event, for example: life bar=0
				anim.SetBool ("dead", true);
				isdead=true;
			}
            //--END DYING

            //--JUMPING
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            {
				if ((anim.GetCurrentAnimatorStateInfo (0).IsName ("Down"))||(anim.GetCurrentAnimatorStateInfo (0).IsName ("Stop_Down"))) {
					anim.SetBool ("down_attack", true);
					area.GetComponent<Collider2D>().enabled=true;
				}
				if ((anim.GetCurrentAnimatorStateInfo (0).IsName ("Up"))||(anim.GetCurrentAnimatorStateInfo (0).IsName ("Stop_Up"))) {
					anim.SetBool ("up_attack", true);
					area.GetComponent<Collider2D>().enabled=true;
				}
				if ((anim.GetCurrentAnimatorStateInfo (0).IsName ("Walking"))||(anim.GetCurrentAnimatorStateInfo (0).IsName ("Normal_Stop"))) {
					anim.SetBool ("attack", true);
					area.GetComponent<Collider2D>().enabled=true;
				}
			}
			if (Input.GetButtonDown("Jump")){
				if(jumping==false){//only once time each jump
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,200));
					jumping=true;
					anim.SetBool ("jump", true);
				}
			}
			//--END JUMPING
			


			if(move>0){//Go right
				anim.SetBool ("walk", true);//Walking animation is activated
				if(faceright==false){
					Flip ();
				}
			}
			if(move==0){//Stop
				anim.SetBool ("walk", false);
			}			
			if((move<0)){//Go left
				anim.SetBool ("walk", true);
				if(faceright==true){
					Flip ();
				}
			}

			if(movey>0){//Go right
				anim.SetBool ("up", true);//Walking animation is activated
				anim.SetBool ("down", false);
			}
			if(movey==0){//Stop
				anim.SetBool ("up", false);
				anim.SetBool ("down", false);
			}			
			if((movey<0)){//Go left
				anim.SetBool ("up", false);
				anim.SetBool ("down", true);

			}
			//END WALKING
		}
		Lifebar_Dead();
	}

	void Set_Layer(){
		switch(actual_layer){
		case 0:
			Set_Arc_Color(basic);
			anim.SetLayerWeight (0, 1f);
			anim.SetLayerWeight (1, 0f);
			anim.SetLayerWeight (2, 0f);
			break;
		case 1:
			Set_Arc_Color(green);
			anim.SetLayerWeight (1, 1f);
			anim.SetLayerWeight (0, 0f);
			anim.SetLayerWeight (2, 0f);
			break;
		case 2:
			Set_Arc_Color(pink);
			anim.SetLayerWeight (2, 1f);
			anim.SetLayerWeight (1, 0f);
			anim.SetLayerWeight (0, 0f);
			break;
		}
	}
	void activate_arc_down(){
		bot_arc.SetActive(true);
	}
	void activate_arc_up(){
		top_arc.SetActive(true);
	}
	void activate_arc_normal(){
		normal_arc.SetActive(true);
	}
	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//--
		theScale = Lifebar_group.transform.localScale;
		theScale.x *= -1;
		Lifebar_group.transform.localScale = theScale;
	}
	void Set_Arc_Color(Sprite auxsprite){
		SpriteRenderer top = top_arc.GetComponent<SpriteRenderer>();
		SpriteRenderer normal = normal_arc.GetComponent<SpriteRenderer>();
		SpriteRenderer bot = bot_arc.GetComponent<SpriteRenderer>();
		top.sprite = auxsprite;
		normal.sprite = auxsprite;
		bot.sprite = auxsprite;
	}

	//--About_Lifebar--
	void Lifebar_Dead(){
		if(isdead==false){
			if((Lifebar.GetComponent<Renderer>().bounds.size.x<=0)||(Lifebar.transform.localScale.x<=0)){
				anim.SetBool("dead",true);
				this.gameObject.GetComponent<Collider2D>().enabled=false;
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
				isdead=true;
				Destroy(Lifebar_group);
			}
		}
	}
	void Life_Down(){
		float aux_c = Lifebar.GetComponent<Renderer>().bounds.size.x;
		Lifebar.transform.localScale += new Vector3(-aux_d/4, 0, 0);// 1/4 part
		Vector3 auxve = new Vector3(Lifebar.transform.position.x,Lifebar.transform.position.y,Lifebar.transform.position.z);
		auxve.x=auxve.x-(aux_c - Lifebar.GetComponent<Renderer>().bounds.size.x)/2;
		Lifebar.transform.position=auxve;
	}

	void Healing_(){
		if(isdead==false){
			if(Lifebar.transform.localScale.x>0){
				float aux_c = Lifebar.GetComponent<Renderer>().bounds.size.x;
				if(Lifebar.transform.localScale.x +aux_d/8 >= initial_localscale.x){
				//Debug.Log(initial_localscale + " - " + Lifebar.transform.localScale.x +aux_d/4);
					Lifebar.transform.localScale = new Vector3(initial_localscale.x, initial_localscale.y, initial_localscale.z);
				}else{
					Lifebar.transform.localScale += new Vector3(+aux_d/8, 0, 0);// 1/4 part
				}
				Vector3 auxve = new Vector3(Lifebar.transform.position.x,Lifebar.transform.position.y,Lifebar.transform.position.z);
				auxve.x=auxve.x-(aux_c - Lifebar.GetComponent<Renderer>().bounds.size.x)/2;
				Lifebar.transform.position=auxve;
			}
		}
		is_healing=false;
	}

	//--
}
