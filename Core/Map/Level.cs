using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TBoGV;

public class Level
{
    protected int Size;
    protected int RoomCount;
    protected Room[,] RoomMap;
    protected Vector2 StartPos;
    protected Player Player;

    public Level(Player player, uint size, int roomCount, Vector2 startPos)
    {
        if (startPos.X > size || startPos.Y > size)
            throw new ArgumentOutOfRangeException("The startPos is not in the level");
        this.StartPos = startPos;
        this.Size = (int)size;
        this.RoomCount = roomCount;
        this.Player = player;
        LevelCreator.GenerateLevel(Size, RoomCount, StartPos, out RoomMap, Player);
    }

    public Level(Player player, uint size, int roomCounts) : this(player, size, roomCounts, new Vector2(size / 2, 0)) { }


}

public static class LevelCreator
{

    private static List<RoomCandidate> Candidates = new List<RoomCandidate>();
    private static int Size;

    public static void GenerateLevel(int size, int roomCount, Vector2 startPos, out Room[,] roomMap, Player player)
    {
        LevelCreator.Size = size;
        roomMap = new Room[size, size];
        RoomCandidate[,] candidateMap = new RoomCandidate[size, size];
        int roomsPlaced = 0;

        RoomCandidate startRoom = new RoomCandidate(startPos, Directions.ENTRY, null, player);
        candidateMap[(int)startPos.X, (int)startPos.Y] = startRoom;
        roomsPlaced++;

        AddCandidate(new RoomCandidate(new Vector2(startPos.X + 1, startPos.Y), Directions.LEFT, startRoom, player));
        AddCandidate(new RoomCandidate(new Vector2(startPos.X - 1, startPos.Y), Directions.RIGHT, startRoom, player));
        AddCandidate(new RoomCandidate(new Vector2(startPos.X, startPos.Y + 1), Directions.UP, startRoom, player));
        AddCandidate(new RoomCandidate(new Vector2(startPos.X, startPos.Y - 1), Directions.DOWN, startRoom, player));

        while (roomsPlaced < roomCount)
        {
            Random rand = new Random();
            RoomCandidate rCan = Candidates[rand.Next(Candidates.Count)];
            RoomCandidate rGet = candidateMap[(int)rCan.Position.X, (int)rCan.Position.Y];
            if (rGet == null && IsValidCandidate(rCan))
            {
                candidateMap[(int)rCan.Position.X, (int)rCan.Position.Y] = rCan;
                roomsPlaced++;
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X + 1, rCan.Position.Y), Directions.LEFT, rCan, player));
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X - 1, rCan.Position.Y), Directions.RIGHT, rCan, player));
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X, rCan.Position.Y + 1), Directions.UP, rCan, player));
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X, rCan.Position.Y - 1), Directions.DOWN, rCan, player));
                Directions d;
                if (rCan.GeneratedFromRoom != null)
                {
                    switch (rCan.GeneratedFromDir)
                    {
                        case Directions.LEFT:
                            d = Directions.RIGHT;
                            break;
                        case Directions.RIGHT:
                            d = Directions.LEFT;
                            break;
                        case Directions.UP:
                            d = Directions.DOWN;
                            break;
                        case Directions.DOWN:
                            d = Directions.UP;
                            break;
                        default:
                            d = Directions.ENTRY;
                            break;
                    }
                    rCan.GeneratedFromRoom.DoorDirectons.Add(d);
                }
            }
            else
            {
                //if (!rGet.DoorDirectons.Contains(rCan.DoorDirectons[0])) roomMap[(int)rCan.Position.X, (int)rCan.Position.Y].DoorDirectons.Add(rCan.DoorDirectons[0]);
            }

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (candidateMap[x, y] != null)
                        Console.Write((int)candidateMap[x, y].DoorDirectons[0] + " ");
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
        }

    }

    private static void AddCandidate(RoomCandidate r)
    {
        if (IsValidCandidate(r))
            Candidates.Add(r);
    }
    private static bool IsValidCandidate(RoomCandidate r)
    {
        return (r.Position.X < LevelCreator.Size && r.Position.Y < LevelCreator.Size && r.Position.X >= 0 && r.Position.Y >= 0) == true;
    }

    private class RoomCandidate : Room
    {
        public Directions GeneratedFromDir;
        public Room GeneratedFromRoom;
        public RoomCandidate(Vector2 pos, Directions from, Room gen, Player p) : base(Vector2.One, pos, from, p)
        {
            GeneratedFromDir = from;
            GeneratedFromRoom = gen;
        }
        public override void GenerateRoom() { }

        protected override void GenerateEnemies() { }
    }
}