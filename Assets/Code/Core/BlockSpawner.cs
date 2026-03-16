using Code.Objects;
using UnityEngine;

namespace Code.Core
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject blockPrefab;
        
        public void SpawnBlock(bool isRightBlock)
        {
            if (isRightBlock)
            {
                Instantiate(blockPrefab, spawnPoints[0].position, Quaternion.identity);
            }
            else
            {
                Instantiate(blockPrefab, spawnPoints[1].position, Quaternion.identity);
            }
        }
    }
}