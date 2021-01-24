using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    public GameObject Player;
    private Vector3 offset;
    private PlayerControler PC;

    // Use this for initialization
    void Start () {
        PC = Player.GetComponent<PlayerControler>();
        offset = transform.position - Player.transform.position;

	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(PC.gameOverL == false && PC.gameOverW == false)
        {
            transform.position = Player.transform.position + offset;
        }
        else
        {
            transform.position = transform.position;
        }
    }
}
