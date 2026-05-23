using System.Collections.Generic;
using System.IO;
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
        private const string GeneratedSpritesFolder = "Assets/_Project/Sprites/Generated";
        private const string GeneratedVisualPrefabFolder = "Assets/_Project/Prefabs/VisualPlaceholders";
        private const string GeneratedSpriteMaterialPath = GeneratedSpritesFolder + "/M_PlaceholderSprites.mat";

        private static readonly Color DemoGroundColor = new Color(0.19f, 0.31f, 0.23f, 1f);
        private static readonly Color DemoPlatformColor = new Color(0.25f, 0.41f, 0.29f, 1f);
        private static readonly Color DemoWallColor = new Color(0.09f, 0.14f, 0.12f, 1f);
        private static readonly Color DemoWalkableEdgeColor = new Color(0.38f, 0.56f, 0.38f, 1f);
        private static readonly Color DemoPlayerColor = new Color(0.22f, 0.62f, 1f, 1f);
        private static readonly Color DemoPlayerAccentColor = new Color(0.92f, 1f, 1f, 0.95f);
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
        private static Material generatedSpriteMaterial;
        private static Font legacyFont;
        private static bool generatedPlaceholderAssetsReady;

        private enum PlaceholderSpriteKind
        {
            Block,
            Ground,
            Platform,
            Wall,
            Trunk,
            TreeSilhouette,
            Fog,
            Checkpoint,
            DashGate,
            Enemy,
            Boss,
            Player,
            DemoEnd,
            Light,
            PitShadow
        }

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
                SortEnemy - 1,
                PlaceholderSpriteKind.Enemy
            );

            SpriteRenderer enemyRenderer = ConfigurePlaceholderSprite(
                enemy,
                DemoEnemyColor,
                new Vector2(1f, 1.45f),
                PlaceholderSpriteKind.Enemy
            );
            enemyRenderer.sortingOrder = SortEnemy;

            CreateVisualChild(
                "Enemy_ReadMarker",
                enemy.transform,
                new Vector3(0f, 0.98f, 0f),
                new Vector2(0.34f, 0.16f),
                DemoEnemyMarkerColor,
                SortEnemy + 1,
                PlaceholderSpriteKind.Light
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
                SortCheckpoint - 1,
                PlaceholderSpriteKind.Light
            );

            SpriteRenderer checkpointRenderer = ConfigurePlaceholderSprite(
                checkpoint,
                DemoCheckpointColor,
                new Vector2(0.9f, 2.2f),
                PlaceholderSpriteKind.Checkpoint
            );
            checkpointRenderer.sortingOrder = SortCheckpoint;

            CreateVisualChild(
                "Checkpoint_TopLight",
                checkpoint.transform,
                new Vector3(0f, 1.15f, 0f),
                new Vector2(0.62f, 0.32f),
                new Color(1f, 0.95f, 0.45f, 1f),
                SortCheckpoint + 1,
                PlaceholderSpriteKind.Light
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
                SortGate - 1,
                PlaceholderSpriteKind.Fog
            );
            CreateVisualChild(
                "Gate_CyanSeal",
                visual.transform,
                Vector3.zero,
                size,
                DemoDashGateColor,
                SortGate,
                PlaceholderSpriteKind.DashGate
            );
            CreateVisualChild(
                "Gate_PurpleLine_Left",
                visual.transform,
                new Vector3(-size.x * 0.38f, 0f, 0f),
                new Vector2(0.14f, size.y + 0.35f),
                DemoDashGateAccentColor,
                SortGate + 1,
                PlaceholderSpriteKind.DashGate
            );
            CreateVisualChild(
                "Gate_PurpleLine_Right",
                visual.transform,
                new Vector3(size.x * 0.38f, 0f, 0f),
                new Vector2(0.14f, size.y + 0.35f),
                DemoDashGateAccentColor,
                SortGate + 1,
                PlaceholderSpriteKind.DashGate
            );
            CreateVisualChild(
                "Gate_MemoryBand",
                visual.transform,
                Vector3.zero,
                new Vector2(size.x + 0.55f, 0.24f),
                new Color(0.92f, 0.96f, 1f, 0.52f),
                SortGate + 2,
                PlaceholderSpriteKind.Light
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
            int layer,
            PlaceholderSpriteKind spriteKind = PlaceholderSpriteKind.Block
        )
        {
            GameObject block = new GameObject(name);
            block.transform.SetParent(parent);
            block.transform.position = position;
            ConfigureLayer(block, layer);
            ConfigurePlaceholderSprite(block, color, size, spriteKind);
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

        private static SpriteRenderer ConfigurePlaceholderSprite(
            GameObject gameObject,
            Color color,
            Vector2 size,
            PlaceholderSpriteKind spriteKind = PlaceholderSpriteKind.Block
        )
        {
            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = GetGeneratedSprite(spriteKind);
            spriteRenderer.sharedMaterial = GetGeneratedSpriteMaterial();
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
            int sortingOrder,
            PlaceholderSpriteKind spriteKind = PlaceholderSpriteKind.Block
        )
        {
            GameObject visual = new GameObject(name);
            visual.transform.SetParent(parent);
            visual.transform.localPosition = localPosition;

            SpriteRenderer renderer = ConfigurePlaceholderSprite(visual, color, size, spriteKind);
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

        private static Sprite GetGeneratedSprite(PlaceholderSpriteKind spriteKind)
        {
            EnsureGeneratedPlaceholderAssets();

            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetGeneratedSpritePath(spriteKind));
            return sprite != null ? sprite : GetPlaceholderSprite();
        }

        private static Material GetGeneratedSpriteMaterial()
        {
            EnsureGeneratedPlaceholderAssets();
            return generatedSpriteMaterial;
        }

        private static void EnsureGeneratedPlaceholderAssets()
        {
            if (generatedPlaceholderAssetsReady)
            {
                return;
            }

            EnsureFolder(GeneratedSpritesFolder);
            EnsureFolder(GeneratedVisualPrefabFolder);

            foreach (PlaceholderSpriteKind spriteKind in GetGeneratedSpriteKinds())
            {
                EnsureGeneratedSpriteAsset(spriteKind);
            }

            EnsureGeneratedPlaceholderMaterial();
            EnsureGeneratedVisualPrefabs();
            generatedPlaceholderAssetsReady = true;
        }

        private static IEnumerable<PlaceholderSpriteKind> GetGeneratedSpriteKinds()
        {
            yield return PlaceholderSpriteKind.Block;
            yield return PlaceholderSpriteKind.Ground;
            yield return PlaceholderSpriteKind.Platform;
            yield return PlaceholderSpriteKind.Wall;
            yield return PlaceholderSpriteKind.Trunk;
            yield return PlaceholderSpriteKind.TreeSilhouette;
            yield return PlaceholderSpriteKind.Fog;
            yield return PlaceholderSpriteKind.Checkpoint;
            yield return PlaceholderSpriteKind.DashGate;
            yield return PlaceholderSpriteKind.Enemy;
            yield return PlaceholderSpriteKind.Boss;
            yield return PlaceholderSpriteKind.Player;
            yield return PlaceholderSpriteKind.DemoEnd;
            yield return PlaceholderSpriteKind.Light;
            yield return PlaceholderSpriteKind.PitShadow;
        }

        private static void EnsureGeneratedSpriteAsset(PlaceholderSpriteKind spriteKind)
        {
            string assetPath = GetGeneratedSpritePath(spriteKind);
            string fullPath = GetFullProjectPath(assetPath);

            if (!File.Exists(fullPath))
            {
                Texture2D texture = CreateGeneratedTexture(spriteKind);
                File.WriteAllBytes(fullPath, texture.EncodeToPNG());
                Object.DestroyImmediate(texture);
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceSynchronousImport);

            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (importer == null)
            {
                return;
            }

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.spritePixelsPerUnit = 64f;
            importer.mipmapEnabled = false;
            importer.alphaIsTransparency = true;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.filterMode = FilterMode.Bilinear;
            importer.spriteBorder = new Vector4(10f, 10f, 10f, 10f);

            TextureImporterSettings textureSettings = new TextureImporterSettings();
            importer.ReadTextureSettings(textureSettings);
            textureSettings.spriteMeshType = SpriteMeshType.FullRect;
            importer.SetTextureSettings(textureSettings);

            importer.SaveAndReimport();
        }

        private static void EnsureGeneratedPlaceholderMaterial()
        {
            generatedSpriteMaterial = AssetDatabase.LoadAssetAtPath<Material>(GeneratedSpriteMaterialPath);

            if (generatedSpriteMaterial != null)
            {
                return;
            }

            Shader shader = Shader.Find("Sprites/Default");

            if (shader == null)
            {
                shader = Shader.Find("Unlit/Transparent");
            }

            generatedSpriteMaterial = new Material(shader)
            {
                name = "M_PlaceholderSprites"
            };
            AssetDatabase.CreateAsset(generatedSpriteMaterial, GeneratedSpriteMaterialPath);
        }

        private static void EnsureGeneratedVisualPrefabs()
        {
            EnsureGeneratedVisualPrefab("PF_Visual_Ground", PlaceholderSpriteKind.Ground, DemoGroundColor, new Vector2(3f, 1f), SortGameplaySolid);
            EnsureGeneratedVisualPrefab("PF_Visual_Platform", PlaceholderSpriteKind.Platform, DemoPlatformColor, new Vector2(3f, 0.55f), SortGameplaySolid);
            EnsureGeneratedVisualPrefab("PF_Visual_Wall", PlaceholderSpriteKind.Wall, DemoWallColor, new Vector2(1f, 3f), SortGameplaySolid);
            EnsureGeneratedVisualPrefab("PF_Visual_Trunk", PlaceholderSpriteKind.Trunk, new Color(0.12f, 0.24f, 0.14f, 0.55f), new Vector2(1.2f, 5f), SortBackground);
            EnsureGeneratedVisualPrefab("PF_Visual_TreeSilhouette", PlaceholderSpriteKind.TreeSilhouette, new Color(0.08f, 0.2f, 0.13f, 0.4f), new Vector2(4f, 3f), SortBackground);
            EnsureGeneratedVisualPrefab("PF_Visual_Fog", PlaceholderSpriteKind.Fog, new Color(0.55f, 0.9f, 0.95f, 0.14f), new Vector2(4f, 1.2f), SortFog);
            EnsureGeneratedVisualPrefab("PF_Visual_Checkpoint", PlaceholderSpriteKind.Checkpoint, DemoCheckpointColor, new Vector2(0.9f, 2.2f), SortCheckpoint);
            EnsureGeneratedVisualPrefab("PF_Visual_DashGate", PlaceholderSpriteKind.DashGate, DemoDashGateColor, new Vector2(1.2f, 4f), SortGate);
            EnsureGeneratedVisualPrefab("PF_Visual_BasicEnemy", PlaceholderSpriteKind.Enemy, DemoEnemyColor, new Vector2(1f, 1.45f), SortEnemy);
            EnsureGeneratedVisualPrefab("PF_Visual_Lucarelli", PlaceholderSpriteKind.Boss, DemoBossColor, new Vector2(1.35f, 2.1f), SortBoss);
            EnsureGeneratedVisualPrefab("PF_Visual_Rubens", PlaceholderSpriteKind.Player, DemoPlayerColor, new Vector2(0.9f, 1.6f), SortPlayer);
            EnsureGeneratedVisualPrefab("PF_Visual_DemoEnd", PlaceholderSpriteKind.DemoEnd, new Color(0.95f, 0.8f, 0.25f, 0.8f), new Vector2(1.6f, 2.7f), SortCheckpoint);
        }

        private static void EnsureGeneratedVisualPrefab(
            string name,
            PlaceholderSpriteKind spriteKind,
            Color color,
            Vector2 size,
            int sortingOrder
        )
        {
            string prefabPath = GeneratedVisualPrefabFolder + "/" + name + ".prefab";

            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                return;
            }

            GameObject visual = new GameObject(name);
            SpriteRenderer renderer = visual.AddComponent<SpriteRenderer>();
            renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GetGeneratedSpritePath(spriteKind));
            renderer.sharedMaterial = generatedSpriteMaterial;
            renderer.color = color;
            renderer.drawMode = SpriteDrawMode.Sliced;
            renderer.size = size;
            renderer.sortingOrder = sortingOrder;

            PrefabUtility.SaveAsPrefabAsset(visual, prefabPath);
            Object.DestroyImmediate(visual);
        }

        private static string GetGeneratedSpritePath(PlaceholderSpriteKind spriteKind)
        {
            return GeneratedSpritesFolder + "/" + GetGeneratedSpriteFileName(spriteKind) + ".png";
        }

        private static string GetGeneratedSpriteFileName(PlaceholderSpriteKind spriteKind)
        {
            switch (spriteKind)
            {
                case PlaceholderSpriteKind.Ground:
                    return "SP_Placeholder_Ground";
                case PlaceholderSpriteKind.Platform:
                    return "SP_Placeholder_Platform";
                case PlaceholderSpriteKind.Wall:
                    return "SP_Placeholder_Wall";
                case PlaceholderSpriteKind.Trunk:
                    return "SP_Placeholder_Trunk";
                case PlaceholderSpriteKind.TreeSilhouette:
                    return "SP_Placeholder_TreeSilhouette";
                case PlaceholderSpriteKind.Fog:
                    return "SP_Placeholder_Fog";
                case PlaceholderSpriteKind.Checkpoint:
                    return "SP_Placeholder_Checkpoint";
                case PlaceholderSpriteKind.DashGate:
                    return "SP_Placeholder_DashGate";
                case PlaceholderSpriteKind.Enemy:
                    return "SP_Placeholder_BasicEnemy";
                case PlaceholderSpriteKind.Boss:
                    return "SP_Placeholder_Lucarelli";
                case PlaceholderSpriteKind.Player:
                    return "SP_Placeholder_Rubens";
                case PlaceholderSpriteKind.DemoEnd:
                    return "SP_Placeholder_DemoEnd";
                case PlaceholderSpriteKind.Light:
                    return "SP_Placeholder_Light";
                case PlaceholderSpriteKind.PitShadow:
                    return "SP_Placeholder_PitShadow";
                default:
                    return "SP_Placeholder_Block";
            }
        }

        private static string GetFullProjectPath(string assetPath)
        {
            DirectoryInfo assetsDirectory = Directory.GetParent(Application.dataPath);
            return Path.Combine(
                assetsDirectory.FullName,
                assetPath.Replace('/', Path.DirectorySeparatorChar)
            );
        }

        private static Texture2D CreateGeneratedTexture(PlaceholderSpriteKind spriteKind)
        {
            const int textureSize = 96;
            Texture2D texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
            Color[] pixels = new Color[textureSize * textureSize];

            for (int y = 0; y < textureSize; y++)
            {
                for (int x = 0; x < textureSize; x++)
                {
                    float u = x / (textureSize - 1f);
                    float v = y / (textureSize - 1f);
                    pixels[y * textureSize + x] = GetGeneratedPixel(spriteKind, x, y, u, v);
                }
            }

            texture.SetPixels(pixels);
            texture.Apply(false, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;
            return texture;
        }

        private static Color GetGeneratedPixel(
            PlaceholderSpriteKind spriteKind,
            int x,
            int y,
            float u,
            float v
        )
        {
            float noise = Hash01(x, y);
            float edge = Mathf.Min(Mathf.Min(u, 1f - u), Mathf.Min(v, 1f - v));
            float border = edge < 0.055f ? 1f : 0f;

            switch (spriteKind)
            {
                case PlaceholderSpriteKind.Ground:
                    return ApplyBorder(
                        Color.Lerp(new Color(0.12f, 0.22f, 0.16f, 1f), new Color(0.24f, 0.38f, 0.25f, 1f), v),
                        new Color(0.08f, 0.13f, 0.1f, 1f),
                        border,
                        noise,
                        v > 0.78f ? 0.16f : 0.04f
                    );
                case PlaceholderSpriteKind.Platform:
                    return ApplyBorder(
                        Color.Lerp(new Color(0.18f, 0.3f, 0.19f, 1f), new Color(0.33f, 0.49f, 0.31f, 1f), v),
                        new Color(0.08f, 0.14f, 0.1f, 1f),
                        border,
                        noise,
                        v > 0.72f ? 0.14f : 0.03f
                    );
                case PlaceholderSpriteKind.Wall:
                    return ApplyBorder(
                        Color.Lerp(new Color(0.04f, 0.08f, 0.07f, 1f), new Color(0.1f, 0.15f, 0.12f, 1f), v),
                        new Color(0.02f, 0.035f, 0.035f, 1f),
                        border,
                        noise,
                        0.05f
                    );
                case PlaceholderSpriteKind.Trunk:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.06f, 0.12f, 0.08f, 1f), new Color(0.16f, 0.24f, 0.13f, 1f), v)
                            * (0.88f + Mathf.Sin(u * 38f) * 0.08f),
                        RoundedMask(u, v, 0.36f, 0.5f)
                    );
                case PlaceholderSpriteKind.TreeSilhouette:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.04f, 0.12f, 0.08f, 1f), new Color(0.16f, 0.3f, 0.17f, 1f), v),
                        BlobMask(u, v)
                    );
                case PlaceholderSpriteKind.Fog:
                    return ApplyAlpha(
                        new Color(0.72f, 0.94f, 0.96f, 1f),
                        Mathf.Clamp01(0.34f * Mathf.Sin(Mathf.PI * v) * (0.72f + Mathf.Sin(u * 18f) * 0.18f))
                    );
                case PlaceholderSpriteKind.Checkpoint:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.84f, 0.53f, 0.1f, 1f), new Color(1f, 0.92f, 0.38f, 1f), v),
                        BeaconMask(u, v)
                    );
                case PlaceholderSpriteKind.DashGate:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.13f, 0.95f, 1f, 1f), new Color(0.75f, 0.24f, 1f, 1f), Mathf.Abs(u - 0.5f) * 1.6f),
                        Mathf.Clamp01(0.42f + border * 0.45f + Mathf.Sin(u * 45f) * 0.1f)
                    );
                case PlaceholderSpriteKind.Enemy:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.35f, 0.05f, 0.52f, 1f), new Color(0.84f, 0.24f, 0.96f, 1f), v),
                        EnemyMask(u, v)
                    );
                case PlaceholderSpriteKind.Boss:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.48f, 0.04f, 0.06f, 1f), new Color(1f, 0.26f, 0.12f, 1f), v),
                        BossMask(u, v)
                    );
                case PlaceholderSpriteKind.Player:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.08f, 0.34f, 0.72f, 1f), new Color(0.4f, 0.88f, 1f, 1f), v),
                        PlayerMask(u, v)
                    );
                case PlaceholderSpriteKind.DemoEnd:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.8f, 0.55f, 0.08f, 1f), new Color(1f, 0.94f, 0.42f, 1f), v),
                        BeaconMask(u, v)
                    );
                case PlaceholderSpriteKind.Light:
                    return ApplyAlpha(
                        Color.Lerp(new Color(0.85f, 1f, 0.9f, 1f), new Color(1f, 0.88f, 0.35f, 1f), v),
                        Mathf.Clamp01(1f - Mathf.Pow(Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.5f)) * 1.9f, 1.4f))
                    );
                case PlaceholderSpriteKind.PitShadow:
                    return ApplyAlpha(
                        new Color(0.015f, 0.01f, 0.025f, 1f),
                        Mathf.Clamp01(0.8f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.45f)))
                    );
                default:
                    return ApplyBorder(
                        Color.Lerp(new Color(0.22f, 0.28f, 0.28f, 1f), new Color(0.34f, 0.42f, 0.4f, 1f), v),
                        new Color(0.08f, 0.11f, 0.11f, 1f),
                        border,
                        noise,
                        0.04f
                    );
            }
        }

        private static Color ApplyBorder(Color baseColor, Color borderColor, float border, float noise, float noiseStrength)
        {
            Color color = Color.Lerp(baseColor, borderColor, border);
            float variation = (noise - 0.5f) * noiseStrength;
            color.r = Mathf.Clamp01(color.r + variation);
            color.g = Mathf.Clamp01(color.g + variation);
            color.b = Mathf.Clamp01(color.b + variation);
            return color;
        }

        private static Color ApplyAlpha(Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha);
            return color;
        }

        private static float RoundedMask(float u, float v, float width, float height)
        {
            float x = Mathf.Abs(u - 0.5f) / width;
            float y = Mathf.Abs(v - 0.5f) / height;
            return Mathf.Clamp01(1.2f - Mathf.Max(x, y));
        }

        private static float BlobMask(float u, float v)
        {
            float baseBlob = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.53f)) * 1.55f;
            float leftBlob = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.31f, 0.48f)) * 2.1f;
            float rightBlob = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.68f, 0.47f)) * 2.1f;
            return Mathf.Clamp01(Mathf.Max(baseBlob, Mathf.Max(leftBlob, rightBlob)));
        }

        private static float BeaconMask(float u, float v)
        {
            float pillar = RoundedMask(u, v, 0.2f, 0.46f);
            float top = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.75f)) * 3.2f;
            return Mathf.Clamp01(Mathf.Max(pillar, top));
        }

        private static float EnemyMask(float u, float v)
        {
            float body = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.44f)) * 1.9f;
            float head = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.72f)) * 3.2f;
            return Mathf.Clamp01(Mathf.Max(body, head));
        }

        private static float BossMask(float u, float v)
        {
            float torso = RoundedMask(u, v, 0.26f, 0.42f);
            float shoulders = 1f - Mathf.Abs(v - 0.62f) * 8f - Mathf.Abs(u - 0.5f) * 1.4f;
            float crest = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.86f)) * 4.2f;
            return Mathf.Clamp01(Mathf.Max(torso, Mathf.Max(shoulders, crest)));
        }

        private static float PlayerMask(float u, float v)
        {
            float body = RoundedMask(u, v, 0.24f, 0.43f);
            float head = 1f - Vector2.Distance(new Vector2(u, v), new Vector2(0.5f, 0.78f)) * 3.8f;
            float scarf = 1f - Mathf.Abs(v - 0.55f) * 18f - Mathf.Abs(u - 0.67f) * 5f;
            return Mathf.Clamp01(Mathf.Max(body, Mathf.Max(head, scarf)));
        }

        private static float Hash01(int x, int y)
        {
            unchecked
            {
                int hash = x * 73856093 ^ y * 19349663;
                hash = (hash << 13) ^ hash;
                return ((hash * (hash * hash * 15731 + 789221) + 1376312589) & 0x7fffffff) / 2147483647f;
            }
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
