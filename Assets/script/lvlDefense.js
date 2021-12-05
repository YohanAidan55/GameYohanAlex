#pragma strict


var lvl : int;
var chanceExit : float;

var fantome : GameObject;
var fantomNoir : GameObject;


function Start () {
     lvl = 0;	
}

function Update () {
	 
}


function transformNoir(a : int, b : float){
     if(((a == 1)&&(b < 0.25))||((a == 2)&&(b < 0.50))||((a == 3)&&(b < 0.75))||((a == 4))){   //en fonction du nombre de defense détruite, la chance de se transformer
        Instantiate(fantomNoir, fantome.transform.position, fantome.transform.rotation);
        Destroy(fantome);
        fantome = null;
     }
}


function OnTriggerEnter2D(other : Collider2D){    //si le fantome entre en contact avec la sorti du panier alors transformation en fonciton de des défenses
       if(other.gameObject.tag == "fantome"){
            chanceExit = Random.value;
            fantome = other.gameObject; //save fantome qui doit sortir
            if(GameObject.Find("touch").GetComponent(touchFantome).lach == true){
           		 transformNoir(lvl, chanceExit);
            }
       }
}