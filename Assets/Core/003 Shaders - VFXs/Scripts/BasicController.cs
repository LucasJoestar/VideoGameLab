using UnityEngine;

public class BasicController : MonoBehaviour
{
    [SerializeField]
    GameObject primaryWeaponPrefab;
    [SerializeField]
    GameObject secondaryWeaponPrefab;
    [SerializeField]
    GameObject primaryFirePoint;
    [SerializeField]
    GameObject secondaryFirePoint;

    GameObject spawnedPrimary;
    GameObject spawnedSecondary;    


    void DisablePrimary()
    {
        spawnedPrimary.SetActive(false);
    }

    void EnablePrimary()
    {
        spawnedPrimary.SetActive(true);
    }


    void UpdatePrimary()
    {
        if (primaryFirePoint != null)
        {
            spawnedPrimary.transform.position = primaryFirePoint.transform.position;
        }
    }





    void DisableSecondary()
    {
        spawnedSecondary.SetActive(false);
    }

    void EnableSecondary()
    {
        spawnedSecondary.SetActive(true);
    }
    

    void UpdateSecondary()
    {
        if(secondaryFirePoint != null)
        {
            spawnedSecondary.transform.position = secondaryFirePoint.transform.position;
        }
    }



    void Start()
    {
        spawnedSecondary = Instantiate(secondaryWeaponPrefab, secondaryFirePoint.transform) as GameObject;
        spawnedPrimary = Instantiate(primaryWeaponPrefab, primaryFirePoint.transform) as GameObject;
        DisablePrimary();
        DisableSecondary();
    }

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.oKey.wasPressedThisFrame)
        {
            EnablePrimary();
        }
        if (UnityEngine.InputSystem.Keyboard.current.oKey.isPressed)
        {
            UpdatePrimary();
        }
        if (UnityEngine.InputSystem.Keyboard.current.oKey.wasReleasedThisFrame)
        {
            DisablePrimary();
        }
        if (UnityEngine.InputSystem.Keyboard.current.pKey.wasPressedThisFrame)
        {
            EnableSecondary();
        }
        if(UnityEngine.InputSystem.Keyboard.current.pKey.isPressed)
        {
            UpdateSecondary();
        }
        if (UnityEngine.InputSystem.Keyboard.current.pKey.wasReleasedThisFrame)
        {
            DisableSecondary();
        }
        if (UnityEngine.InputSystem.Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * Time.deltaTime * 10;
        }
        if (UnityEngine.InputSystem.Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * Time.deltaTime * 10;
        }
        if (UnityEngine.InputSystem.Keyboard.current.wKey.isPressed)
        {
            transform.position += Vector3.up * Time.deltaTime * 10;
        }
        if (UnityEngine.InputSystem.Keyboard.current.sKey.isPressed)
        {
            transform.position += Vector3.down * Time.deltaTime * 10;
        }
        
    } 
}
