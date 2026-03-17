using Code.StackSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private BlockSpawner blockSpawner;

        public int Score { get; private set; }
        public bool IsPlaying { get; private set; }

        public StackBlock CurrentBlock { get; private set; }
        public StackBlock PreviousBlock { get; private set; }

        private void OnEnable() => inputReader.OnPlayerTouch += HandlePlayerTouch;
        private void OnDisable() => inputReader.OnPlayerTouch -= HandlePlayerTouch;

        public void InitGame()
        {
            Score = 0;
            IsPlaying = true;
            PreviousBlock = null;
            CurrentBlock = blockSpawner.SpawnBlock(true, null);
        }

        private void HandlePlayerTouch()
        {
            if (!IsPlaying)
            {
                InitGame();
                return;
            }

            if (CurrentBlock == null) return;

            // 1️⃣ 현재 블록 멈춤
            CurrentBlock.Stop();

            // 2️⃣ 판정
            StackResult result;
            if (PreviousBlock == null)
            {
                // 첫 블록
                result = CurrentBlock.CurrentStackResult;
            }
            else
            {
                result = StackUtil.JudgeBlock(CurrentBlock.transform.position, PreviousBlock, CurrentBlock.transform.localScale);
                CurrentBlock.ApplyStackResult(result);

                // 3️⃣ 실패 시
                if (!result.isSuccess)
                {
                    IsPlaying = false;
                    Debug.Log("Game Over");
                    return;
                }

                // 4️⃣ 잘린 블록 처리
                BlockCutter.ApplyCut(CurrentBlock, result);
            }

            // 5️⃣ 다음 블록 생성
            PreviousBlock = CurrentBlock;
            bool isRightBlock = Score % 2 == 0;
            CurrentBlock = blockSpawner.SpawnBlock(isRightBlock, PreviousBlock);
            Score++;
        }
    }
}