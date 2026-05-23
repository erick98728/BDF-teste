using System.Collections.Generic;
using Tester.Enemies;
using Tester.Interactables;
using Tester.Player;
using Tester.World;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Tester.Editor
{
    public static partial class PrototypeSceneBuilder
    {
        private const int SortRegionMarker = -30;
        private const int SortBackground = -24;
        private const int SortPitShadow = -18;
        private const int SortLandmark = -16;
        private const int SortLight = -12;
        private const int SortFog = -8;
        private const int SortDeathZone = -6;
        private const int SortGameplaySolid = 0;
        private const int SortGameplayEdge = 1;
        private const int SortGate = 6;
        private const int SortCheckpoint = 8;
        private const int SortPlayer = 10;
        private const int SortEnemy = 11;
        private const int SortBoss = 13;
        private const int SortSign = 15;

        private static readonly Color DemoGroundColor = new Color(0.19f, 0.31f, 0.23f, 1f);
        private static readonly Color DemoPlatformColor = new Color(0.25f, 0.41f, 0.29f, 1f);
        private static readonly Color DemoWallColor = new Color(0.09f, 0.14f, 0.12f, 1f);
        private static readonly Color DemoWalkableEdgeColor = new Color(0.38f, 0.56f, 0.38f, 1f);
        private static readonly Color DemoEnemyColor = new Color(0.66f, 0.16f, 0.78f, 1f);
        private static readonly Color DemoEnemyOutlineColor = new Color(0.13f, 0.04f, 0.17f, 1f);
        private static readonly Color DemoEnemyMarkerColor = new Color(1f, 0.72f, 0.95f, 0.92f);
        private static readonly Color DemoCheckpointColor = new Color(1f, 0.82f, 0.22f, 0.96f);
        private static readonly Color DemoCheckpointGlowColor = new Color(1f, 0.86f, 0.28f, 0.28f);
        private static readonly Color DemoDashGateColor = new Color(0.22f, 0.92f, 1f, 0.52f);
        private static readonly Color DemoDashGateAccentColor = new Color(0.72f, 0.32f, 1f, 0.42f);
        private static readonly Color DemoBossColor = new Color(0.96f, 0.16f, 0.12f, 1f);
        private static readonly Color DemoBossGlowColor = new Color(1f, 0.36f, 0.12f, 0.28f);

        private static Sprite placeholderSprite;
        private static Font legacyFont;

        private static void CreateBasicEnemy(
            string name,
            Transform parent,
            Vector2 position,
            int enemyLayer
        )
        {
            GameObject enemy = new GameObject(name);
            enemy.transform.SetParent(parent);
            enemy.transform.position = position;
            ConfigureLayer(enemy, enemyLayer);
            CreateVisualChild(
                "Enemy_Outline",
                enemy.transform,
                Vector3.zero,
                new Vector2(1.16f, 1.6f),
                DemoEnemyOutlineColor,
                SortEnemy - 1
            );

            SpriteRenderer enemyRenderer = ConfigurePlaceholderSprite(
                enemy,
                DemoEnemyColor,
                new Vector2(1f, 1.45f)
            );
            enemyRenderer.sortingOrder = SortEnemy;

            CreateVisualChild(
                "Enemy_ReadMarker",
                enemy.transform,
                new Vector3(0f, 0.98f, 0f),
                new Vector2(0.34f, 0.16f),
                DemoEnemyMarkerColor,
                SortEnemy + 1
            );

            Rigidbody2D body = enemy.AddComponent<Rigidbody2D>();
            ConfigureDynamicBody(body, 3f);

            ConfigureBoxCollider2D(enemy, new Vector2(0.9f, 1.4f));

            enemy.AddComponent<BasicPatrolEnemy>();
        }

        private static void CreateCheckpoint(Transform parent, int interactableLayer)
        {
            CreateCheckpoint(
                "Checkpoint_01",
                parent,
                new Vector2(-1.5f, -0.4f),
                interactableLayer
            );
        }

        private static void CreateCheckpoint(
            string name,
            Transform parent,
            Vector2 position,
            int interactableLayer
        )
        {
            GameObject checkpoint = new GameObject(name);
            checkpoint.transform.SetParent(parent);
            checkpoint.transform.position = position;
            ConfigureLayer(checkpoint, interactableLayer);
            ConfigureTagIfAvailable(checkpoint, "Interactable");
            CreateVisualChild(
                "Checkpoint_SafeGlow",
                checkpoint.transform,
                new Vector3(0f, 0.15f, 0f),
                new Vector2(2.4f, 2.9f),
                DemoCheckpointGlowColor,
                SortCheckpoint - 1
            );

            SpriteRenderer checkpointRenderer = ConfigurePlaceholderSprite(
                checkpoint,
                DemoCheckpointColor,
                new Vector2(0.9f, 2.2f)
            );
            checkpointRenderer.sortingOrder = SortCheckpoint;

            CreateVisualChild(
                "Checkpoint_TopLight",
                checkpoint.transform,
                new Vector3(0f, 1.15f, 0f),
                new Vector2(0.62f, 0.32f),
                new Color(1f, 0.95f, 0.45f, 1f),
                SortCheckpoint + 1
            );

            ConfigureBoxCollider2D(checkpoint, new Vector2(1f, 2f), true);

            Checkpoint checkpointComponent = checkpoint.AddComponent<Checkpoint>();
            Transform respawnPoint = CreateChildPoint(
                checkpoint.transform,
                "RespawnPoint",
                new Vector3(0f, 0.2f, 0f)
            );

            SetObjectReference(checkpointComponent, "respawnPoint", respawnPoint);
        }

        private static void CreateDashGate(
            Transform parent,
            int interactableLayer,
            AbilityManager abilities
        )
        {
            CreateDashGate(
                "DashGate_01",
                parent,
                new Vector2(13.3f, 0f),
                new Vector2(0.75f, 4f),
                interactableLayer,
                abilities
            );
        }

        private static void CreateDashGate(
            string name,
            Transform parent,
            Vector2 position,
            Vector2 size,
            int interactableLayer,
            AbilityManager abilities
        )
        {
            GameObject gate = new GameObject(name);
            gate.transform.SetParent(parent);
            gate.transform.position = position;
            ConfigureLayer(gate, interactableLayer);
            ConfigureTagIfAvailable(gate, "Interactable");

            BoxCollider2D blockingCollider = ConfigureBoxCollider2D(gate, size);

            GameObject visual = new GameObject("LockedVisual");
            visual.transform.SetParent(gate.transform);
            visual.transform.localPosition = Vector3.zero;

            CreateVisualChild(
                "Gate_Aura",
                visual.transform,
                Vector3.zero,
                new Vector2(size.x + 1.7f, size.y + 0.8f),
                new Color(0.15f, 0.82f, 1f, 0.2f),
                SortGate - 1
            );
            CreateVisualChild(
                "Gate_CyanSeal",
                visual.transform,
                Vector3.zero,
                size,
                DemoDashGateColor,
                SortGate
            );
            CreateVisualChild(
                "Gate_PurpleLine_Left",
                visual.transform,
                new Vector3(-size.x * 0.38f, 0f, 0f),
                new Vector2(0.14f, size.y + 0.35f),
                DemoDashGateAccentColor,
                SortGate + 1
            );
            CreateVisualChild(
                "Gate_PurpleLine_Right",
                visual.transform,
                new Vector3(size.x * 0.38f, 0f, 0f),
                new Vector2(0.14f, size.y + 0.35f),
                DemoDashGateAccentColor,
                SortGate + 1
            );
            CreateVisualChild(
                "Gate_MemoryBand",
                visual.transform,
                Vector3.zero,
                new Vector2(size.x + 0.55f, 0.24f),
                new Color(0.92f, 0.96f, 1f, 0.52f),
                SortGate + 2
            );

            AbilityGate abilityGate = gate.AddComponent<AbilityGate>();
            SetObjectReference(abilityGate, "abilityManager", abilities);
            SetObjectReference(abilityGate, "blockingCollider", blockingCollider);
            SetObjectReference(abilityGate, "lockedVisual", visual);
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
            ConfigureLayer(block, layer);
            ConfigurePlaceholderSprite(block, color, size);
            return block;
        }

        private static BoxCollider2D ConfigureBoxCollider2D(
            GameObject gameObject,
            Vector2 size,
            bool isTrigger = false
        )
        {
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = size;
            collider.isTrigger = isTrigger;
            return collider;
        }

        private static SpriteRenderer ConfigurePlaceholderSprite(GameObject gameObject, Color color, Vector2 size)
        {
            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = GetPlaceholderSprite();
            spriteRenderer.color = color;
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.size = size;
            return spriteRenderer;
        }

        private static GameObject CreateVisualChild(
            string name,
            Transform parent,
            Vector3 localPosition,
            Vector2 size,
            Color color,
            int sortingOrder
        )
        {
            GameObject visual = new GameObject(name);
            visual.transform.SetParent(parent);
            visual.transform.localPosition = localPosition;

            SpriteRenderer renderer = ConfigurePlaceholderSprite(visual, color, size);
            renderer.sortingOrder = sortingOrder;
            return visual;
        }

        private static Color WithAlpha(Color color, float alpha)
        {
            color.a = alpha;
            return color;
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

        private static GameObject CreateRootGroup(string name)
        {
            return new GameObject(name);
        }

        private static Transform CreateEmptyGroup(string name, Transform parent)
        {
            GameObject group = new GameObject(name);
            group.transform.SetParent(parent);
            return group.transform;
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

        private static void ConfigureLayer(GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
        }

        private static void ConfigureTagIfAvailable(GameObject gameObject, string tagName)
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
    }
}
