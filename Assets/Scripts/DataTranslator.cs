using UnityEngine;
using System;

//Can be improved/overwritten

public class DataTranslator
{
    private static string KILLS_TAG = "[KILLS]";
    private static string DEATH_TAG = "[DEATH]";

    public static string ValuesToData (int kills, int deaths)
    {
        return KILLS_TAG + kills + "/" + DEATH_TAG + deaths;
    }

    public static int DataToKills(string data)
    {
        return int.Parse(DataToValue(data, KILLS_TAG));
    }

    public static int DataToDeath(string data)
    {
        return int.Parse(DataToValue(data, DEATH_TAG));
    }

    private static string DataToValue(string data, string symbol)
    {

        string[] pieces = data.Split('/');
        foreach (string p in pieces)
        {
            if (p.StartsWith(symbol))
            {
                return p.Substring(symbol.Length);

            }
        }

        Debug.LogError(symbol + " not found in " + data);

        return "";
    }
	
}
