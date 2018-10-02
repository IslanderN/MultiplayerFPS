// using UnityEngine;
// using UnityEngine.Networking;

// public class PlayerTriggerReaction : MonoBehaviour
// {
//     [SerializeField]
//     private PlayerSetup playerSetup;
//     [SerializeField]
//     private PlayerInteraction playerExecution;

//     private GameObject pickupField;
//     private bool isFieldEmpty = true;

//     void Start()
//     {
//         Setup();
//     }
    
//     private void Setup()
//     {
//         if (playerSetup != null)
//         {
//             pickupField = playerSetup.pickUpFieldInstance;
//             isFieldEmpty = false;
//         }
//     }


//     public void OnEnter()
//     {
//         if(pickupField != null)
//         {
//             pickupField.SetActive(true);
//         }
// 		Debug.Log("On Enter");
//     }  

//     public void OnExit()
//     {
//         if (pickupField != null)
//         {
//             pickupField.SetActive(false);
//         }
//     }

//     public void OnButton(GameObject obj)
//     {
//         if (pickupField != null)
//         {
//             pickupField.SetActive(false);
//         }

//         Debug.Log(obj.name + " iterract");

//         NetworkTransport networkTransport = obj.GetComponent<NetworkTransport>();
//         if (networkTransport != null)
//         {
//             playerExecution.InteractWithTransport(networkTransport);
//         }
//         else
//         {

//             playerExecution.PickUpObject(obj);
//         }
//     }
// }
