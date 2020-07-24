using System;
using System.Linq;

namespace DungeonGeneration {
    public static class DungeonLayoutGeneration {
        public enum Tile {
            Wall, Enemies, Treasure, MiniBoss, Special
        }

        public static Tile[,] GenerateDungeonLayout(string seed) {
            (int width, int height) =  (9, 9);
            Tile[,] map = new Tile[width, height];

            (int centerx, int centery) = ((width+1)/2, (height+1)/2);

            // Generate a 3x3 grid of rooms in the middle of the map
            for (int neighbourX = centerx-1; neighbourX <= centerx+1; neighbourX++) {
                for (int neighbourY = centery-1; neighbourY <= centery+1; neighbourY++) {
                    map[neighbourX, neighbourY] = Tile.Enemies;
                }
            }

            // Pick a random spot on the perimeter of the square and add a 2x2 square on top for variation
            (int, int)[] visited = new (int, int)[3];
            for (int index = 0; index<3; index++) {
                (int x, int y) = (0, 0);
                #region Pick a random point on the perimeter of the 3x3 square
                {
                    int perimeter = 4*3 - 4;
                    int randomNumber = new System.Random(seed.GetHashCode()).Next(0, perimeter-1);

                    if (randomNumber==0) {
                        (x, y) = (centerx-1, centery-1);
                    } else {
                        for (int i = centerx-1; i<=centerx+1; i++) {
                            for (int j = centery-1; j<=centery+1; j++) {
                                if (i==centerx-1 || i==centerx+1 || j==centery-1 || j==centery+1) randomNumber--;
                                if (randomNumber==0) (x, y) = (i, j);
                            }
                        }
                    }
                }
                #endregion

                if (visited.Contains((x,y))) {
                    index--;
                    continue;
                }
                visited[index] = (x,y);

                if (map[x-1, y] == Tile.Wall) {
                    map[x-1, y] = Tile.Enemies;
                    map[x, y+1] = Tile.Enemies;
                    map[x-1, y+1] = Tile.Enemies;
                } else {
                    map[x+1, y] = Tile.Enemies;
                    map[x, y+1] = Tile.Enemies;
                    map[x+1, y+1] = Tile.Enemies;
                }
            }

            return map;
        }
    }
}