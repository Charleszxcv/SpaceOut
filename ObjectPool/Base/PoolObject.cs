using System.Collections;
using UnityEngine;

public class PoolObject: MonoBehaviour {

    public virtual void OnObjectReuse() { }

    public virtual void Destroy() {
        gameObject.SetActive(false);
    }
}
