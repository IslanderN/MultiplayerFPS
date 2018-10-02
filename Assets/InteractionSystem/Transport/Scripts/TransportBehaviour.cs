using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Candlelight;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.Networking;

public enum NetworkTransportState { empty, controlled }

public class TransportBehaviour : NetworkBehaviour {

	 [SerializeField]
    private Camera carCam;

    private NetworkIdentity networkIdentety;

    private CarController carController;

	private CarUserControl carUserControl;

	private CarAudio carAudio;

	// [SerializeField]
	// private CarExit carExit;

	// [HideInInspector]
	[SyncVar]
	public NetworkIdentity Driver;

	public Transform DriverExitPoint;

	[SyncVar]
    [SerializeField, Candlelight.PropertyBackingField]
    private NetworkTransportState m_State;
    public NetworkTransportState State
    {
        get { return m_State; }
        set
        {
            m_State = value;
            UpdateState();
        }
    }

	[Space]
	[SyncVar]
	public string TempText;

    void Start()
    {
        networkIdentety = GetComponent<NetworkIdentity>();
        carController = GetComponent<CarController>();
		carUserControl = GetComponent<CarUserControl>();
		carAudio = GetComponent<CarAudio>();


    }

	private void Update()
	{
		if(State == NetworkTransportState.controlled)
		{
			if(Driver != null)
			{
				if (Input.GetKeyDown(KeyCode.F))
				{
					State = NetworkTransportState.empty;
					Driver.transform.position = DriverExitPoint.position;
					Driver.transform.rotation = DriverExitPoint.rotation;
					Driver.gameObject.SetActive(true);
					Driver = null;
					TempText = "Empty";
				}
					
			}
		}
	}

	private void UpdateState()
    {

		Debug.Log("update state");
        switch (m_State)
        {
            case NetworkTransportState.empty:
                {
                    carController.enabled = false;
					carUserControl.enabled = false;
					// carAudio.enabled = false;
                    carCam.enabled = false;
					carCam.tag = GameManager.UNTAGGED_CAM_TAG;

					// if (Driver != null)
					// 	Driver.GetOutFromTransport();
					//carExit.enabled = false;
					//GetComponent<CarSetup>().SetActiveAsPlayer(false);
					break;
                }
            case NetworkTransportState.controlled:
                {
                    networkIdentety.localPlayerAuthority = true;
                    carController.enabled = true;
					carUserControl.enabled = true;
					// carAudio.enabled = true;
					carCam.enabled = true;
					carCam.tag = GameManager.MAIN_CAM_TAG;

					if (Driver != null)
					{
						
					}
					
					// carExit.enabled = true;

					//GetComponent<CarSetup>().SetActiveAsPlayer(true);
					break;
                }
        }


    }
}
