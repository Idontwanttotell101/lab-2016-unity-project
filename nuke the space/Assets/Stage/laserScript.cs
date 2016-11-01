using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {
	public Vector3 startPoint;
	public Vector3 endPoint;
	LineRenderer laserLine;
	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer> ();
		laserLine.SetWidth (.2f, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		laserLine.SetPosition (0, startPoint);
		laserLine.SetPosition (1, endPoint);

	}
}
