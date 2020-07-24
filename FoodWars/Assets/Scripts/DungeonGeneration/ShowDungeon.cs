using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DLG = DungeonGeneration.DungeonLayoutGeneration;

[Serializable]
public class ShowDungeon : MonoBehaviour {
    [SerializeField]
    GameObject dungeonRoomVisualPrefab;

    void Start() {
        var map = DLG.GenerateDungeonLayout(Time.time+"");

        DrawDungeon(in map);
    }

    void DrawDungeon(in DLG.Tile[,] map) {
        for(int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                UnityEngine.Color color = (map[x,y] == DLG.Tile.Wall) ? UnityEngine.Color.black : UnityEngine.Color.white;
                Vector3 pos = new Vector3(-0.5f * (map.GetLength(0) - 1) + x, -0.5f * (map.GetLength(1) - 1) + y, 0);
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
