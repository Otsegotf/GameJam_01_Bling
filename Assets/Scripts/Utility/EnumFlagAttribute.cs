using GJgame;
using System;
using UnityEngine;

public class EnumFlagAttribute : PropertyAttribute
{
	public string enumName;

	public EnumFlagAttribute() { }

	public EnumFlagAttribute(string name)
	{
		enumName = name;
	}
}

public static class EnumFlagUtils
{
    internal static UInt32 Count(this ShopItemType itemType)
    {
        UInt32 v = (UInt32)itemType;
        v = v - ((v >> 1) & 0x55555555); // reuse input as temporary
        v = (v & 0x33333333) + ((v >> 2) & 0x33333333); // temp
        UInt32 c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
        return c;
    }
}