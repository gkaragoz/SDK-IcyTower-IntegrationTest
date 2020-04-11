using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Range(0, 1)]
    public float _progressValue;

    public Image _progressImage;

    public Text _progressState;

    public Text _progressValueText;

    // Update is called once per frame
    
    public void UpdateProgress(float value, string state, string valueText)
    {
        _progressImage.transform.localScale = new Vector3(_progressValue, 1, 1);

        _progressState.text = state;

        _progressValueText.text = "% " + valueText;
    }
}

