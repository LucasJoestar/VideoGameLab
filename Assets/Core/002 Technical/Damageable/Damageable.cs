using UnityEngine;

public class Damageable : MonoBehaviour
{
    #region Fields and Properties
    #endregion

    #region Methods
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Touch me -> " + other.name);
    }
    #endregion
}
