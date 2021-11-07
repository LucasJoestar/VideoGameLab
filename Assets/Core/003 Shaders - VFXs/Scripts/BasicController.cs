using UnityEngine;

public class BasicController : MonoBehaviour
{
        [SerializeField]
    GameObject SecondaryWeaponPrefab;
        [SerializeField]
    GameObject firePoint;
    GameObject spawnedLaser;


    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
    }

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
    }
    

    void UpdateLaser()
    {
        if(firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }



    void Start()
    {
        spawnedLaser = Instantiate(SecondaryWeaponPrefab, firePoint.transform) as GameObject;
        DisableLaser();
    }

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.pKey.wasPressedThisFrame)
        {
            EnableLaser();
        }
        if(UnityEngine.InputSystem.Keyboard.current.pKey.isPressed)
        {
            UpdateLaser();
        }
        if (UnityEngine.InputSystem.Keyboard.current.pKey.wasReleasedThisFrame)
        {
            DisableLaser();
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
