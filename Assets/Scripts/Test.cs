using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    enum States
    {
        None,
        Scan,
        ScanRSSI,
        Connect,
        RequestMTU,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }

    private States _state;
    private float _timeout;
    private string StatusMessage;
    private bool _rssiOnly = false;
    private string _deviceName;
    private string _deviceAdress = "A4:CF:12:9A:C8:5A";
    private string _serviceUID = "6E400001-B5A3-F393-E0A9-E50E24DCCA9E";
    private string _charaUID = "6E400003-B5A3-F393-E0A9-E50E24DCCA9E";

    private List<string> services = new List<string>();
    private List<string> chars = new List<string>();
    private bool mtu = false;
    private int errorCount = 0;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {

            SetState(States.Scan, 0.1f);
            StatusMessage = "Init OK";
            text.text = StatusMessage;
            BluetoothLEHardwareInterface.BluetoothConnectionPriority(BluetoothLEHardwareInterface.ConnectionPriority.High);

        }, (error) =>
        {
            errorCount++;
            StatusMessage = errorCount + " Error during initialize: " + error;
            text.text = StatusMessage;
            if (StatusMessage.Contains("Mtu") && mtu == false)
            {
                SetState(States.RequestMTU, 0.5f);
            }
        });
    }

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;

            if (_timeout <= 0f)
            {
                _timeout = 0;

                switch (_state)
                {
                    case States.None:
                        break;
                    case States.Scan:
                        StatusMessage = "Scanning";
                        text.text = StatusMessage;

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            // if your device does not advertise the rssi and manufacturer specific data
                            // then you must use this callback because the next callback only gets called
                            // if you have manufacturer specific data

                            if (!_rssiOnly)
                            {
                                if (address.Contains(_deviceAdress))
                                {
                                    StatusMessage = "1Found " + name;
                                    text.text = StatusMessage;
                                    BluetoothLEHardwareInterface.StopScan();

                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAdress = address;
                                    _deviceName = name;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        });

                        break;
                    case States.ScanRSSI:
                        break;
                    case States.Connect:
                        StatusMessage = "Connecting...";
                        text.text = StatusMessage;
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAdress, (deviceName) => {
                            StatusMessage = "Connected to " + deviceName;
                            text.text = StatusMessage;
                            StatusMessage = "";
                        }, null,
                        (deviceAdress, serviceUID, charaUID) => {
                            StatusMessage += "1 " + deviceAdress + System.Environment.NewLine + "2 " + serviceUID + System.Environment.NewLine + "3 " + charaUID + System.Environment.NewLine;
                            text.text = StatusMessage;
                            //_serviceUID = serviceUID;
                            //_charaUID = charaUID;
                            SetState(States.RequestMTU, 2f);
                        });
                        break;
                    case States.RequestMTU:
                        StatusMessage = "Requesting MTU";

                        BluetoothLEHardwareInterface.RequestMtu(_deviceAdress, 185, (address, newMTU) =>
                        {
                            StatusMessage = "MTU set to " + newMTU.ToString();
                            mtu = true;
                            SetState(States.Subscribe, 1f);
                        });
                        break;
                    case States.Subscribe:
                        StatusMessage = "Subscribing to characteristics...";
                        text.text = StatusMessage;
                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAdress, _serviceUID, _charaUID, null,
                        (address, characteristicUUID, bytes) =>
                        {
                            StatusMessage = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                            text.text = StatusMessage;
                        });
                        break;
                    case States.Unsubscribe:
                        break;
                    case States.Disconnect:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
