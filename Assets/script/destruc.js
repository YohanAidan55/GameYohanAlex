﻿#pragma strict

function Start () {
	
}

function Update () {
	
}


function OnTriggerEnter2D(other : Collider2D){
	if(other.gameObject.tag=="systeme"){
		Destroy(other.gameObject);
	}

}