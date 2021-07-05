using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BleDeviceInfo : MonoBehaviour
{
    public TMP_Text NameText;
    public TMP_Text AddressText;

    private BleDevice device;
    public void SetBleDevice(BleDevice _device)
    {
        device = _device;
        NameText.text = device.Name;
        AddressText.text = device.Address;
    }

    public void OnClicked()
    {
        GameObject.FindObjectOfType<BluetoothMenu>()?.DeviceSelected(device);
    }
}
