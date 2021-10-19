using UnityEngine;

namespace GameResources
{
    public class ResourceItem : MonoBehaviour
    {
        public void InitResource(Resource resource)
        {
            
        }

        public void SetSize(Vector3 size)
        {
            transform.localScale = size;
        }
    }
}