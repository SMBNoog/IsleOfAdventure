using UnityEngine;
using System.Collections;

public class Example_Simple_Enemy_Controller : MonoBehaviour {
	private float maxspeed; //walk speed
	Animator anim;
	private bool faceright;
	private bool isdead=false;
	private int d_cont=0;
	public bool hitted = false;
	public GameObject mucus_area;
	private bool twinkled = false;
	private float speed;
	private bool near=false;
	private bool objetive=false;
	private GameObject pj_;
	//--
	public bool attack=false;
	public bool is_attacking=false;
	//About progress-bar --
	private float aux_d=0;
	public GameObject Lifebar;
	public GameObject Lifebar_group;
	//--
	//--
	// Use this for initialization
	void Start () {
		aux_d = Lifebar.GetComponent<Renderer>().bounds.size.x;//lifebar width
		//--
		speed=1f;
		faceright=true;//Default right side
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool("walk",false);
		anim.SetBool("down_attack",false);
		anim.SetBool("normal_attack",false);
		anim.SetBool("up_attack",false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="Area"){hitted=true;}
		if(other.name=="Activation_Area"){
			objetive=true;
			pj_=other.gameObject;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		near=true;
		attack=true;
	}

	void OnCollisionExit2D(Collision2D coll) {
		attack=false;
		is_attacking=false;
		Invoke ("Go_",2f);
	}

	void FixedUpdate(){
		if((attack==true)&&(is_attacking==false)){
			is_attacking=true;
			mucus_area.GetComponent<Collider2D>().enabled=true;
			Invoke("Attack_",0.5f);
		}
		if (hitted==true){
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

	// Update is called once per frame
	void Update () {
		if(objetive==true){
			float step = speed * Time.deltaTime;
			anim.SetBool("walk",true);
			if((near==true)){
				step=0;
				anim.SetBool("walk",false);
			}
			if((pj_.transform.position.x < this.gameObject.transform.position.x)&&(faceright==true)){
				Flip();
			}else{
				if((pj_.transform.position.x > this.gameObject.transform.position.x)&&(faceright==false)){
					Flip();
				}
			}
			transform.position = Vector3.MoveTowards(transform.position, pj_.transform.position, step);
		}
		//--
		Lifebar_Dead();
	}
	//--About Attack
	void Attack_(){
		mucus_area.GetComponent<Collider2D>().enabled=false;
		Invoke("Attack_Delay",2.5f);
	}
	void Attack_Delay(){
		is_attacking=false;
	}
	void Twinkle_(){
		SpriteRenderer pj_renderer = this.gameObject.GetComponent<SpriteRenderer>();
		pj_renderer.enabled=!pj_renderer.enabled;
		twinkled=false;
	}
	void Go_(){
		near=false;
	}
	//--End About Attack

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
	//--About_Lifebar--
	void Lifebar_Dead(){
		if((Lifebar.GetComponent<Renderer>().bounds.size.x<=0)||(Lifebar.transform.localScale.x<=0)){
			this.gameObject.GetComponent<Collider2D>().enabled=false;
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic=true;
			isdead=true;
			Destroy(this.gameObject);
		}
	}
	void Life_Down(){
		float aux_c = Lifebar.GetComponent<Renderer>().bounds.size.x;
		Lifebar.transform.localScale += new Vector3(-aux_d/4, 0, 0);// 1/4 part
		Vector3 auxve = new Vector3(Lifebar.transform.position.x,Lifebar.transform.position.y,Lifebar.transform.position.z);
		auxve.x=auxve.x-(aux_c - Lifebar.GetComponent<Renderer>().bounds.size.x)/2;
		Lifebar.transform.position=auxve;
	}
	//--
}
