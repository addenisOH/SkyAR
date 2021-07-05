using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetPropertyController : MonoBehaviour
{
    void OnEnable()
    {
        if (!MoverioCamera.IsActive())
        {
            return;
        }

        var cameraProperty = MoverioCamera.GetProperty();
        if (cameraProperty == null)
        {
            return;
        }

        SetBrightness(cameraProperty.Brightness);
        SetBrightnessMin(cameraProperty.BrightnessMin);
        SetBrightnessMax(cameraProperty.BrightnessMax);
        SetWhiteBalanceMode(cameraProperty.WhiteBalanceMode);
        SetExposureMode(cameraProperty.ExposureMode);
        SetExposureStep(cameraProperty.ExposureStep);
        SetExposureStepMin(cameraProperty.ExposureStepMin);
        SetExposureStepMax(cameraProperty.ExposureStepMax);
        SetCaptureSize(cameraProperty.CaptureSize);
        SetPowerLineFrequencyControlMode(cameraProperty.PowerLineFrequencyControlMode);
        SetCaptureFps(cameraProperty.CaptureFps);
    }

    private void SetBrightness(int value)
    {
        var valueDropdownObject = GameObject.Find("BrightnessValue");
        var brightnessValue = valueDropdownObject.GetComponent<Dropdown>();
        brightnessValue.value = brightnessValue.options.FindIndex(e => e.text == value.ToString());
    }

    private void SetBrightnessMin(int value)
    {
        var textToDisplayObject = GameObject.Find("BrightnessMinValue");
        var brightnessMinValue = textToDisplayObject.GetComponent<Text>();
        brightnessMinValue.text = value.ToString();
    }

    private void SetBrightnessMax(int value)
    {
        var textToDisplayObject = GameObject.Find("BrightnessMaxValue");
        var brightnessMaxValue = textToDisplayObject.GetComponent<Text>();
        brightnessMaxValue.text = value.ToString();
    }

    private void SetWhiteBalanceMode(WhiteBalanceMode mode)
    {
        var modeString = Enum.GetName(typeof(WhiteBalanceMode), mode);
        var valueDropdownObject = GameObject.Find("WhiteBalanceModeValue");
        var whiteBalanceModeValue = valueDropdownObject.GetComponent<Dropdown>();
        whiteBalanceModeValue.value = whiteBalanceModeValue.options.FindIndex(e => e.text == modeString);
    }

    private void SetExposureMode(ExposureMode mode)
    {
        var modeString = Enum.GetName(typeof(ExposureMode), mode);
        var valueDropdownObject = GameObject.Find("ExposureModeValue");
        var exposureModeValue = valueDropdownObject.GetComponent<Dropdown>();
        exposureModeValue.value = exposureModeValue.options.FindIndex(e => e.text == modeString);
    }

    private void SetExposureStep(int value)
    {
        var valueDropdownObject = GameObject.Find("ExposureStepValue");
        var exposureStepValue = valueDropdownObject.GetComponent<Dropdown>();
        exposureStepValue.value = exposureStepValue.options.FindIndex(e => e.text == value.ToString());
    }

    private void SetExposureStepMin(int value)
    {
        var textToDisplayObject = GameObject.Find("ExposureStepMinValue");
        var exposureStepMinValue = textToDisplayObject.GetComponent<Text>();
        exposureStepMinValue.text = value.ToString();
    }

    private void SetExposureStepMax(int value)
    {
        var textToDisplayObject = GameObject.Find("ExposureStepMaxValue");
        var exposureStepMaxValue = textToDisplayObject.GetComponent<Text>();
        exposureStepMaxValue.text = value.ToString();
    }

    private void SetCaptureSize(CaptureSize size)
    {
        var sizeString = Enum.GetName(typeof(CaptureSize), size);
        var valueDropdownObject = GameObject.Find("CaptureSizeValue");
        var captureSizeValue = valueDropdownObject.GetComponent<Dropdown>();
        captureSizeValue.value = captureSizeValue.options.FindIndex(e => e.text == sizeString);
    }

    private void SetPowerLineFrequencyControlMode(PowerLineFrequencyControlMode mode)
    {
        var modeString = Enum.GetName(typeof(PowerLineFrequencyControlMode), mode);
        var valueDropdownObject = GameObject.Find("PowerLineFrequencyControlModeValue");
        var powerLineFrequencyControlModeValue = valueDropdownObject.GetComponent<Dropdown>();
        powerLineFrequencyControlModeValue.value = powerLineFrequencyControlModeValue.options.FindIndex(e => e.text == modeString);
    }

    private void SetCaptureFps(CaptureFps mode)
    {
        var modeString = Enum.GetName(typeof(CaptureFps), mode);
        var valueDropdownObject = GameObject.Find("CaptureFpsValue");
        var captureFpsValue = valueDropdownObject.GetComponent<Dropdown>();
        captureFpsValue.value = captureFpsValue.options.FindIndex(e => e.text == modeString);
    }
}
