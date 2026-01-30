using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public Dictionary<string, GameObject> reference = new Dictionary<string, GameObject>();

    private IEnumerator Start()
    {
        while (Manager.instance == null)
        {
            Debug.Log("Testing Log");
            yield return null;
        }

        Manager.instance.referenceManager = this;
    }

    public bool checkReference(string name)
    {
        return reference.ContainsKey(name);
    }

}
