using UnityEngine;
using Unity.Netcode;

// NOTE:  What I did here is update to inherit from NetworkBehaviour instead of MonoBehavior.
//        This required adding the com.unity.netcode.runtime assembly to these assemblies:
//        MoreMountains.CorgiEngine
//        MoreMountains.CorgiEngine.Editor

namespace MoreMountains.CorgiEngine
{
    /// <summary>
    /// The CorgiMonoBehaviour class is a base class for all Corgi Engine classes.
    /// It doesn't do anything, but ensures you have a single point of change should you want your classes to inherit from something else than a plain MonoBehaviour
    /// A frequent use case for this would be adding a network layer
    /// </summary>
    public class CorgiMonoBehaviour : NetworkBehaviour { }
}