using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BluetoothMenu : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text DebugText;
    public TMP_Text DataText;
    public GameObject ScanButton;
    public GameObject DisconnectButton;
    public GameObject BleDeviceInfoPrefab;
    public GameObject BleDeviceInfoGrid;
    public GameObject BleDeviceListView;

    [Header("BLE")]
    public string characterisitcUUID = "6E400003-B5A3-F393-E0A9-E50E24DCCA9E";

    private DataSourceBluetooth bleSource;
    private BLEStates currentState = BLEStates.None;
    private float timeout = 0f;
    private bool isScanning = false;
    private BleDevice currentDevice;

    void Awake()
    {
        bleSource = GameObject.FindObjectOfType<DataSourceBluetooth>();
    }

    private void BleSource_NewDataReceived(SkyArData data)
    {
        DebugText.text = "";
        DataText.text = data.ToString();
    }

    public void Start()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            DebugText.text = "Init OK";
            BluetoothLEHardwareInterface.BluetoothConnectionPriority(BluetoothLEHardwareInterface.ConnectionPriority.High);

        }, (error) =>
        {
            DebugText.text = "Error " + error;
        });
    }

    private void Update()
    {
        if (timeout > 0f)
        {
            timeout -= Time.deltaTime;

            if (timeout <= 0f)
            {
                timeout = 0f;

                switch (currentState)
                {
                    case BLEStates.None:
                        break;
                    case BLEStates.Scan:
                        DebugText.text = "Scanning...";
                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, ScannedDeviceFound);
                        break;
                    case BLEStates.Connect:
                        DebugText.text = "Connecting...";
                        BluetoothLEHardwareInterface.ConnectToPeripheral(currentDevice.Address, null, null, GetCharacteristicFromDevice, null);
                        break;
                    case BLEStates.Subscribe:
                        DebugText.text = "Subscribing...";
                        SubscribeToCharacteristic();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void ToggleScan()
    {
        isScanning = !isScanning;

        if (isScanning)
        {
            ClearScannedDevices();
            SetState(BLEStates.Scan, 0.5f);
            ScanButton.GetComponentInChildren<TMP_Text>().text = "Stop Scan";
        }
        else
        {
            BluetoothLEHardwareInterface.StopScan();
            ScanButton.GetComponentInChildren<TMP_Text>().text = "Scan";
            DebugText.text = "Scan stopped";
        }
    }
    private void ScannedDeviceFound(string address, string name)
    {
        if (BleDeviceInfoGrid != null && BleDeviceInfoPrefab != null)
        {
            BleDevice newDevice = new BleDevice(address, name);
            Instantiate(BleDeviceInfoPrefab, BleDeviceInfoGrid.transform).GetComponent<BleDeviceInfo>().SetBleDevice(newDevice);

            DebugText.text = "Found " + address;
        }
    }
    private void ClearScannedDevices()
    {
        currentDevice = null;
        if (BleDeviceInfoGrid != null)
        {
            foreach (Transform device in BleDeviceInfoGrid.transform)
            {
                Destroy(device.gameObject);
            }
        }
    }
    public void DeviceSelected(BleDevice device)
    {
        //Stop Scan
        if (isScanning)
            ToggleScan();

        currentDevice = device;
        SetState(BLEStates.Connect, 0.5f);

        BleDeviceListView.SetActive(false);
        ScanButton.SetActive(false);
        DisconnectButton.SetActive(true);
    }

    private void GetCharacteristicFromDevice(string adress, string serviceUid, string characteristicUid)
    {
        DebugText.text += serviceUid + " : " + characteristicUid + System.Environment.NewLine;
        if (currentDevice != null)
        {
            if (currentDevice.Characteristics.ContainsKey(serviceUid))
            {
                List<string> values;
                if (currentDevice.Characteristics.TryGetValue(serviceUid, out values))
                {
                    if (!values.Contains(characteristicUid))
                        values.Add(characteristicUid);
                }
            }
            else
            {
                currentDevice.Characteristics.Add(serviceUid, new List<string>() { characteristicUid });
            }
        }
        SetState(BLEStates.Subscribe, 2f);
    }
    private void SubscribeToCharacteristic()
    {
        if (currentDevice != null)
        {
            foreach (var service in currentDevice.Characteristics)
            {
                foreach (string characteristic in service.Value)
                {
                    if (characteristic.Contains(characterisitcUUID.ToLower()))
                    {
                        DebugText.text = "Subscribing... to " + service.Key + " : " + characteristic;
                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(currentDevice.Address, service.Key, characteristic, null, bleSource.DataSubscribe);
                        currentDevice.Subscibed = true;
                        bleSource.NewDataReceived += BleSource_NewDataReceived;
                    }
                }
            }
        }
    }

    private void UnsubscribeToAllCharacteristics()
    {
        if (currentDevice != null)
        {
            foreach (var service in currentDevice.Characteristics)
            {
                foreach (string characteristic in service.Value)
                {
                    if (characteristic.Contains(characterisitcUUID.ToLower()))
                    {
                        DebugText.text = "Unsubscribing... to " + service.Key + " : " + characteristic;
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(currentDevice.Address, service.Key, characteristic, null);
                    } 
                }
            }
        }
    }

    public void DisconnectFromDevice()
    {
        bleSource.NewDataReceived -= BleSource_NewDataReceived;
        BleDeviceListView.SetActive(true);
        ScanButton.SetActive(true);
        DisconnectButton.SetActive(false);
        DataText.text = "";
        if (currentDevice.Subscibed)
            UnsubscribeToAllCharacteristics();
        ClearScannedDevices();
        BluetoothLEHardwareInterface.DisconnectAll();
        DebugText.text = "Disconnected from device";
    }

    private void SetState(BLEStates newState, float newTimeout)
    {
        currentState = newState;
        timeout = newTimeout;
    }
}

public enum BLEStates
{
    None,
    Scan,
    Connect,
    RequestMTU,
    Subscribe,
    Unsubscribe,
    Disconnect,
}

public class BleDevice 
{
    public string Address { get; set; }
    public string Name { get; set; }
    public Dictionary<string,List<string>> Characteristics { get; set; }
    public bool Subscibed { get; set; }
    public BleDevice(string adr, string name)
    {
        Address = adr;
        Name = name;
        Characteristics = new Dictionary<string, List<string>>();
        Subscibed = false;
    }
}
