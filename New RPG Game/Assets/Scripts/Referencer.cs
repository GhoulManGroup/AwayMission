using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referencer : MonoBehaviour
{
    public string referenceName;

    private IEnumerator Start()
    {
        while (Manager.instance.referenceManager != null)
        {
            yield return null;
        }

        Manager.instance.referenceManager.reference.Add(referenceName, this.gameObject);

    }
}
