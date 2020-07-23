using System.Linq;

namespace DungeonGeneration {
    public static class DungeonLayoutGeneration {
        public enum RoomTypes {
            EmptyRoom, Wall, Enemies, Treasure, MiniBoss, Special
        }

        public struct Map {
            public RoomTypes[,] rooms;

            public readonly int width;
            public readonly int height;

            public Map(int width,int height) {
                this.width = width;
                this.height = height;
                this.rooms = new RoomTypes[width,height];
            }
        }

        public static void GenerateDungeonLayout(ref Map map, string seed) {
            DungeonShapeGeneration.GenerateDungeonShape(ref map, seed);
            
        }

        // Get the number rooms of certain types in an nxn square centered at some location 
        static int CountNeighborsOfTypes(in Map map, (int x, int y) location, int distance, params RoomTypes[] roomTypes) {
            int count = 0;
            for (int neighbourX = location.x - distance; neighbourX <= location.x + distance; neighbourX++) {
                for (int neighbourY = location.y - distance; neighbourY <= location.y + distance; neighbourY++) {
                    if ((neighbourX, neighbourY) == location) continue;
                    if (neighbourX >= 0 && neighbourX < map.width && neighbourY >= 0 && neighbourY < map.height) {
                        count += roomTypes.Contains(map.rooms[neighbourX, neighbourY]) ? 1 : 0;
                    } else {
                        // Edges are counted as walls
                        count++;
                    }
                }
            }
            return count;
        }

        private static class DungeonShapeGeneration {
            public static void GenerateDungeonShape(ref Map map, string seed) {

            }

            public static void GenerateGridOfRooms(ref Map map, (int x, int y) location, int size) {

            }
        }
    }
}