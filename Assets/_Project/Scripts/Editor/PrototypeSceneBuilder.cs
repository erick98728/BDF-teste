using System.Collections.Generic;
using Tester.Bosses;
using Tester.Combat;
using Tester.Core;
using Tester.Enemies;
using Tester.Interactables;
using Tester.Player;
using Tester.UI;
using Tester.World;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tester.Editor
{
    /// <summary>
    /// Builds the Bosque prototype room with simple Unity placeholders.
    /// </summary>
    public static class PrototypeSceneBuilder
    {
        private const string MenuPath = "Tools/Tester/Build Prototype Scene";
        private const string SceneFolder = "Assets/_Project/Scenes";
        private const string ScenePath = SceneFolder + "/Prototype_Bosque_Test.unity";
        private const string TagManagerPath = "ProjectSettings/TagManager.asset";

        private static Sprite placeholderSprite;
        private static Font legacyFont;

        [MenuItem(MenuPath)]
        public static void BuildPrototypeScene()
        {
            if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }

            BuildPrototypeSceneContents();
        }

        private static void BuildPrototypeSceneContents()
        {
            EnsureFolder(SceneFolder);
            EnsureProjectLabels();

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            BuildSceneContents();

            EditorSceneManager.SaveScene(scene, ScenePath);
            AddSceneToBuildSettings(ScenePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Object sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
            Selection.activeObject = sceneAsset;
            EditorGUIUtility.PingObject(sceneAsset);

            Debug.Log($"Prototype scene built at {ScenePath}.");
        }

        private static void BuildSceneContents()
        {
            int playerLayer = GetLayerOrDefault("Player");
            int groundLayer = GetLayerOrDefault("Ground");
            int enemyLayer = GetLayerOrDefault("Enemy");
            int interactableLayer = GetLayerOrDefault("Interactable");

            GameObject systemsRoot = CreateRoot("Systems");
            GameObject playerRoot = CreateRoot("Player");
            GameObject cameraRoot = CreateRoot("Camera");
            GameObject levelRoot = CreateRoot("Level");
            GameObject enemiesRoot = CreateRoot("Enemies");
            GameObject worldRoot = CreateRoot("World");
            GameObject gatesRoot = CreateRoot("Gates");
            GameObject bossRoot = CreateRoot("Bosses");

            CreateGameManager(systemsRoot.transform);

            PlayerBuild player = CreatePlayer(playerRoot.transform, playerLayer, groundLayer, enemyLayer);
            CreateCamera(cameraRoot.transform, player.GameObject.transform);
            CreateLevel(levelRoot.transform, groundLayer);
            CreateBasicEnemy("Enemy_01", enemiesRoot.transform, new Vector2(-6f, -0.55f), enemyLayer);
            CreateBasicEnemy("Enemy_02", enemiesRoot.transform, new Vector2(1.25f, -0.55f), enemyLayer);
            CreateCheckpoint(worldRoot.transform, interactableLayer);
            CreateAbilityGate(gatesRoot.transform, interactableLayer, player.Abilities);
            CreateLucarelli(bossRoot.transform, enemyLayer, player.Health, player.Abilities);
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
            player.layer = playerLayer;
            SetTagIfAvailable(player, "Player");
            AddWorldSprite(player, new Color(0.2f, 0.55f, 0.95f), new Vector2(0.9f, 1.6f));

            Rigidbody2D body = player.AddComponent<Rigidbody2D>();
            ConfigureDynamicBody(body, 3f);

            BoxCollider2D collider = player.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.8f, 1.5f);

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

        private static void CreateLevel(Transform parent, int groundLayer)
        {
            CreateLevelBlock(
                "Ground_Main",
                parent,
                new Vector2(0f, -2f),
                new Vector2(28f, 1f),
                new Color(0.18f, 0.3f, 0.18f),
                groundLayer
            );
            CreateLevelBlock(
                "Platform_Low",
                parent,
                new Vector2(-4f, -0.15f),
                new Vector2(3.5f, 0.45f),
                new Color(0.2f, 0.36f, 0.22f),
                groundLayer
            );
            CreateLevelBlock(
                "Platform_Arena",
                parent,
                new Vector2(7.5f, -0.1f),
                new Vector2(3.2f, 0.45f),
                new Color(0.2f, 0.36f, 0.22f),
                groundLayer
            );
            CreateLevelBlock(
                "Ground_Exit",
                parent,
                new Vector2(17f, -2f),
                new Vector2(6f, 1f),
                new Color(0.18f, 0.3f, 0.18f),
                groundLayer
            );
        }

        private static void CreateLevelBlock(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int layer
        )
        {
            GameObject block = CreateWorldBlock(name, parent, position, size, color, layer);
            BoxCollider2D collider = block.AddComponent<BoxCollider2D>();
            collider.size = size;
        }

        private static void CreateBasicEnemy(string name, Transform parent, Vector2 position, int enemyLayer)
        {
            GameObject enemy = new GameObject(name);
            enemy.transform.SetParent(parent);
            enemy.transform.position = position;
            enemy.layer = enemyLayer;
            AddWorldSprite(enemy, new Color(0.5f, 0.15f, 0.6f), new Vector2(1f, 1.45f));

            Rigidbody2D body = enemy.AddComponent<Rigidbody2D>();
            ConfigureDynamicBody(body, 3f);

            BoxCollider2D collider = enemy.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.9f, 1.4f);

            enemy.AddComponent<BasicPatrolEnemy>();
        }

        private static void CreateCheckpoint(Transform parent, int interactableLayer)
        {
            GameObject checkpoint = new GameObject("Checkpoint_01");
            checkpoint.transform.SetParent(parent);
            checkpoint.transform.position = new Vector3(-1.5f, -0.4f, 0f);
            checkpoint.layer = interactableLayer;
            SetTagIfAvailable(checkpoint, "Interactable");
            AddWorldSprite(checkpoint, new Color(0.95f, 0.8f, 0.2f, 0.55f), new Vector2(1f, 2.1f));

            BoxCollider2D trigger = checkpoint.AddComponent<BoxCollider2D>();
            trigger.size = new Vector2(1f, 2f);
            trigger.isTrigger = true;

            Checkpoint checkpointComponent = checkpoint.AddComponent<Checkpoint>();
            Transform respawnPoint = CreateChildPoint(
                checkpoint.transform,
                "RespawnPoint",
                new Vector3(0f, 0.2f, 0f)
            );

            SetObjectReference(checkpointComponent, "respawnPoint", respawnPoint);
        }

        private static void CreateAbilityGate(
            Transform parent,
            int interactableLayer,
            AbilityManager abilities
        )
        {
            GameObject gate = new GameObject("DashGate_01");
            gate.transform.SetParent(parent);
            gate.transform.position = new Vector3(13.3f, 0f, 0f);
            gate.layer = interactableLayer;
            SetTagIfAvailable(gate, "Interactable");

            BoxCollider2D blockingCollider = gate.AddComponent<BoxCollider2D>();
            blockingCollider.size = new Vector2(0.75f, 4f);

            GameObject visual = new GameObject("LockedVisual");
            visual.transform.SetParent(gate.transform);
            visual.transform.localPosition = Vector3.zero;
            AddWorldSprite(visual, new Color(0.25f, 0.7f, 0.95f, 0.85f), new Vector2(0.75f, 4f));

            AbilityGate abilityGate = gate.AddComponent<AbilityGate>();
            SetObjectReference(abilityGate, "abilityManager", abilities);
            SetObjectReference(abilityGate, "blockingCollider", blockingCollider);
            SetObjectReference(abilityGate, "lockedVisual", visual);
        }

        private static void CreateLucarelli(
            Transform parent,
            int enemyLayer,
            PlayerHealth playerHealth,
            AbilityManager abilities
        )
        {
            GameObject lucarelli = new GameObject("Lucarelli_Boss");
            lucarelli.transform.SetParent(parent);
            lucarelli.transform.position = new Vector3(8.5f, -0.4f, 0f);
            lucarelli.layer = enemyLayer;
            AddWorldSprite(lucarelli, new Color(0.75f, 0.12f, 0.18f), new Vector2(1.35f, 2.1f));

            Rigidbody2D body = lucarelli.AddComponent<Rigidbody2D>();
            ConfigureDynamicBody(body, 3f);

            BoxCollider2D collider = lucarelli.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1.2f, 2f);

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

        private static GameObject CreateWorldBlock(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            Color color,
            int layer
        )
        {
            GameObject block = new GameObject(name);
            block.transform.SetParent(parent);
            block.transform.position = position;
            block.layer = layer;
            AddWorldSprite(block, color, size);
            return block;
        }

        private static void AddWorldSprite(GameObject gameObject, Color color, Vector2 size)
        {
            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = GetPlaceholderSprite();
            spriteRenderer.color = color;
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.size = size;
        }

        private static Transform CreateChildPoint(Transform parent, string name, Vector3 localPosition)
        {
            GameObject point = new GameObject(name);
            point.transform.SetParent(parent);
            point.transform.localPosition = localPosition;
            return point.transform;
        }

        private static RectTransform CreateRectObject(string name, Transform parent)
        {
            GameObject gameObject = new GameObject(name, typeof(RectTransform));
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            return rect;
        }

        private static void StretchToParent(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
        }

        private static void ConfigureDynamicBody(Rigidbody2D body, float gravityScale)
        {
            body.bodyType = RigidbodyType2D.Dynamic;
            body.gravityScale = gravityScale;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private static GameObject CreateRoot(string name)
        {
            return new GameObject(name);
        }

        private static Sprite GetPlaceholderSprite()
        {
            if (placeholderSprite == null)
            {
                placeholderSprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            }

            return placeholderSprite;
        }

        private static Font GetLegacyFont()
        {
            if (legacyFont == null)
            {
                legacyFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            }

            return legacyFont;
        }

        private static void SetObjectReference(Object target, string propertyName, Object value)
        {
            SerializedObject serializedTarget = new SerializedObject(target);
            SerializedProperty property = serializedTarget.FindProperty(propertyName);

            if (property == null)
            {
                Debug.LogWarning($"Could not set {propertyName} on {target.name}.", target);
                return;
            }

            property.objectReferenceValue = value;
            serializedTarget.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void SetLayerMask(Object target, string propertyName, int value)
        {
            SerializedObject serializedTarget = new SerializedObject(target);
            SerializedProperty property = serializedTarget.FindProperty(propertyName);

            if (property == null)
            {
                Debug.LogWarning($"Could not set {propertyName} on {target.name}.", target);
                return;
            }

            property.intValue = value;
            serializedTarget.ApplyModifiedPropertiesWithoutUndo();
        }

        private static int LayerMaskFor(int layer)
        {
            return layer > 0 ? 1 << layer : 0;
        }

        private static int GetLayerOrDefault(string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);

            if (layer >= 0)
            {
                return layer;
            }

            Debug.LogWarning($"Layer {layerName} is unavailable. Using Default for the generated scene.");
            return 0;
        }

        private static void SetTagIfAvailable(GameObject gameObject, string tagName)
        {
            if (!HasTag(tagName))
            {
                return;
            }

            gameObject.tag = tagName;
        }

        private static void EnsureProjectLabels()
        {
            EnsureTag("Player");
            EnsureTag("Interactable");
            EnsureLayer("Player");
            EnsureLayer("Ground");
            EnsureLayer("Enemy");
            EnsureLayer("Interactable");
        }

        private static bool HasTag(string tagName)
        {
            foreach (string existingTag in InternalEditorUtility.tags)
            {
                if (existingTag == tagName)
                {
                    return true;
                }
            }

            return false;
        }

        private static void EnsureTag(string tagName)
        {
            if (HasTag(tagName))
            {
                return;
            }

            SerializedObject tagManager = GetTagManager();
            SerializedProperty tags = tagManager.FindProperty("tags");
            tags.InsertArrayElementAtIndex(tags.arraySize);
            tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = tagName;
            tagManager.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void EnsureLayer(string layerName)
        {
            if (LayerMask.NameToLayer(layerName) >= 0)
            {
                return;
            }

            SerializedObject tagManager = GetTagManager();
            SerializedProperty layers = tagManager.FindProperty("layers");

            for (int index = 8; index < layers.arraySize; index++)
            {
                SerializedProperty layer = layers.GetArrayElementAtIndex(index);

                if (!string.IsNullOrEmpty(layer.stringValue))
                {
                    continue;
                }

                layer.stringValue = layerName;
                tagManager.ApplyModifiedPropertiesWithoutUndo();
                return;
            }

            Debug.LogWarning($"No user Layer slot is available for {layerName}.");
        }

        private static SerializedObject GetTagManager()
        {
            Object tagManager = AssetDatabase.LoadAllAssetsAtPath(TagManagerPath)[0];
            return new SerializedObject(tagManager);
        }

        private static void EnsureFolder(string assetPath)
        {
            string[] folders = assetPath.Split('/');
            string currentPath = folders[0];

            for (int index = 1; index < folders.Length; index++)
            {
                string nextPath = currentPath + "/" + folders[index];

                if (!AssetDatabase.IsValidFolder(nextPath))
                {
                    AssetDatabase.CreateFolder(currentPath, folders[index]);
                }

                currentPath = nextPath;
            }
        }

        private static void AddSceneToBuildSettings(string scenePath)
        {
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(
                EditorBuildSettings.scenes
            );

            foreach (EditorBuildSettingsScene buildScene in scenes)
            {
                if (buildScene.path == scenePath)
                {
                    buildScene.enabled = true;
                    EditorBuildSettings.scenes = scenes.ToArray();
                    return;
                }
            }

            scenes.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = scenes.ToArray();
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
