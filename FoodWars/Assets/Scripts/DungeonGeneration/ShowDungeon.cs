using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLG = DungeonGeneration.DungeonLayoutGeneration;

[Serializable]
public class ShowDungeon : MonoBehaviour {
    [SerializeField]
    GameObject dungeonRoomVisualPrefab;

    void Start() {
        int width = 50;
        int height = 25;

        DLG.Map map = new DLG.Map(width, height);
        DLG.GenerateDungeonLayout(ref map, Time.time+"");

        DrawDungeon(in map);
    }

    void DrawDungeon(in DLG.Map map) {
        for(int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                UnityEngine.Color color = (map.rooms[x,y] == DLG.RoomTypes.Wall) ? UnityEngine.Color.black : UnityEngine.Color.white;
                Vector3 pos = new Vector3(-0.5f * (map.width - 1) + x, -0.5f * (map.height - 1) + y, 0);
                GameObject roomVisual = Instantiate(dungeonRoomVisualPrefab, pos, Quaternion.identity);
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
