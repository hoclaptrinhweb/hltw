﻿using System;

public static class Util
{
    public static T Parse<T>(object value)
    {
        try { return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value.ToString()); }
        catch (Exception) { return default(T); }
    }
}
