using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RLG = DungeonGeneration.RoomLayoutGeneration;

[Serializable]
public class ShowRoom : MonoBehaviour {
    [SerializeField]
    GameObject tilePrefab;

    void Start() {
        int width = 50;
        int height = 25;

        RLG.Layout room = new RLG.Layout(width, height);
        RLG.GenerateRoomLayout(ref room, Time.time+"");

        DrawDungeon(in room);
    }

    void DrawDungeon(in RLG.Layout room) {
        for (int x = 0; x < room.width; x++) {
            for (int y = 0; y < room.height; y++) {
                UnityEngine.Color color = (room.tiles[x, y] == RLG.TileTypes.Wall) ? UnityEngine.Color.black : UnityEngine.Color.white;
                Vector3 pos = new Vector3(-0.5f * (room.width - 1) + x, -0.5f * (room.height - 1) + y, 0);
                GameObject roomVisual = Instantiate(tilePrefab, pos, Quaternion.identity);
                roomVisual.transform.parent = this.transform;
                roomVisual.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    void Update() {
        // Generate a new room when the  space bar is pressed
        if (Input.GetMouseButtonDown(0)) Start();
    }


}
