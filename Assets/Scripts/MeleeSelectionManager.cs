using UnityEngine;

namespace Scripts
{
    public class MeleeSelectionManager : MonoBehaviour
    {
        private Transform playerTransform;
        private Transform cameraTransform;
    
        private readonly float damageTakeAmount = 50f;
    
        private void Start()
        {
            playerTransform = GameObject.Find("Player").transform;
            cameraTransform = GameObject.Find("Main Camera").transform;
        }
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
                
            RaycastHit hit;
            if (!Physics.Raycast(playerTransform.position, cameraTransform.forward, out hit, 3f)) return;
                
            var selectionObj = hit.transform.gameObject;
            var selection = selectionObj.GetComponent<IDamageable>();
            if (selection == null) return;
                
            selection.TakeDamage(damageTakeAmount);
        }
    }
}

