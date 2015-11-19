#pragma strict

//this is the object we want to spawn.
var enemy:GameObject;
//this is how fast we want enemies to spawn.
var spawnRate:float = 6.0;
//this is the highest point we want the enemy to spawn in a random range.
var maxHeight:float = 12.0;
//this is the lowest point we want the enemy to spawn in a random range.
var minHeight:float = 0.0;
//this is how far ahead of the player we want enemies to spawn.
var spawnAhead:float = 16.0;
//this is the counter that records time so we can spawn enemies based on time.
private var counter:float = 0.0;
//this is a private variable that will find the player in function Start()
private var player:GameObject;

function Start () {
player = GameObject.Find("Player");
}

function Update () {
//this is so the object follows the player
transform.position.x = player.transform.position.x;
//here is where we keep track of time so we can spawn enemies based on time.
counter += Time.deltaTime;
//if the counter's number is higher than the spawnRate set, it will spawn an enemy
if(counter > spawnRate){
Instantiate(enemy, Vector3(transform.position.x+spawnAhead,Random.Range(minHeight,maxHeight),0), Quaternion.Euler(0,180,0));
//this resets the counter so spawning can repeat infinitely
counter = 0.0;
}
}