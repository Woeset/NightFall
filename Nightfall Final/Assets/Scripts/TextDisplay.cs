using UnityEngine;
using System.Collections;

public class TextDisplay : MonoBehaviour {

    public string text = "";
    public int size = 14;
    public Color color = Color.white;

    void Start() {

    }

    void Update() {
        
    }

    void OnDrawGizmos() {
        print(color);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(this.transform.position, 0.5F);
    }

    void OnGUI() {
        print(color);
        GUI.contentColor = Color.white;
        Vector3 getPixelPos = Camera.main.WorldToScreenPoint(this.transform.position);
        getPixelPos.y = Screen.height - getPixelPos.y;
        GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, 300.0F, 100.0F), text);
    }

}