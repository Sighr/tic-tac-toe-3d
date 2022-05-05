using System;
using UnityEngine;

public class ChangeCellDimension : MonoBehaviour
{
    public IntVariable gridDimension;

    public void OnValueChanged(string value)
    {
        gridDimension.value = Convert.ToInt32(value);
    }
}
