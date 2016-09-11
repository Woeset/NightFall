using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILight : MonoBehaviour {

    public GameManager gameManager;
    public float viewDistance = 4F;

    private GameObject[] canvasContents;
    private GameObject[] canvasSliders;

    void Start() {
        canvasContents = GameObject.FindGameObjectsWithTag("Canvas");
        canvasSliders = GameObject.FindGameObjectsWithTag("Slider");
    }

    void Update() {
        for (int i = 0; i < canvasContents.Length; i++) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 contentPosition = Camera.main.ScreenToWorldPoint(canvasContents[i].transform.position);
            Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
            Vector2 contentPos = new Vector2(contentPosition.x, contentPosition.y);
            float dist = Vector2.Distance(mousePos, contentPos);
            float alpha = 1 - (dist / viewDistance);
            alpha *= gameManager.Darkness;
            if (dist <= viewDistance) {
                if (canvasContents[i].gameObject.name.Equals("Checkmark") && canvasContents[i].transform.parent.parent.gameObject.GetComponent<Toggle>().isOn) {
                    canvasContents[i].GetComponent<CanvasRenderer>().SetAlpha(alpha);
                } else if (canvasContents[i].gameObject.name.Equals("Checkmark")) {
                    canvasContents[i].GetComponent<CanvasRenderer>().SetAlpha(0F);
                } else {
                    canvasContents[i].GetComponent<CanvasRenderer>().SetAlpha(alpha);
                }
            } else {
                canvasContents[i].GetComponent<CanvasRenderer>().SetAlpha(0F);
            }
        }
        for (int i = 0; i < canvasSliders.Length; i++) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 contentPosition = Camera.main.ScreenToWorldPoint(canvasSliders[i].transform.position);
            Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);
            Vector2 contentPos = new Vector2(contentPosition.x, contentPosition.y);
            float dist = Vector2.Distance(mousePos, contentPos);
            if (dist < viewDistance * 1.5F) {
                float alpha = 1 - (dist / (viewDistance * 1.5F));
                alpha *= gameManager.Darkness;
                Image[] childImageScripts = canvasSliders[i].GetComponentsInChildren<Image>();
                for (int j = 0; j < childImageScripts.Length; j++) {
                    childImageScripts[j].color = new Color(childImageScripts[j].color.r, childImageScripts[j].color.g, childImageScripts[j].color.b, alpha);
                }
            } else {
                float alpha = 0.0F;
                Image[] childImageScripts = canvasSliders[i].GetComponentsInChildren<Image>();
                for (int j = 0; j < childImageScripts.Length; j++) {
                    childImageScripts[j].color = new Color(childImageScripts[j].color.r, childImageScripts[j].color.g, childImageScripts[j].color.b, alpha);
                }
            }
        }
    }

}