using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIMinion : MonoBehaviour
{
    public Slider healthSlider3D;
    // Start is called before the first frame update
    public void Start3DSlider(float maxValue)
    {
        healthSlider3D.maxValue = maxValue;
        healthSlider3D.value = maxValue;
    }

    // Update is called once per frame
    public void Update3DSlider(float value)
    {
        healthSlider3D.value = value;
    }
}
