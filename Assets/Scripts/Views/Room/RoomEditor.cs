using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;

public class RoomEditor : MonoBehaviour {

    public EntityStorage EntityStorage;
    public TerrainStorage TerrainStorage;
    public SpriteStorage SpriteStorage;
    public MapGrid MapGrid;
    public CampaignController CampaignController;
    public GUIStyle CharacterWindowStyle;
    public GUIStyle CharacterImageStyle;

    ICollection<Ground> Terrains;
    ICollection<Entity> Entities;
    int EntityCount = 0;

    public Ground SelectedTerrain = null;

	// Use this for initialization
	void Start () {
        Terrains = TerrainStorage.GetTerrains();
        Entities = EntityStorage.GetEntities();
	}

    

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 150, 0, 150, 500));
        GUILayout.BeginVertical("box");
        GUILayout.Button("Set as active room");
        GUILayout.Label("Terrains:");
        GUILayout.BeginHorizontal("box");
        foreach (Ground terrain in Terrains)
        {
            if (GUILayout.Button(SpriteStorage.GetTexture(terrain.TextureFile), GUILayout.Width(30), GUILayout.Height(30)))
            {
                SelectedTerrain = terrain;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Entities:");
        GUILayout.BeginHorizontal("box");
        foreach (Entity entity in Entities)
        {
            if (GUILayout.Button(SpriteStorage.GetTexture(entity.Image), GUILayout.Width(30), GUILayout.Height(30)))
            {
                MapGrid.SelectClone(entity);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (modifyEntity != null)
        {
            GUI.Window(3, new Rect(Screen.width - 400, 0, 300, 240), EntityModWindow, "", CharacterWindowStyle);
        }

        if (choosingImage)
        {
            GUI.Window(4, new Rect(Screen.width - 600, 10, 400, 200), CharacterImageWindow, "", CharacterWindowStyle);
        }
    }

    bool choosingImage = false;
    int selectedImage = -1;
    Texture[] CharacterTextures;
    Texture[] CreatureTextures;
    Entity modifyEntity;

    void EntityModWindow(int id)
    {
        Entity entity = modifyEntity;

        GUI.Box(new Rect(0, 0, 300, 240), "");

        GUILayout.BeginArea(new Rect(0, 0, 300, 100), "");

        if (GUILayout.Button("Close"))
        {
            modifyEntity = null;
            choosingImage = false;
        }

        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

        if (GUILayout.Button(SpriteStorage.GetTexture(entity.Image), GUILayout.Width(70), GUILayout.Width(70), GUILayout.MaxHeight(70), GUILayout.MaxHeight(70)))
        {
            choosingImage = !choosingImage;
            selectedImage = -1;
        }

        if (choosingImage && selectedImage >= 0)
        {
            choosingImage = false;
            entity.Image = SpriteStorage.CharacterSprites[selectedImage];
        }

        GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        GUILayout.Label("Name");
        GUILayout.FlexibleSpace();
        entity.Name = GUILayout.TextField(entity.Name, GUILayout.MinWidth(50), GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Owner");
        GUILayout.FlexibleSpace();
        entity.Name = GUILayout.TextField(entity.Owner, GUILayout.MinWidth(50));
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.EndArea();

        Rect Left = new Rect(0, 100, 100, 40);
        Rect Right = new Rect(100, 100, 200, 40);
        int VerticalSpacing = 5;

        GUI.Label(Left, "Description");
        entity.Description = GUI.TextArea(Right, entity.Description);

        Left.y += Left.height + VerticalSpacing;
        Right.y += Left.height + VerticalSpacing;

        GUI.Label(Left, "GM Description");
        entity.GMDescription = GUI.TextArea(Right, entity.GMDescription);

        Left.y += Left.height + VerticalSpacing;
        Right.y += Left.height + VerticalSpacing;

        int width = entity.Width;
        GUI.Label(new Rect(Left.x, Left.y, 50, 20), "Width");
        int.TryParse(GUI.TextField(new Rect(Left.x + 50, Left.y, 50, 20), entity.Width.ToString()), out width);
        entity.Width = width;

        int height = entity.Height;
        GUI.Label(new Rect(Left.x + 150, Left.y, 50, 20), "Height");
        int.TryParse(GUI.TextField(new Rect(Left.x + 200, Left.y, 50, 20), entity.Height.ToString()), out height);
        entity.Height = width;

        Left.y += 20 + VerticalSpacing;
        Right.y += 20 + VerticalSpacing;

        if(GUI.Button(new Rect(Left.x, Left.y, Right.xMax, 20), "Save"))
        {
            CampaignController.SaveTempEntity(entity);
        }



    }



    void CharacterImageWindow(int windowID)
    {
        Texture[] textures;
        if (modifyEntity.Type == EntityType.PlayerCharacter)
        {
            if (CharacterTextures == null)
            {
                CharacterTextures = new Texture[SpriteStorage.CharacterSprites.Length];
                for (int i = 0; i < SpriteStorage.CharacterSprites.Length; i++)
                {
                    CharacterTextures[i] = SpriteStorage.GetTexture(SpriteStorage.CharacterSprites[i]);
                }
            }
            textures = CharacterTextures;
        }
        else
        {
            if (CreatureTextures == null)
            {
                CreatureTextures = new Texture[SpriteStorage.NPCSprites.Length];
                for (int i = 0; i < SpriteStorage.NPCSprites.Length; i++)
                {
                    CreatureTextures[i] = SpriteStorage.GetTexture(SpriteStorage.NPCSprites[i]);
                }
            }
            textures = CreatureTextures;
        }
        
        selectedImage = GUILayout.SelectionGrid(selectedImage, textures, 10, CharacterImageStyle);
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(1))
        {
            SelectedTerrain = null;
            if (MapGrid.MouseOverEntity != null)
            {
                modifyEntity = CampaignController.GetTempCopy(MapGrid.MouseOverEntity);
            }
        }
	}
}
