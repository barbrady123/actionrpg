using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{
    private BoxCollider2D _areaBox;

    // Start is called before the first frame update
    void Start()
    {
        _areaBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        Camera.main.GetComponent<CameraController>().AreaBox = _areaBox;
    }
}
