#pragma strict


//the enemy will be animated with just 2 different images.
var texture1:Texture;
var texture2:Texture;
//here is a public variable that can be changed so the birds can have a higher or lower random speed.
var minSpeed:float = 4.0;
var maxSpeed:float = 12.0;
//frameSpeed allows us to change the speed of the animation, in this case it switches between 2 textures to make the enemy look like it flap its wings.
var frameSpeed:float = 4;
//this is the x position where the bird notices it went too far and destroys itself.
var destroyPoint:float = -10.0;

//this is the counter so we can animate the bird.
private var counter:float = 0.0;
//this is an empty float variable that we'll fill within the random range determined by minSpeed and maxSpeed.
private var randSpeed:float;

function Start () {
randSpeed = Random.Range(minSpeed,maxSpeed);
//make bird travel left at random speed set in function Start()
GetComponent.<Rigidbody>().velocity.x = -randSpeed;
}

function Update () {
//counter for animation set to change based on Frame Speed chosen
counter += Time.deltaTime*frameSpeed;

//animate the enemy with 2 frames, then allow it to start over again by resetting the counter back to 0 after it animates with both frames.
if(counter > 0 && GetComponent.<Renderer>().material.mainTexture != texture1){
GetComponent.<Renderer>().material.mainTexture = texture1;
}
if(counter > 1 && GetComponent.<Renderer>().material.mainTexture != texture2){
GetComponent.<Renderer>().material.mainTexture = texture2;
}
if(counter > 2){
counter = 0.0;
}

//destroy the enemy if it goes too far left
if(transform.position.x < destroyPoint){
Destroy(gameObject);
}

}

//this is where the bird becomes lethal. if it collides with the player, it will reset the level.
function OnTriggerEnter (other : Collider){
if(other.tag == "Player"){
var lvlName:String = Application.loadedLevelName;
Application.LoadLevel(lvlName);
}
}