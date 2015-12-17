#pragma strict

//how fast the player walks
var walkSpeed:float = 14.0;
//how high the player can jump
var jumpHeight:float = 8.0;
//at what point the level resets if the player falls in a hole
var fallLimit:float = -10;
//jump sound
var jumpSound:AudioClip;

private var hit:RaycastHit;
//using this to ensure the jump sound doesn't play more than once at a time.
private var jumpCounter:float = 0.0;

function Update () {
//jumpCounter becomes a timer.
jumpCounter += Time.deltaTime;

#if UNITY_WEBPLAYER
//Keyboard Controls for web versions (Same as Standalone because they both deal with keyboard)
//This checks to see if the player is pressing A or D. This is connected to the else{} statement below
if(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right")){
//If the player presses A, add velocity to move left.
if(Input.GetKey("a") || Input.GetKey("left")){
	if(rigidbody.velocity.x > 0){
		rigidbody.velocity.x = 0;
	}
	if(rigidbody.velocity.x > -walkSpeed){
		rigidbody.velocity.x -= 48*Time.deltaTime;
	}
}
//if the player pressed D, add velocity to move right.
if(Input.GetKey("d")|| Input.GetKey("right")){
	if(rigidbody.velocity.x < 0){
		rigidbody.velocity.x = 0;
	}
	if(rigidbody.velocity.x < walkSpeed){
		rigidbody.velocity.x += 48*Time.deltaTime;
	}
}

}else{
//use else to do the opposite of an if() statement. this stops the player if lets go of A or D
rigidbody.velocity.x = 0.0;
}

//check to see if player is on terrain and can jump
if (Physics.Raycast (transform.position - Vector3(0,0.25,0), Vector3(0,-1,0), hit)) {
	if(hit.transform.tag == "terrain" && hit.distance < 0.74 && Input.GetKey("space")){
		rigidbody.velocity.y = jumpHeight;
		//once jump counter hits a quarter of a second, it can play the sound again.
		if(jumpCounter > 0.25){
			audio.PlayOneShot(jumpSound);
			jumpCounter = 0.0;
		}
	}
}
#endif

#if UNITY_STANDALONE
//Keyboard Controls for Mac, PC, and Linux builds. (Same as Webplayer because they both deal with keyboard)
//This checks to see if the player is pressing A or D. This is connected to the else{} statement below
if(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right")){
//If the player presses A, add velocity to move left.
if(Input.GetKey("a") || Input.GetKey("left")){
	if(GetComponent.<Rigidbody>().velocity.x > 0){
		GetComponent.<Rigidbody>().velocity.x = 0;
	}
	if(GetComponent.<Rigidbody>().velocity.x > -walkSpeed){
		GetComponent.<Rigidbody>().velocity.x -= 48*Time.deltaTime;
	}
}
//if the player pressed D, add velocity to move right.
if(Input.GetKey("d")|| Input.GetKey("right")){
	if(GetComponent.<Rigidbody>().velocity.x < 0){
		GetComponent.<Rigidbody>().velocity.x = 0;
	}
	if(GetComponent.<Rigidbody>().velocity.x < walkSpeed){
		GetComponent.<Rigidbody>().velocity.x += 48*Time.deltaTime;
	}
}

}else{
//use else to do the opposite of an if() statement. this stops the player if lets go of A or D
GetComponent.<Rigidbody>().velocity.x = 0.0;
}

//check to see if player is on terrain and can jump
if (Physics.Raycast (transform.position - Vector3(0,0.25,0), Vector3(0,-1,0), hit)) {
	if(hit.transform.tag == "terrain" && hit.distance < 0.74 && Input.GetKey("space")){
		GetComponent.<Rigidbody>().velocity.y = jumpHeight;
		//once jump counter hits a quarter of a second, it can play the sound again.
		if(jumpCounter > 0.25){
			GetComponent.<AudioSource>().PlayOneShot(jumpSound);
			jumpCounter = 0.0;
		}
	}
}
#endif

#if UNITY_IOS
//iOS Controls (same as Android because they both deal with screen touches)
if(Input.touchCount > 0){
for(var touch1 : Touch in Input.touches) {
	//if player presses less than 1/5 of the screen, go left.
	if(touch1.position.x < Screen.width/5 && touch1.position.y < Screen.height/3){
		if(rigidbody.velocity.x > 0){
			rigidbody.velocity.x = 0;
		}
		if(rigidbody.velocity.x > -walkSpeed){
			rigidbody.velocity.x -= 48*Time.deltaTime;
		}
	}
	//if player presses between 1/5 and 2/5 of the screen, go right.
	if(touch1.position.x > Screen.width/5 && touch1.position.x < Screen.width/5*2 && touch1.position.y < Screen.height/3){
		if(rigidbody.velocity.x < 0){
			rigidbody.velocity.x = 0;
		}
		if(rigidbody.velocity.x < walkSpeed){
			rigidbody.velocity.x += 48*Time.deltaTime;
		}
	}
	if(touch1.position.x > Screen.width/5*3 && touch1.position.y < Screen.height/3){
	
	}
}
}else{
rigidbody.velocity.x = 0.0;
}

if(Input.touchCount > 0){
for(var touch2 : Touch in Input.touches) { 
//2nd touch for jump button
	if(touch2.position.x > Screen.width/2 && touch2.position.y < Screen.height/3){
		if(Input.touchCount == 1){
			rigidbody.velocity.x = 0.0;
		}
		if (Physics.Raycast (transform.position - Vector3(0,0.25,0), Vector3(0,-1,0), hit)) {
			if(hit.transform.tag == "terrain" && hit.distance < 0.74){
				rigidbody.velocity.y = jumpHeight;
		//once jump counter hits a quarter of a second, it can play the sound again.
			if(jumpCounter > 0.25){
				audio.PlayOneShot(jumpSound);
				jumpCounter = 0.0;
			}
			}
		}
	}
}
}
#endif

#if UNITY_ANDROID
//iOS Controls (same as Android because they both deal with screen touches)
if(Input.touchCount > 0){
for(var touch1 : Touch in Input.touches) {
	//if player presses less than 1/5 of the screen, go left.
	if(touch1.position.x < Screen.width/5 && touch1.position.y < Screen.height/3){
		if(rigidbody.velocity.x > 0){
			rigidbody.velocity.x = 0;
		}
		if(rigidbody.velocity.x > -walkSpeed){
			rigidbody.velocity.x -= 48*Time.deltaTime;
		}
	}
	//if player presses between 1/5 and 2/5 of the screen, go right.
	if(touch1.position.x > Screen.width/5 && touch1.position.x < Screen.width/5*2 && touch1.position.y < Screen.height/3){
		if(rigidbody.velocity.x < 0){
			rigidbody.velocity.x = 0;
		}
		if(rigidbody.velocity.x < walkSpeed){
			rigidbody.velocity.x += 48*Time.deltaTime;
		}
	}
	if(touch1.position.x > Screen.width/5*3 && touch1.position.y < Screen.height/3){
	
	}
}
}else{
rigidbody.velocity.x = 0.0;
}

if(Input.touchCount > 0){
for(var touch2 : Touch in Input.touches) { 
//2nd touch for jump button
	if(touch2.position.x > Screen.width/2 && touch2.position.y < Screen.height/3){
		if(Input.touchCount == 1){
			rigidbody.velocity.x = 0.0;
		}
		if (Physics.Raycast (transform.position - Vector3(0,0.25,0), Vector3(0,-1,0), hit)) {
			if(hit.transform.tag == "terrain" && hit.distance < 0.74){
				rigidbody.velocity.y = jumpHeight;
		//once jump counter hits a quarter of a second, it can play the sound again.
			if(jumpCounter > 0.25){
				audio.PlayOneShot(jumpSound);
				jumpCounter = 0.0;
			}
			}
		}
	}
}
}
#endif

//reset level if player falls past Fall Limit
if(transform.position.y < fallLimit){
var lvlName:String = Application.loadedLevelName;
Application.LoadLevel(lvlName);
}

//end of function update
}