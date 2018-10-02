using UnityEngine;

public class Util
{
    public static void SetLayerRecursively(GameObject _obj, int _layer)
    {
        if(_obj == null)
        {
            return;
        }

        _obj.layer = _layer;

        foreach (Transform _child in _obj.transform)
        {
            if (_child != null)
            {
                SetLayerRecursively(_child.gameObject, _layer);
            }
        }
    }

}
