using UnityEngine;

public static class LayerMaskExtensions
{
    /// <summary>
    /// Returns true if layer mask contains layer.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}