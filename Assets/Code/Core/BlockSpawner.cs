using Code.StackSystem;
using UnityEngine;

namespace Code.Core
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private StackBlock _blockPrefab;
        private StackBlock.MoveAxis _currentAxis = StackBlock.MoveAxis.X;

        public StackBlock Spawn(Vector3 lastPos, Vector3 lastSize, float speed)
        {
            // 다음 층은 축을 교체 (X -> Z -> X ...)
            _currentAxis = (_currentAxis == StackBlock.MoveAxis.X) ? StackBlock.MoveAxis.Z : StackBlock.MoveAxis.X;
        
            // Y값만 한 층 위로 올림
            Vector3 spawnPos = lastPos + Vector3.up; 
        
            StackBlock newBlock = Instantiate(_blockPrefab, spawnPos, Quaternion.identity);
            newBlock.Init(_currentAxis, speed, lastSize, lastPos);
        
            return newBlock;
        }
    }
}