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
        List<Room> rL = new List<Room> {
            new RoomEmpty(new Vector2(10, 10), player),
            new RoomEmpty(new Vector2(5, 10), player),
            new RoomEmpty(new Vector2(10, 5), player),
            new RoomEmpty(new Vector2(5, 5), player),
        };
        this.RoomMap = new LevelCreator(rL, 7, StartPos, Player).GenerateLevel();

    }

    public Level(Player player, uint size, int roomCounts) : this(player, size, roomCounts, new Vector2(size / 2, 0)) { }

}

public class LevelCreator
{

    private List<RoomCandidate> Candidates = new List<RoomCandidate>();
    private int Size;
    private int RoomCount;
    private Vector2 StartPos;
    private Room[,] RoomMap;
    private Player Player;


    public LevelCreator(List<Room> roomList, int size, Vector2 startPos, Player player)
    {
        this.Size = size;
        this.RoomCount = roomList.Count;
        this.StartPos = startPos;
        this.Player = player;
    }

    public Room[,] GenerateLevel()
    {
        RoomCandidate[,] candidateMap = PopulateCandidates();

        foreach (RoomCandidate c in candidateMap)
        {

        }
        return null;
    }

    private RoomCandidate[,] PopulateCandidates()
    {
        RoomCandidate[,] candidateMap = new RoomCandidate[Size, Size];
        int roomsPlaced = 0;

        RoomCandidate startRoom = new RoomCandidate(StartPos, Directions.ENTRY, null, Player);
        candidateMap[(int)StartPos.X, (int)StartPos.Y] = startRoom;
        roomsPlaced++;

        AddCandidate(new RoomCandidate(new Vector2(StartPos.X + 1, StartPos.Y), Directions.LEFT, startRoom, Player));
        AddCandidate(new RoomCandidate(new Vector2(StartPos.X - 1, StartPos.Y), Directions.RIGHT, startRoom, Player));
        AddCandidate(new RoomCandidate(new Vector2(StartPos.X, StartPos.Y + 1), Directions.UP, startRoom, Player));
        AddCandidate(new RoomCandidate(new Vector2(StartPos.X, StartPos.Y - 1), Directions.DOWN, startRoom, Player));

        while (roomsPlaced < RoomCount)
        {
            Random rand = new Random();
            RoomCandidate rCan = Candidates[rand.Next(Candidates.Count)];
            RoomCandidate rGet = candidateMap[(int)rCan.Position.X, (int)rCan.Position.Y];

            if (!IsValidCandidate(rCan))
                continue;

            if (rGet == null)
            {
                candidateMap[(int)rCan.Position.X, (int)rCan.Position.Y] = rCan;
                roomsPlaced++;
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X + 1, rCan.Position.Y), Directions.LEFT, rCan, Player));
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X - 1, rCan.Position.Y), Directions.RIGHT, rCan, Player));
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X, rCan.Position.Y + 1), Directions.UP, rCan, Player));
                AddCandidate(new RoomCandidate(new Vector2(rCan.Position.X, rCan.Position.Y - 1), Directions.DOWN, rCan, Player));
            }
            else
            {
                if (!rGet.DoorDirections.Contains(rCan.GeneratedFromDir))
                    rGet.DoorDirections.Add(rCan.GeneratedFromDir);
            }
            Candidates.Remove(rCan);

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
                if (!rCan.GeneratedFromRoom.DoorDirections.Contains(d))
                    rCan.GeneratedFromRoom.DoorDirections.Add(d);
            }
            PrintMap(candidateMap);
        }

        return candidateMap;
    }

    private void AddCandidate(RoomCandidate r)
    {
        if (IsValidCandidate(r))
            Candidates.Add(r);
    }
    private bool IsValidCandidate(RoomCandidate r)
    {
        return (r.Position.X < Size && r.Position.Y < Size && r.Position.X >= 0 && r.Position.Y >= 0) == true;
    }

    private void PrintMap(RoomCandidate[,] map)
    {
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (map[x, y] != null)
                    Console.Write((int)map[x, y].DoorDirections[0] + " ");
                else
                    Console.Write(". ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private class RoomCandidate
    {
        public Directions GeneratedFromDir;
        public RoomCandidate GeneratedFromRoom;
        public List<Directions> DoorDirections = new List<Directions>();
        public Vector2 Position;
        public RoomCandidate(Vector2 pos, Directions from, RoomCandidate gen, Player p)
        {
            Position = pos;
            GeneratedFromDir = from;
            DoorDirections.Add(from);
            GeneratedFromRoom = gen;
        }
    }
}