namespace CrazyHub.Hyderabad.Assignment {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraController : MonoBehaviour {

        public Transform Target;
        public Vector3 offset;
        // Start is called before the first frame update
        void Start() {
            offset = transform.position - Target.position;
        }

        // Update is called once per frame
        void Update() {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, offset.z + Target.position.z);
            transform.position = newPos;
        }
    }
}

