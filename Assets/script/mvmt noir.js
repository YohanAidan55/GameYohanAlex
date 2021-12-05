#pragma strict
var n : int ;
function Start () {
	
}

function Update () {

    	n += 1;
	if(n > 30){
    	this.gameObject.GetComponent.<Rigidbody2D>().velocity = Vector2(Random.Range(-4, 2), Random.Range(-1, 3)*Time.timeScale);
	    n = 0;
	    }
}
