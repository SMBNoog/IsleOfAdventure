#pragma strict

//This is a special script that automatically rescales GUIText and GUITextures so they aren't stretched.
//No variables need to be changed in this to make it work. Just drag and drop it onto any GUIText or GUITexture.

private var getTxt:Component;
private var getTxtr:Component;
private var resX:float;
private var resY:float;
private var origResX:float;
private var origResY:float;
private var txtrX:float;
private var txtrY:float;
private var txtX:float;
private var txtY:float;

function Start () {
getTxt = transform.GetComponent(GUIText);
getTxtr = transform.GetComponent(GUITexture);
if(getTxtr == null && getTxt == null){
	print("No GUIText or GUITexture exists on: " + transform.gameObject.name);
}

}

function Update () {

if(Screen.width != origResX || Screen.height != origResY){

	origResX = Screen.width;
	origResY = Screen.height;
	
	if(getTxt != null){
		resX = Screen.width;
		resY = Screen.height;
		txtX = transform.localScale.x;
		txtY = transform.localScale.y;
		transform.localScale.y = (transform.localScale.x*(resX/resY));
	}
	if(getTxtr != null){
		resX = Screen.width;
		resY = Screen.height;
		txtrX = transform.GetComponent.<GUITexture>().texture.width;
		txtrY = transform.GetComponent.<GUITexture>().texture.height;
		transform.localScale.y = (transform.localScale.x*(resX/resY))/(txtrX/txtrY);
	}
}

}