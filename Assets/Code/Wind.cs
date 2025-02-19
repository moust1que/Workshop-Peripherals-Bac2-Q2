using UnityEngine;

public class WindManager : MonoBehaviour
{
    public Vector3 currentWindDirection;
    public float windStrength;

    public enum WindDirection
    { 
        North,
        South,
        East,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest
    }
    void Awake()
    {
        Debug.Log("WindManager Awake : génération du vent");
        SetRandomWind();
    }

    public void SetRandomWind()
    {
        WindDirection[] directions = (WindDirection[])System.Enum.GetValues(typeof(WindDirection));
        WindDirection selectedWind = directions[UnityEngine.Random.Range(0, directions.Length)];

        currentWindDirection = GetWindVector(selectedWind);
        windStrength = UnityEngine.Random.Range(0f, 80f); 

        UIContoller uiController = UnityEngine.Object.FindFirstObjectByType<UIContoller>();
        if (uiController != null)
        {
            uiController.UpdateWindUI(currentWindDirection, windStrength);
        }
    }

    private Vector3 GetWindVector(WindDirection direction)
    {
        switch (direction)
        {
            case WindDirection.North:      return Vector3.forward;
            case WindDirection.South:      return Vector3.back;
            case WindDirection.East:       return Vector3.right;
            case WindDirection.West:       return Vector3.left;
            case WindDirection.NorthEast:  return (Vector3.forward + Vector3.right).normalized;
            case WindDirection.NorthWest:  return (Vector3.forward + Vector3.left).normalized;
            case WindDirection.SouthEast:  return (Vector3.back + Vector3.right).normalized;
            case WindDirection.SouthWest:  return (Vector3.back + Vector3.left).normalized;
            default:                     return Vector3.zero;
        }
    }

    public Vector3 GetCurrentWindDirection()
    {
        return currentWindDirection;
    }

    public float GetCurrentWindStrength()
    {
        return windStrength;
    }
}
