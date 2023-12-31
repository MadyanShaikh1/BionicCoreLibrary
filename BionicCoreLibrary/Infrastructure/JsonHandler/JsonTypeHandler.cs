﻿using System;
using System.Data;
using Dapper;
using Newtonsoft.Json;

public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    public override void SetValue(IDbDataParameter parameter, T value)
    {
        parameter.Value = JsonConvert.SerializeObject(value);
    }

    public override T Parse(object value)
    {
        return JsonConvert.DeserializeObject<T>(value.ToString());
    }
}
