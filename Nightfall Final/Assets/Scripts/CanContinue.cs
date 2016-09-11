using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanContinue : MonoBehaviour {

    public GameManager gameManager;
    public Color inactiveColor;

    private Color normalColor;

    void Start() {
        normalColor = this.GetComponent<Image>().color;
    }
	
	void Update() {
        int save = gameManager.GetSave();
        if (save == 0) {
            this.GetComponent<Image>().color = inactiveColor;
            //this.GetComponentInChildren<Text>().color = inactiveColor;
            //this.GetComponent<Button>().interactable = false;
        } else {
            this.GetComponent<Image>().color = normalColor;
            //this.GetComponentInChildren<Text>().color = normalColor;
            //this.GetComponent<Button>().interactable = true;
        }
    }

}