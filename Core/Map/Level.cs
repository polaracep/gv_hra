using System;
using System.Collections.Generic;
using System.Numerics;
using TBoGV;

public class Level
{
    protected List<Room> RoomList = new List<Room>();
    protected List<Room> Candidates = new List<Room>();
    protected Room[,] RoomMap;
    protected int Size;
    protected int RoomCount;
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
        GenerateLevel();
    }


    protected void GenerateLevel()
    {
        RoomMap = new Room[Size, Size];
        Random rand = new Random();
        int roomsPlaced = 0;

        RoomMap[(int)StartPos.X, (int)StartPos.Y] = new RoomStart(StartPos, Player);
        roomsPlaced++;

        AddCandidate(new RoomEmpty(new Vector2(StartPos.X + 1, 1), Directions.LEFT, Player));
        AddCandidate(new RoomEmpty(new Vector2(StartPos.X - 1, 1), Directions.RIGHT, Player));
        AddCandidate(new RoomEmpty(new Vector2(StartPos.X, StartPos.Y + 1), Directions.DOWN, Player));
        AddCandidate(new RoomEmpty(new Vector2(StartPos.X, StartPos.Y - 1), Directions.UP, Player));

        while (roomsPlaced < RoomCount)
        {
            Room rGen = Candidates[rand.Next(Candidates.Count)];
            if (RoomMap[(int)rGen.Position.X, (int)rGen.Position.Y] == null)
            {
                RoomMap[(int)rGen.Position.X, (int)rGen.Position.Y] = rGen;
                roomsPlaced++;
            }
            else
            {
                RoomMap[(int)rGen.Position.X, (int)rGen.Position.Y].DoorDirectons.Add(rGen.DoorDirectons[0]);
            }
        }


    }

    protected void AddCandidate(Room r)
    {
        if (r.Position.X < Size && r.Position.Y < Size &&
            r.Position.X >= 0 && r.Position.Y >= 0)
            Candidates.Add(r);
    }
}