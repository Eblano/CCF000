using UnityEngine;
using System.Collections;

public class NewMinimapObj : MonoBehaviour 
{
    public Transform _camera;

    private float timer = 0;
    public bool Flashing = false;
    private Material mainMaterial;
    private Color tempCol = new Color(1, 1, 1, 1);
    private bool reverseAlpha = false;

	void Start () 
    {
        if (!_camera) _camera = GameObject.Find("MinimapCamera").transform;
        mainMaterial = GetComponent<MeshRenderer>().material;
        tempCol = mainMaterial.GetColor("_Color");
	}
	
	void Update () 
    {
        if (_camera)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, _camera.eulerAngles.y + 180, transform.eulerAngles.z);
        }
        if(Flashing)
        {
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                reverseAlpha = !reverseAlpha;
                timer = 0;
            }
            if(!reverseAlpha)
            {
                if (tempCol.a>0.1f)
                    tempCol.a -= 0.02f;
            }
            else
            {
                tempCol.a += 0.02f;
            }
            GetComponent<MeshRenderer>().material.SetColor("_Color", tempCol);
        }
	}
}
