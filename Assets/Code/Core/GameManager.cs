using Code.StackSystem;
using UnityEngine;

namespace Code.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReaderSO _inputReader;
        [SerializeField] private BlockSpawner _spawner;
        [SerializeField] private BlockCutter _cutter;
    
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 2.0f;
        [SerializeField] private StackBlock _baseBlock; // 맨 처음 바닥 블록

        private StackBlock _currentBlock;
        private Vector3 _lastPos;
        private Vector3 _lastSize;
        private bool _isGameOver = false;

        private void Start()
        {
            _lastPos = _baseBlock.transform.position;
            _lastSize = _baseBlock.transform.localScale;
        
            // 첫 블록 생성
            SpawnNext();
        
            // 입력 이벤트 연결
            _inputReader.OnPlayerTouch += HandleStackAttempt;
        }

        private void Update()
        {
            if (_isGameOver) return;
        }

        private void HandleStackAttempt()
        {
            if (_currentBlock == null || _isGameOver) return;

            _currentBlock.Stop();
        
            // 절단 및 판정 수행
            // 주의: _currentBlock.Axis는 위에서 짠 코드를 참조해 수정 필요
            _cutter.Slice(_currentBlock, _lastPos, _lastSize,_currentBlock.Axis);

            // 절단 후 데이터 갱신 (다음 층의 기준이 됨)
            _lastPos = _currentBlock.transform.position;
            _lastSize = _currentBlock.transform.localScale;

            SpawnNext();
        }

        private void SpawnNext()
        {
            _currentBlock = _spawner.Spawn(_lastPos, _lastSize, _moveSpeed);
            _moveSpeed += 0.1f; // 난이도 조절: 층마다 조금씩 빨라짐
        }

        private void OnDestroy()
        {
            _inputReader.OnPlayerTouch -= HandleStackAttempt;
        }
    }
}