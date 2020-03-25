using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMisc
{
   private static Dictionary<int, float> roomTimeDictionary = new Dictionary<int, float>() 
   {
       { -1, -1},
       { 0, 1},
       { 1, 1.5f},
       { 2, 2},
   };
    
    public static float GetTimeByRoomIndex(int roomIndex)
    {
        if (roomTimeDictionary.ContainsKey(roomIndex))
        {
            return roomTimeDictionary[roomIndex];
        }
        return 1;
    }
}
