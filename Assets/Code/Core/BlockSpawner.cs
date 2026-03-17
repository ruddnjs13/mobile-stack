using Code.StackSystem;
using UnityEngine;

namespace Code.Core
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private StackBlock blockPrefab;

        public StackBlock SpawnBlock(bool isRightBlock, StackBlock previousBlock)
        {
            Vector3 spawnPosition = isRightBlock ? spawnPoints[0].position : spawnPoints[1].position;

            StackBlock block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

            Vector3 moveDir = isRightBlock ? -Vector3.forward : Vector3.right;
            block.SetBlock(spawnPosition, moveDir);

            return block;
        }
    }
}