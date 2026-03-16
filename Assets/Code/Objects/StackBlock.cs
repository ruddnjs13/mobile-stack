using System;
using UnityEngine;

namespace Code.Objects
{
    public class StackBlock : MonoBehaviour
    {
        [SerializeField] private bool _isRightBlock = false;
        [SerializeField] private float _moveSpeed;
        
        private void Update()
        {
            MoveBlock();
        }

        public void MoveBlock()
        {
            Vector3 moveDir = _isRightBlock ? Vector3.forward * -1 : Vector3.right;
            
            transform.Translate(Vector3.right * (_moveSpeed * Time.deltaTime));
        }
    }
}