#pragma strict


function Update () {

if (Input.GetKey("1"))
    { 
    GetComponent.<Animation>().Play("out-snap-open");
    }

if (Input.GetKey("2"))
    {
    GetComponent.<Animation>().Play("out-slam-shut");
    }

if (Input.GetKey("3"))
    {
    GetComponent.<Animation>().Play("out-open-slowly");
    } 

if (Input.GetKey("4"))
    {
    GetComponent.<Animation>().Play("out-close");
    }

if (Input.GetKey("5"))
    {
    GetComponent.<Animation>().Play("in-snap-open");
    }

if (Input.GetKey("6"))
    {
    GetComponent.<Animation>().Play("in-slam-shut");
    }

if (Input.GetKey("7"))
    {
    GetComponent.<Animation>().Play("in-open-slowly");
    }        

if (Input.GetKey("8"))
    {
    GetComponent.<Animation>().Play("in-close");
    }

}