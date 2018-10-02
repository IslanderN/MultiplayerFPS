using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PressKeyToInteract : InteractionValidation
{

    public KeyCode Key;

    public override bool CanInteract(NetworkBehaviour player)
    {

        if (Input.GetKeyDown(Key))
        {

            float ValidationAtemptFrame = Time.frameCount;

            if(LastValidationAtemptFrame == 0f)
            {
                //Debug.Log("firstValidation");
                LastValidationAtemptFrame = ValidationAtemptFrame;
                return true;
            }
            else
            {
                if(LastValidationAtemptFrame == ValidationAtemptFrame)
                {
                    //Debug.Log("Cant validate more then ones at the same frame");
                    return false;
                }
                else
                {
                    //Debug.Log("Validation condition passed: diffrent frames");
                    LastValidationAtemptFrame = ValidationAtemptFrame;
                    return true;
                }
            }


            // if (Locked)
            // {
            //     Debug.Log("Validator is locked");
            //     return false;
            // }

            // Locked = true;
            // return true;
        }

        else return false;
    }
}


