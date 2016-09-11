using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public PlaySoundOnce sound;
    public GameObject player;
    public GUIStyle style;
    public string type;
    public string description;
    public float duration = 3.0F;
    public Rect rekt;

    private bool collided = false;
    private float Duration = 0.0F;
    private float yDisp = 0.0F;

    void Start () {

    }
	
	void Update () {
        Vector3 position = Camera.main.WorldToScreenPoint(player.transform.position);
        if (collided) {
            Duration += Time.deltaTime;
            if (Duration <= 1.0F) {
                yDisp = Duration * 25.0F;
                style.normal.textColor = new Color(style.normal.textColor.r, style.normal.textColor.g,
                style.normal.textColor.b, Duration);
                rekt.position = new Vector2(rekt.position.x, rekt.position.y - (1.0F - Duration));
            }

            if (Duration > duration) {
                gameObject.SetActive(false);
            }
        } else if (gameObject.tag.Equals("Collected")) {
            if (sound != null) {
                sound.PlayOnce();
            }
            if (gameObject.GetComponent<SpriteRenderer>() != null) {
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
            style.normal.textColor = new Color(style.normal.textColor.r, style.normal.textColor.g,
                style.normal.textColor.b, 0.0F);
            gameObject.tag = "Collectable";
            rekt.position = new Vector2(position.x - player.GetComponent<SpriteRenderer>().sprite.rect.width / 2.0F, position.y);
            collided = true;    
        }
    }

    void OnGUI() {
        if (collided) {
            GUI.Label(rekt, description, style);
        }
    }

    public string GetCollectableType() {
        return type;
    }

}