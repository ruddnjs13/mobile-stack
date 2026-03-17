using UnityEngine;

namespace Code.StackSystem
{
    public class StackBlock : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        private Vector3 _moveDir;
        private bool _isMoving;

        public StackResult CurrentStackResult { get; private set; }

        public void SetBlock(Vector3 spawnPosition, Vector3 moveDir)
        {
            transform.position = spawnPosition;
            _moveDir = moveDir;
            _isMoving = true;

            // 초기 StackResult
            CurrentStackResult = new StackResult
            {
                remainingSize = new Vector2(transform.localScale.x, transform.localScale.z),
                cutSize = Vector2.zero,
                offset = Vector2.zero,
                isSuccess = true,
                isInitialized = true
            };
        }

        private void Update()
        {
            if (!_isMoving) return;
            transform.Translate(_moveDir * moveSpeed * Time.deltaTime);
        }

        public void Stop()
        {
            _isMoving = false;
        }

        public void ApplyStackResult(StackResult result)
        {
            CurrentStackResult = result;
        }
    }
}