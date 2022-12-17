using UnityEngine;

namespace BrotatoM
{
    public class GameManagerSystem : MonoBehaviour
    {
        private GameManagerSystem() { }
        private static GameManagerSystem instance;
        private static readonly object mLock = new();
        public static GameManagerSystem Instance
        {
            get
            {
                lock (mLock)
                {
                    if (instance == null)
                    {
                        GameObject go = new("GameManager");
                        go.AddComponent<GameManagerSystem>();
                        go.AddComponent<DontDestroyOnLoadScript>();
                        instance = go.GetComponent<GameManagerSystem>();
                        return instance;
                    }
                }
                return instance;
            }
        }

        public GameState State { get; set; } = GameState.PLAY;

        public void GenerateEnemies(string name, int amount)
        {
            var enemyGO = Resources.Load<GameObject>("Prefabs/" + name);
            if (enemyGO == null)
            {
                Log.Error("[" + name + "] is not a valid Prefab name!");
                return;
            }

            float x, y;
            for (int i = 0; i < amount; i++)
            {
                x = Random.Range(-Params.HorizontalBoundSize, Params.HorizontalBoundSize);
                y = Random.Range(-Params.VerticalBoundSize, Params.VerticalBoundSize);
                Instantiate(enemyGO, new Vector3(x, y, 0), Quaternion.identity, GameObject.FindWithTag("Enemy").transform);
            }
        }
    }
}
