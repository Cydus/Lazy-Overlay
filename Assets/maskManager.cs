using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maskManager : MonoBehaviour {

    public GameObject[] masks;
    public float maskScale = 11.2f;
    public Material maskMat;
    public bool isFlipped = false;
    public int selectedMaskID = 0;

    private GameObject selectedMask;

    //private Vector3 rotatedAroundY = new Vector3 (0.0f, 180.0f, 0.0f);
    Quaternion flippedRot = Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f));
    Quaternion normalRot = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

    // ui controls
    public BoxSlider boxSlider;
    public Slider hueSlider;
    public Slider maskScaleSLider;
    public Slider maskAlphaSLider;
    public Dropdown selectedMaskDropDown;
    public Toggle flipToggle;

    // Use this for initialization
    void Start () {

        Debug.Log("mask manager started");
        loadPrefs();
        // initlaise UI
        maskAlphaSLider.value = maskMat.GetColor("_Color").a;

        //float hue, sat, value;
        //Color.RGBToHSV(maskMat.GetColor("_Color"), out hue, out sat, out value);
        //Debug.Log("hue " + hue);
       //boxSlider.value = .5f; boxSlider.valueY = .5f; hueSlider.value = 128f;

        maskScaleSLider.value = maskScale;
        selectedMaskDropDown.value = selectedMaskID;
        flipToggle.isOn = isFlipped;

        
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < masks.Length; i++)
        {
            if (i == selectedMaskID)
            {
                masks[i].SetActive(true);
            }
            else
            {
                masks[i].SetActive(false);
            }
        }
        selectedMask = masks[selectedMaskID];

        if (isFlipped)
        {
            selectedMask.transform.localScale = new Vector3(-maskScale, -maskScale, -maskScale);
            selectedMask.transform.localRotation = flippedRot;

        } else
        {
            selectedMask.transform.localScale = new Vector3(maskScale, maskScale, maskScale);
            selectedMask.transform.localRotation = normalRot;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            maskScale += .1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            maskScale -= .1f;
            maskScale = (maskScale < 1.8f) ? 1.8f : maskScale;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlipped = (isFlipped == true) ? false : true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            selectedMaskID = (selectedMaskID == 0) ? 1 : 0;
        }
    }

    void OnApplicationQuit()
    {
        CustomPlayerPrefs.SetBool("isMaskFlipped", isFlipped, true);
        CustomPlayerPrefs.SetFloat("maskScale", maskScale, true);
        CustomPlayerPrefs.SetInt("selectedMaskID", selectedMaskID, true);

        CustomPlayerPrefs.SetFloat("maskAlpha", maskMat.GetColor("_Color").a, true);
        CustomPlayerPrefs.SetFloat("maskColor_R", maskMat.GetColor("_Color").r, true);
        CustomPlayerPrefs.SetFloat("maskColor_G", maskMat.GetColor("_Color").g, true);
        CustomPlayerPrefs.SetFloat("maskColor_B", maskMat.GetColor("_Color").b, true);
    }

    void loadPrefs()
    {
        if (CustomPlayerPrefs.HasKey("selectedMaskID"))
        {
            selectedMaskID = CustomPlayerPrefs.GetInt("selectedMaskID");
            Debug.Log("prefered selectedMaskID detected");
        }
        if (CustomPlayerPrefs.HasKey("isMaskFlipped")) {
            isFlipped = CustomPlayerPrefs.GetBool("isMaskFlipped");
            Debug.Log("prefered isMaskFlipped detected");
        }
        if (CustomPlayerPrefs.HasKey("maskScale"))
        {
            maskScale = CustomPlayerPrefs.GetFloat("maskScale");
            Debug.Log("prefered maskScale detected");
        } else
        {
            maskScale = 11.2f;
        }
        if (CustomPlayerPrefs.HasKey("maskColor_R"))
        {
            float red = CustomPlayerPrefs.GetFloat("maskColor_R");
            float green = CustomPlayerPrefs.GetFloat("maskColor_G");
            float blue = CustomPlayerPrefs.GetFloat("maskColor_B");

            maskMat.SetColor("_Color", new Color(red,green,blue, maskMat.GetColor("_Color").a));
        }
        if (CustomPlayerPrefs.HasKey("maskAlpha"))
        {
            float alpha = CustomPlayerPrefs.GetFloat("maskAlpha");
            maskMat.SetColor("_Color", new Color(maskMat.GetColor("_Color").r, maskMat.GetColor("_Color").g, maskMat.GetColor("_Color").b, alpha));
        }
    }

    public void setMaskScale(float value)
    {
        maskScale = value;
    }

    public void setSetSelectedMask(int maskID)
    {
        selectedMaskID = maskID;
        if (selectedMaskID == 1)
        {
            maskScaleSLider.enabled = false;
        }
        else
        {
            maskScaleSLider.enabled = true;
        }
    }

    public void setFlipped(bool flipped)
    {
        isFlipped = flipped;
    }

    public void changeAlpha(float value)
    {
        Color col = new Color(maskMat.color.r, maskMat.color.g, maskMat.color.b, value);
        maskMat.SetColor("_Color", col);
    }

    public void SetColor(Color value)
    {
        Color col = new Color(value.r, value.g, value.b, maskMat.GetColor("_Color").a);
        maskMat.SetColor("_Color", col);
    }

    public void gotToWebsite()
    {
        Application.OpenURL("https://www.reddit.com/r/LazyVR/");
    }
}
