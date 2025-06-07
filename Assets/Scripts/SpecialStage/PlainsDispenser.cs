using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsDispenser : MonoBehaviour
{
    [SerializeField] GameObject substitute;
    [SerializeField] Plains plain;
    void Start()
    {
        int carId = plain.carModel.Id;
        SpecialRoadModule roadModule = FindObjectOfType<SpecialRoadModule>();

        bool carIsAvailable = GameManager.instance.IsCarAvailable(carId);
        
        if (carIsAvailable)
        {

            GameObject plainInstantiated = Instantiate(substitute, transform.position, Quaternion.identity);
            plainInstantiated.transform.SetParent(roadModule.transform);

            Destroy(this.gameObject);
            return;
        }

        GameObject coinInantiated =  Instantiate(plain.gameObject, transform.position, Quaternion.identity);
        coinInantiated.transform.SetParent(roadModule.transform);

        Destroy(this.gameObject);
    }


}
