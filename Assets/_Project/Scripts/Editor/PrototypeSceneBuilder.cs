using Tester.Bosses;
using Tester.Combat;
using Tester.Core;
using Tester.Player;
using Tester.UI;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tester.Editor
{
    /// <summary>
    /// Builds the Bosque prototype scenes with simple Unity placeholders.
    /// </summary>
    public static partial class PrototypeSceneBuilder
    {
        private const string PrototypeMenuPath = "Tools/Tester/Build Prototype Scene";
        private const string DemoMenuPath = "Tools/Tester/Build Bosque Demo Scene";
        private const string SceneFolder = "Assets/_Project/Scenes";
        private const string PrototypeScenePath = SceneFolder + "/Prototype_Bosque_Test.unity";
        private const string DemoScenePath = SceneFolder + "/Prototype_Bosque_Demo.unity";
        private const string TagManagerPath = "ProjectSettings/TagManager.asset";

        [MenuItem(PrototypeMenuPath)]
        public static void BuildPrototypeScene()
        {
            if (!CanReplaceOpenScene())
            {
                return;
            }

            BuildScene(PrototypeScenePath, BuildPrototypeSceneContents, "Prototype scene");
        }

        [MenuItem(DemoMenuPath)]
        public static void BuildBosqueDemoScene()
        {
            if (!CanReplaceOpenScene())
            {
                return;
            }

            BuildScene(DemoScenePath, BuildDemoSceneContents, "Bosque demo scene");
        }

        private static bool CanReplaceOpenScene()
        {
            return Application.isBatchMode || EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        private static void BuildScene(string scenePath, System.Action createContents, string sceneLabel)
        {
            EnsureFolder(SceneFolder);
            EnsureProjectLabels();

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            createContents();

            EditorSceneManager.SaveScene(scene, scenePath);
            AddSceneToBuildSettings(scenePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Object sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            Selection.activeObject = sceneAsset;
            EditorGUIUtility.PingObject(sceneAsset);

            Debug.Log($"{sceneLabel} built at {scenePath}.");
        }

        private static void BuildPrototypeSceneContents()
        {
            int playerLayer = GetLayerOrDefault("Player");
            int groundLayer = GetLayerOrDefault("Ground");
            int enemyLayer = GetLayerOrDefault("Enemy");
            int interactableLayer = GetLayerOrDefault("Interactable");

            GameObject systemsRoot = CreateRootGroup("Systems");
            GameObject playerRoot = CreateRootGroup("Player");
            GameObject cameraRoot = CreateRootGroup("Camera");
            GameObject levelRoot = CreateRootGroup("Level");
            GameObject enemiesRoot = CreateRootGroup("Enemies");
            GameObject worldRoot = CreateRootGroup("World");
            GameObject gatesRoot = CreateRootGroup("Gates");
            GameObject bossRoot = CreateRootGroup("Bosses");

            CreateGameManager(systemsRoot.transform);

            PlayerBuild player = CreatePlayer(playerRoot.transform, playerLayer, groundLayer, enemyLayer);
            CreateCamera(cameraRoot.transform, player.GameObject.transform);
            CreatePrototypeLevel(levelRoot.transform, groundLayer);
            CreateBasicEnemy("Enemy_01", enemiesRoot.transform, new Vector2(-6f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_02", enemiesRoot.transform, new Vector2(1.25f, -0.55f), enemyLayer);
            CreateCheckpoint(worldRoot.transform, interactableLayer);
            CreateDashGate(gatesRoot.transform, interactableLayer, player.Abilities);
            CreateLucarelli(bossRoot.transform, enemyLayer, player.Health, player.Abilities);
            CreateCanvas(player.Health, player.Abilities);
            CreateEventSystem(systemsRoot.transform);
        }

        private static void BuildDemoSceneContents()
        {
            int playerLayer = GetLayerOrDefault("Player");
            int groundLayer = GetLayerOrDefault("Ground");
            int enemyLayer = GetLayerOrDefault("Enemy");
            int interactableLayer = GetLayerOrDefault("Interactable");

            GameObject systemsRoot = CreateRootGroup("Systems");
            GameObject playerRoot = CreateRootGroup("Player");
            GameObject cameraRoot = CreateRootGroup("Camera");
            GameObject levelRoot = CreateRootGroup("Level");
            GameObject enemiesRoot = CreateRootGroup("Enemies");
            GameObject checkpointsRoot = CreateRootGroup("Checkpoints");
            GameObject gatesRoot = CreateRootGroup("Gates");
            GameObject tutorialRoot = CreateRootGroup("Tutorial");
            GameObject decorationRoot = CreateRootGroup("Decoration");
            GameObject deathZonesRoot = CreateRootGroup("DeathZones");

            CreateGameManager(systemsRoot.transform);

            PlayerBuild player = CreatePlayer(playerRoot.transform, playerLayer, groundLayer, enemyLayer);
            player.GameObject.transform.position = new Vector3(-158f, -0.55f, 0f);

            CreateCamera(cameraRoot.transform, player.GameObject.transform);
            DemoLevelBuild demoLevel = CreateDemoRooms(
                levelRoot.transform,
                deathZonesRoot.transform,
                groundLayer
            );
            CreateDemoTutorial(tutorialRoot.transform);
            CreateDemoDecoration(decorationRoot.transform);

            CreateBasicEnemy("Enemy_Combat_01", enemiesRoot.transform, new Vector2(-56f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_Combat_02", enemiesRoot.transform, new Vector2(-42f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_Combat_03", enemiesRoot.transform, new Vector2(-28f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_Combat_04", enemiesRoot.transform, new Vector2(-11f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_Roots_01", enemiesRoot.transform, new Vector2(-62f, -6.55f), enemyLayer);
            CreateBasicEnemy("Enemy_Roots_02", enemiesRoot.transform, new Vector2(-30f, -6.55f), enemyLayer);
            CreateBasicEnemy("Enemy_Canopy_01", enemiesRoot.transform, new Vector2(-43f, 9.35f), enemyLayer);
            CreateBasicEnemy("Enemy_LucarelliPath_01", enemiesRoot.transform, new Vector2(18f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_LucarelliPath_02", enemiesRoot.transform, new Vector2(58f, -0.55f), enemyLayer);

            CreateCheckpoint(
                "Checkpoint_01_CentralHub",
                checkpointsRoot.transform,
                new Vector2(-95f, -0.4f),
                interactableLayer
            );
            CreateCheckpoint(
                "Checkpoint_02_Convergence",
                checkpointsRoot.transform,
                new Vector2(34f, -0.4f),
                interactableLayer
            );
            CreateCheckpoint(
                "Checkpoint_03_ArenaEntry",
                checkpointsRoot.transform,
                new Vector2(73f, -0.4f),
                interactableLayer
            );

            CreateDashGate(
                "DashGate_HubReturn",
                gatesRoot.transform,
                new Vector2(-117.5f, 6.35f),
                new Vector2(0.9f, 4.6f),
                interactableLayer,
                player.Abilities
            );

            CreateLucarelli(
                "Lucarelli_Boss",
                demoLevel.LucarelliArena,
                new Vector2(100f, -0.4f),
                enemyLayer,
                player.Health,
                player.Abilities
            );

            CreateCanvas(player.Health, player.Abilities);
            CreateEventSystem(systemsRoot.transform);
        }

        private static void CreateGameManager(Transform parent)
        {
            GameObject gameManager = new GameObject("GameManager");
            gameManager.transform.SetParent(parent);
            gameManager.AddComponent<GameManager>();
        }

        private static PlayerBuild CreatePlayer(
            Transform parent,
            int playerLayer,
            int groundLayer,
            int enemyLayer
        )
        {
            GameObject player = new GameObject("Rubens_Player");
            player.transform.SetParent(parent);
            player.transform.position = new Vector3(-10f, -0.55f, 0f);
            ConfigureLayer(player, playerLayer);
            ConfigureTagIfAvailable(player, "Player");
            ConfigurePlaceholderSprite(player, new Color(0.2f, 0.55f, 0.95f), new Vector2(0.9f, 1.6f));

            Rigidbody2D body = player.AddComponent<Rigidbody2D>();
            ConfigureDynamicBody(body, 3f);

            ConfigureBoxCollider2D(player, new Vector2(0.8f, 1.5f));

            AbilityManager abilities = player.AddComponent<AbilityManager>();
            PlayerHealth health = player.AddComponent<PlayerHealth>();
            PlayerController2D controller = player.AddComponent<PlayerController2D>();
            PlayerCombat combat = player.AddComponent<PlayerCombat>();

            Transform groundCheck = CreateChildPoint(player.transform, "GroundCheck", new Vector3(0f, -0.85f, 0f));
            Transform attackPoint = CreateChildPoint(player.transform, "AttackPoint", new Vector3(0.78f, 0f, 0f));

            SetObjectReference(controller, "groundCheck", groundCheck);
            SetObjectReference(controller, "abilityManager", abilities);
            SetLayerMask(controller, "groundLayer", LayerMaskFor(groundLayer));
            SetObjectReference(combat, "attackPoint", attackPoint);
            SetLayerMask(combat, "enemyLayers", LayerMaskFor(enemyLayer));

            return new PlayerBuild(player, health, abilities);
        }

        private static void CreateCamera(Transform parent, Transform target)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.transform.SetParent(parent);
            cameraObject.transform.position = new Vector3(target.position.x, target.position.y + 1.5f, -10f);
            cameraObject.tag = "MainCamera";

            Camera camera = cameraObject.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 6f;
            camera.backgroundColor = new Color(0.06f, 0.1f, 0.12f);
            cameraObject.AddComponent<AudioListener>();

            CameraFollow2D follow = cameraObject.AddComponent<CameraFollow2D>();
            SetObjectReference(follow, "target", target);
        }

        private static void CreateLucarelli(
            Transform parent,
            int enemyLayer,
            PlayerHealth playerHealth,
            AbilityManager abilities
        )
        {
            CreateLucarelli(
                "Lucarelli_Boss",
                parent,
                new Vector2(8.5f, -0.4f),
                enemyLayer,
                playerHealth,
                abilities
            );
        }

        private static void CreateLucarelli(
            string name,
            Transform parent,
            Vector2 position,
            int enemyLayer,
            PlayerHealth playerHealth,
            AbilityManager abilities
        )
        {
            GameObject lucarelli = new GameObject(name);
            lucarelli.transform.SetParent(parent);
            lucarelli.transform.position = position;
            ConfigureLayer(lucarelli, enemyLayer);
            ConfigurePlaceholderSprite(
                lucarelli,
                new Color(0.75f, 0.12f, 0.18f),
                new Vector2(1.35f, 2.1f)
            );

            Rigidbody2D body = lucarelli.AddComponent<Rigidbody2D>();
            ConfigureDynamicBody(body, 3f);

            ConfigureBoxCollider2D(lucarelli, new Vector2(1.2f, 2f));

            LucarelliBoss boss = lucarelli.AddComponent<LucarelliBoss>();
            Transform closeAttackPoint = CreateChildPoint(
                lucarelli.transform,
                "CloseAttackPoint",
                new Vector3(0.25f, 0f, 0f)
            );

            SetObjectReference(boss, "playerHealth", playerHealth);
            SetObjectReference(boss, "rewardAbilityManager", abilities);
            SetObjectReference(boss, "closeAttackPoint", closeAttackPoint);
        }

        private static void CreateCanvas(PlayerHealth playerHealth, AbilityManager abilities)
        {
            GameObject canvasObject = new GameObject(
                "PrototypeCanvas",
                typeof(RectTransform),
                typeof(Canvas),
                typeof(CanvasScaler),
                typeof(GraphicRaycaster)
            );

            Canvas canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            RectTransform hudRoot = CreateRectObject("HUDRoot", canvasObject.transform);
            StretchToParent(hudRoot);

            HUDController hud = hudRoot.gameObject.AddComponent<HUDController>();
            Text healthText = CreateText(
                "Txt_Health",
                hudRoot,
                "Vida: --/--",
                new Vector2(0f, 1f),
                new Vector2(0f, 1f),
                new Vector2(24f, -24f),
                new Vector2(320f, 40f),
                new Vector2(0f, 1f),
                TextAnchor.MiddleLeft,
                26
            );
            Text dashText = CreateText(
                "Txt_Dash",
                hudRoot,
                "Dash: Bloqueado",
                new Vector2(0f, 1f),
                new Vector2(0f, 1f),
                new Vector2(24f, -68f),
                new Vector2(360f, 40f),
                new Vector2(0f, 1f),
                TextAnchor.MiddleLeft,
                24
            );
            Text messageText = CreateText(
                "Txt_Message",
                hudRoot,
                string.Empty,
                new Vector2(0.5f, 1f),
                new Vector2(0.5f, 1f),
                new Vector2(0f, -24f),
                new Vector2(760f, 50f),
                new Vector2(0.5f, 1f),
                TextAnchor.MiddleCenter,
                28
            );
            messageText.gameObject.SetActive(false);

            SetObjectReference(hud, "hudCanvas", canvas);
            SetObjectReference(hud, "playerHealth", playerHealth);
            SetObjectReference(hud, "abilityManager", abilities);
            SetObjectReference(hud, "healthText", healthText);
            SetObjectReference(hud, "dashStateText", dashText);
            SetObjectReference(hud, "messageText", messageText);

            RectTransform pauseRoot = CreateRectObject("PauseMenu", canvasObject.transform);
            StretchToParent(pauseRoot);
            PauseMenuController pause = pauseRoot.gameObject.AddComponent<PauseMenuController>();

            GameObject pausePanel = CreatePausePanel(pauseRoot);
            SetObjectReference(pause, "pausePanel", pausePanel);
            pausePanel.SetActive(false);

            CreatePauseContent(pausePanel.transform, pause);
        }

        private static GameObject CreatePausePanel(RectTransform parent)
        {
            GameObject panel = new GameObject(
                "PausePanel",
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Image)
            );
            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            StretchToParent(rect);

            Image image = panel.GetComponent<Image>();
            image.color = new Color(0f, 0f, 0f, 0.72f);

            return panel;
        }

        private static void CreatePauseContent(Transform panel, PauseMenuController pause)
        {
            RectTransform panelRect = panel as RectTransform;

            CreateText(
                "PauseTitle",
                panelRect,
                "Jogo Pausado",
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0f, 180f),
                new Vector2(440f, 72f),
                new Vector2(0.5f, 0.5f),
                TextAnchor.MiddleCenter,
                40
            );

            CreateButton(
                "Btn_Continuar",
                panelRect,
                "Continuar",
                new Vector2(0f, 70f),
                pause.ContinueGame
            );
            CreateButton(
                "Btn_Reiniciar",
                panelRect,
                "Reiniciar",
                Vector2.zero,
                pause.RestartScene
            );
            CreateButton(
                "Btn_Sair",
                panelRect,
                "Sair",
                new Vector2(0f, -70f),
                pause.QuitGame
            );
        }

        private static Button CreateButton(
            string name,
            RectTransform parent,
            string label,
            Vector2 anchoredPosition,
            UnityAction onClick
        )
        {
            GameObject buttonObject = new GameObject(
                name,
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Image),
                typeof(Button)
            );

            RectTransform rect = buttonObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(320f, 54f);
            rect.anchoredPosition = anchoredPosition;

            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.22f, 0.28f, 0.32f, 0.95f);

            Button button = buttonObject.GetComponent<Button>();
            UnityEventTools.AddPersistentListener(button.onClick, onClick);

            Text text = CreateText(
                "Label",
                rect,
                label,
                Vector2.zero,
                Vector2.one,
                Vector2.zero,
                Vector2.zero,
                new Vector2(0.5f, 0.5f),
                TextAnchor.MiddleCenter,
                24
            );
            StretchToParent(text.rectTransform);

            return button;
        }

        private static Text CreateText(
            string name,
            RectTransform parent,
            string value,
            Vector2 anchorMin,
            Vector2 anchorMax,
            Vector2 anchoredPosition,
            Vector2 size,
            Vector2 pivot,
            TextAnchor alignment,
            int fontSize
        )
        {
            GameObject textObject = new GameObject(
                name,
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Text)
            );

            RectTransform rect = textObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.pivot = pivot;
            rect.anchoredPosition = anchoredPosition;
            rect.sizeDelta = size;

            Text text = textObject.GetComponent<Text>();
            text.text = value;
            text.font = GetLegacyFont();
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = Color.white;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;

            return text;
        }

        private static void CreateEventSystem(Transform parent)
        {
            GameObject eventSystemObject = new GameObject(
                "EventSystem",
                typeof(EventSystem),
                typeof(StandaloneInputModule)
            );
            eventSystemObject.transform.SetParent(parent);
        }

        private readonly struct PlayerBuild
        {
            public readonly GameObject GameObject;
            public readonly PlayerHealth Health;
            public readonly AbilityManager Abilities;

            public PlayerBuild(GameObject gameObject, PlayerHealth health, AbilityManager abilities)
            {
                GameObject = gameObject;
                Health = health;
                Abilities = abilities;
            }
        }
    }
}
