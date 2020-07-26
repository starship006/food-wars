using System;
using System.Linq;
using Console = UnityEngine.Debug;

namespace DungeonGeneration {
    public static class DungeonLayoutGeneration {
        public enum Tile {
            Wall, Enemies, MiniBoss, StartEnd
        }

        public static Tile[,] GenerateDungeonLayout(string seed) {
            int width = 11;
            int height = 11;

            int centerx = width/2 + 1;
            int centery = height/2 + 1;

            Tile[,] map = new Tile[width, height];
            int borderLength = 0;
            
            for (int x = centerx-1; x <= centerx+1; x++) {
                for (int y = centery-1; y <= centery+1; y++) {
                    map[x, y] = Tile.Enemies;
                    if(x!=y) {
                        borderLength++;
                    }
                }
            }

            System.Random random = new System.Random(seed.GetHashCode());
            for (int i = 0; i<3; i++) {
                int r = random.Next(1, borderLength);
                for(int x=1; x<width-1; x++) {
                    for(int y=1; y<height-1; y++) {
                        string edges = "";
                        edges += (map[x-1, y] == Tile.Wall) ? "l" : "";
                        edges += (map[x+1, y] == Tile.Wall) ? "r" : "";
                        edges += (map[x, y+1] == Tile.Wall) ? "u" : "";
                        edges += (map[x, y-1] == Tile.Wall) ? "d" : "";
                        if (edges.Length != 0 && edges.Length != 4 && map[x,y] != Tile.Wall) r--;
                        if (r==0) {
                            Console.Log(edges.Length);

                            int xadd = (x<centerx) ? -1 : 1;
                            int yadd = (y<centery) ? -1 : 1;

                            switch (edges.Length) {
                                case 1:
                                    bool extrude = random.Next(0, 2) < 1;
                                    if(extrude) {
                                        switch (edges) {
                                            case "l": map[x-1, y] = Tile.Enemies; break;
                                            case "r": map[x+1, y] = Tile.Enemies; break;
                                            case "u": map[x, y+1] = Tile.Enemies; break;
                                            case "d": map[x, y-1] = Tile.Enemies; break;
                                        };
                                        borderLength++;
                                        break;
                                    } else {
                                        goto case 2;
                                    }
                                case 2:
                                    map[x, y+yadd] = Tile.Enemies;
                                    map[x+xadd, y] = Tile.Enemies;
                                    map[x+xadd, y+yadd] = Tile.Enemies;
                                    borderLength+=3;
                                    break;
                                case 3:
                                    if(edges.Contains("lr")) {
                                        edges = string.Join("", edges.Split('l', 'r'));
                                    } else {
                                        edges = string.Join("", edges.Split('u', 'd'));
                                    }
                                    
                                    switch (edges) {
                                        case "l": map[x-1, y] = Tile.Enemies; break;
                                        case "r": map[x+1, y] = Tile.Enemies; break;
                                        case "u": map[x, y+1] = Tile.Enemies; break;
                                        case "d": map[x, y-1] = Tile.Enemies; break;
                                    };
                                    borderLength++;
                                    break;
                            }
                        }
                    }
                }
            }

            return map;
        }
    }
}