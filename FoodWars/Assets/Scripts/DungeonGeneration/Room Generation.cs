using System.Linq;

namespace DungeonGeneration {
    public static class RoomLayoutGeneration {
        public enum TileTypes {
            Empty, Wall, Chest
        }

        public struct Layout {
            public TileTypes[,] tiles;

            public readonly int width;
            public readonly int height;

            public Layout(int width, int height) {
                this.width = width;
                this.height = height;
                this.tiles = new TileTypes[width, height];
            }
        }

        public static void GenerateRoomLayout(ref Layout room, string seed) {
            RoomShapeGeneration.GenerateRoomShape(ref room, seed);

        }

        private static class RoomShapeGeneration {
            public static int randomFillPercent = 70;

            public static void GenerateRoomShape(ref Layout room, string seed, int smoothingPasses = 3) {
                Randomize(ref room, seed);

                for (int i = 0; i<smoothingPasses; i++) {
                    Smoothen(ref room);
                }
            }

            // Fill room randomly with rooms and walls
            static void Randomize(ref Layout room, string seed) {
                System.Random pseudoRandom = new System.Random(seed.GetHashCode());
                for (int x = 0; x < room.width; x++) {
                    for (int y = 0; y < room.height; y++) {
                        room.tiles[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? TileTypes.Empty : TileTypes.Wall;
                    }
                }
            }

            // Smooth out the randomness to form a blob
            static void Smoothen(ref Layout room) {
                for (int x = 0; x < room.width; x++) {
                    for (int y = 0; y < room.height; y++) {
                        int neighbourWallTiles = CountNeighborsOfTypes(in room, (x, y), 1, TileTypes.Wall);

                        // Cellular automata rules
                        if (neighbourWallTiles > 4) room.tiles[x, y] = TileTypes.Wall;
                        else if (neighbourWallTiles < 4) room.tiles[x, y] = TileTypes.Empty;
                    }
                }
            }
        }

        // Get the number rooms of certain types in an nxn square centered at some location 
        static int CountNeighborsOfTypes(in Layout room, (int x, int y) location, int distance, params TileTypes[] tileTypes) {
            int count = 0;
            for (int neighbourX = location.x - distance; neighbourX <= location.x + distance; neighbourX++) {
                for (int neighbourY = location.y - distance; neighbourY <= location.y + distance; neighbourY++) {
                    if ((neighbourX, neighbourY) == location) continue;
                    if (neighbourX >= 0 && neighbourX < room.width && neighbourY >= 0 && neighbourY < room.height) {
                        count += tileTypes.Contains(room.tiles[neighbourX, neighbourY]) ? 1 : 0;
                    } else {
                        // Edges are counted as walls
                        count++;
                    }
                }
            }
            return count;
        }
    }
}