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
            yield return null;
        }
        Manager.instance.referenceManager = this;
    }

    public IEnumerator Get(string name)
    {
        yield return reference[name];
    }
}
