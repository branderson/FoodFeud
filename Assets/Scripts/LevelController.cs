using System.Linq;
using Assets.Scripts.Towers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        // Cached instance of LevelController for lookup
        private static LevelController instanceRef = null;

        private Text moneyText;
        private Text healthText;
        private Text scoreText;
        private Text enemyText;

        // Tower Building
        [SerializeField] private GameObject Tower1Prefab;
        [SerializeField] private GameObject Tower2Prefab;
        [SerializeField] private GameObject Tower3Prefab;
        [SerializeField] private GameObject _startButton;

        public int SodaTowerControllerCost = 100;
        public int FriesTowerControllerCost = 400;
        public int NuggetTowerControllerCost = 500;

        // Enemy types
        [SerializeField] public GameObject TacoPrefab;
        private bool startedLevel;
        private bool placeTower;
        public GameObject TowerPrefab;
        public int TowerCost = 0;
        // Player information
        private int money;
        private int health;
        private int score;
        private int enemies;

        // Instantiate singleton at start of scene
        public static LevelController instance {
            get {
                if (instanceRef == null) {
                    // This is where the magic happens.
                    //  FindObjectOfType(...) returns the first LevelController object in the scene.
                    instanceRef =  FindObjectOfType(typeof (LevelController)) as LevelController;
                }
 
                // If it is still null, create a new instance
                if (instanceRef == null) {
                    GameObject obj = new GameObject("LevelController");
                    instanceRef = obj.AddComponent(typeof (LevelController)) as LevelController;
                }
 
                return instanceRef;
            }
        }

        public int Money
        {
            get
            {
                return money;
            }

            set
            {
                money = value;
                if (money < SodaTowerControllerCost)
                {
                    TowerSelectorController.instance.SetUnbuildable(Tower.SodaTower);
                }
                else
                {
                    TowerSelectorController.instance.SetBuildable(Tower.SodaTower);
                }
                if (money < FriesTowerControllerCost)
                {
                    TowerSelectorController.instance.SetUnbuildable(Tower.FriesTower);
                }
                else
                {
                    TowerSelectorController.instance.SetBuildable(Tower.FriesTower);
                }
                if (money < NuggetTowerControllerCost)
                {
                    TowerSelectorController.instance.SetUnbuildable(Tower.NuggetTower);
                }
                else
                {
                    TowerSelectorController.instance.SetBuildable(Tower.NuggetTower);
                }
            }
        }

        public bool PlaceTower
        {
            get
            {
                return placeTower;
            }

            set
            {
                placeTower = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
                if (healthText == null) return;
                healthText.text = health.ToString();
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                if (scoreText == null) return;
                if (startedLevel) scoreText.text = score.ToString();
                else scoreText.text = "";
            }
        }

        public int Enemies
        {
            get
            {
                return enemies;
            }

            set
            {
                enemies = value;
                if (enemyText == null) return;
                enemyText.text = enemies.ToString();
            }
        }

        // Use this for initialization
        void Start ()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            if (SceneManager.GetActiveScene().name == "LevelScene")
            {
                moneyText = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
                healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
                scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
                enemyText = GameObject.FindGameObjectWithTag("EnemyText").GetComponent<Text>();

                money = 500;
                Score = 0;
                Enemies = 0;
                Health = 20;
                placeTower = false;
                TowerCost = 0;
                startedLevel = false;
            }
        }
	
        // Update is called once per frame
        void Update () {
            if (SceneManager.GetActiveScene().name == "LevelScene")
            {
                if (Input.GetButtonDown("Start") && startedLevel == false)
                {
                    StartGame();
                }

                // Update money display
                moneyText.text = Money.ToString();
            }
        }

        public void SetSelection(Tower tower)
        {
            switch (tower)
            {
                case Tower.SodaTower:
                    PlaceTower = true;
                    TowerPrefab = Tower1Prefab;
                    TowerCost = SodaTowerControllerCost;
                    break;
                case Tower.FriesTower:
                    PlaceTower = true;
                    TowerPrefab = Tower2Prefab;
                    TowerCost = FriesTowerControllerCost;
                    break;
                case Tower.NuggetTower:
                    PlaceTower = true;
                    TowerPrefab = Tower3Prefab;
                    TowerCost = NuggetTowerControllerCost;
                    break;
                default:
                    PlaceTower = false;
                    TowerCost = 0;
                    break;
            }
        }

        // Ensure that the instance is destroyed when the game is stopped in the editor.
        void OnApplicationQuit() {
            instanceRef = null;
        }

        public void StartGame()
        {
            startedLevel = true;
            foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnPoint"))
            {
                spawn.GetComponent<SpawnPointController>().enabled = true;
            }
            _startButton.SetActive(false);
            Score = 0;
        }

        public void LoseGame()
        {
            SceneManager.LoadScene("GameOverScene");
        }

        public void QuitGame()
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}