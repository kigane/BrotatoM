using UnityEngine;

namespace BrotatoM
{
    public class GameManager : MonoBehaviour
    {
        private GameManager() { }
        private readonly static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new("GameManager");
                    go.AddComponent<GameManager>();
                    go.AddComponent<DontDestroyOnLoadScript>();
                    return go.GetComponent<GameManager>();
                }
                return instance;
            }
        }

        public GameState State { get; set; } = GameState.PLAY;

        public void GenerateEnemies(int amount)
        {
            var enemyGO = Resources.Load<GameObject>("Prefabs/BabyAlien");
            float x, y;
            for (int i = 0; i < amount; i++)
            {
                x = Random.Range(-Params.HorizontalBoundSize, Params.HorizontalBoundSize);
                y = Random.Range(-Params.VerticalBoundSize, Params.VerticalBoundSize);
                Instantiate(enemyGO, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
