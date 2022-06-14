using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//store all move locations, other class can get reference of move locations from this class
public class LocationManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> locations;

    //getters & setters
    public List<GameObject> Locations {get {return locations;}}
}
