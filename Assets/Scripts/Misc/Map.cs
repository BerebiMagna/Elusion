using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    public static Vector3 GetSpawnPoint()
    {
        GameObject[] roomArray = GameObject.FindGameObjectsWithTag("Room");
        for (int i = 0; i < roomArray.Length; i++)
        {
            try
            {
                if (roomArray[i].name.Split('-')[1].Trim() == "Spawn")
                {
                    return roomArray[i].transform.position;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                continue;
            }
        }
        return new Vector3();
    }

    public static List<Vector3> Pathfind(Vector3 start, Vector3 end, int mapWidth, int mapHeight, RoomsList roomList, bool ai=false)
    {
        Dictionary<Vector3, Vector3> parents = new Dictionary<Vector3, Vector3>();
        Dictionary<Vector3, KeyValuePair<Vector3, float>> costs = new Dictionary<Vector3, KeyValuePair<Vector3, float>>();
        Vector3 current = start;
        try
        {
            while (!current.Equals(end))
            {
                Dictionary<Vector3, KeyValuePair<Vector3, float>> pathFindCosts;
                if (ai)
                {
                    pathFindCosts = CalculatePathfindCosts(current, mapWidth, mapHeight, parents, start, end, roomList, ai: true);
                }
                else 
                {
                    pathFindCosts = CalculatePathfindCosts(current, mapWidth, mapHeight, parents, start, end, roomList);
                }
                foreach (KeyValuePair<Vector3, KeyValuePair<Vector3, float>> kVP in pathFindCosts)
                {
                    costs[kVP.Key] = kVP.Value;
                }
                KeyValuePair<Vector3, KeyValuePair<Vector3, float>> minCost = new KeyValuePair<Vector3, KeyValuePair<Vector3, float>>(new Vector3(), new KeyValuePair<Vector3, float>(new Vector3(), Mathf.Infinity));
                foreach (KeyValuePair<Vector3, KeyValuePair<Vector3, float>> cost in costs)
                {
                    if (cost.Value.Value < minCost.Value.Value)
                    {
                        minCost = cost;
                    }
                }
                costs.Remove(minCost.Key);
                parents.Add(minCost.Key, minCost.Value.Key);
                current = minCost.Key;
            }
        }
        catch (System.ArgumentException) { }
        return reversePathfind(parents, start, end);
    }

    private static Dictionary<Vector3, KeyValuePair<Vector3, float>> CalculatePathfindCosts(Vector3 current, int mapWidth, int mapHeight, Dictionary<Vector3, Vector3> parents, Vector3 start, Vector3 end, RoomsList roomList, bool ai=false)
    {
        Dictionary<Vector3, KeyValuePair<Vector3, float>> costs = new Dictionary<Vector3, KeyValuePair<Vector3, float>>();
        Vector3 v = new Vector3(current.x, current.y, current.z - 10);
        Room r = roomList.GetRoomByPosition(v);
        if(r != null)
        {
            if (r.GetRoomIndex() >= 0)
            {
                if (!parents.ContainsKey(v) && v != start)
                {
                    float cost;
                    if (ai)
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v)) * RoomMisc.GetTimeByRoomIndex(r.GetRoomIndex());
                    }
                    else
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v));
                    }
                    costs.Add(v, new KeyValuePair<Vector3, float>(current, cost));
                }
            }
        }
        v = new Vector3(current.x, current.y, current.z + 10);
        r = roomList.GetRoomByPosition(v);
        if (r != null)
        {
            if (r.GetRoomIndex() >= 0)
            {
                if (!parents.ContainsKey(v) && v != start)
                {
                    float cost;
                    if (ai)
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v)) * RoomMisc.GetTimeByRoomIndex(r.GetRoomIndex());
                    }
                    else
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v));
                    }
                    costs.Add(v, new KeyValuePair<Vector3, float>(current, cost));
                }
            }
        }
        v = new Vector3(current.x - 10, current.y, current.z);
        r = roomList.GetRoomByPosition(v);
        if (r != null)
        {
            if (r.GetRoomIndex() >= 0)
            {
                if (!parents.ContainsKey(v) && v != start)
                {
                    float cost;
                    if (ai)
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v)) * RoomMisc.GetTimeByRoomIndex(r.GetRoomIndex());
                    }
                    else
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v));
                    }
                    costs.Add(v, new KeyValuePair<Vector3, float>(current, cost));
                }
            }
        }
        v = new Vector3(current.x + 10, current.y, current.z);
        r = roomList.GetRoomByPosition(v);
        if (r != null)
        {
            if (r.GetRoomIndex() >= 0)
            {
                if (!parents.ContainsKey(v) && v != start)
                {
                    float cost;
                    if (ai)
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v)) * RoomMisc.GetTimeByRoomIndex(r.GetRoomIndex());
                    }
                    else
                    {
                        cost = (StartingPathFindDistance(v, current, start, parents) + Vector3.Distance(end, v));
                    }
                    costs.Add(v, new KeyValuePair<Vector3, float>(current, cost));
                }
            }
        }
        return costs;
    }

    private static int StartingPathFindDistance(Vector3 v, Vector3 current, Vector3 start, Dictionary<Vector3, Vector3> parents)
    {
        Dictionary<Vector3, Vector3> parentsCopy = new Dictionary<Vector3, Vector3>();
        foreach (KeyValuePair<Vector3, Vector3> item in parents)
        {
            parentsCopy[item.Key] = item.Value;
        }
        parentsCopy.Add(v, current);
        return reversePathFindDistance(parentsCopy, start, v); ;
    }

    private static List<Vector3> reversePathfind(Dictionary<Vector3, Vector3> parents, Vector3 start, Vector3 end)
    {
        List<Vector3> path = new List<Vector3>();
        Vector3 current = end;
        while (current != start)
        {
            try
            {
                path.Add(current);
                current = parents[current];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
        return path;
    }

    private static int reversePathFindDistance(Dictionary<Vector3, Vector3> parents, Vector3 start, Vector3 v)
    {
        List<Vector3> path = new List<Vector3>();
        Vector3 current = v;
        int i = 0;
        while (current != start)
        {
            i++;
            path.Add(current);
            current = parents[current];
        }
        return i;
    }
}
