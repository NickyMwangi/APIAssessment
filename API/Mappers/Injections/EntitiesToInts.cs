﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using API.Models;
using Omu.ValueInjecter.Injections;

namespace API.Mappers.Injections
{
    // go from ICollection<Entity> to int[] 
    public class EntitiesToInts : LoopInjection
    {
        protected override bool MatchTypes(Type src, Type trg)
        {
            return trg == typeof(int[])
                && src.IsGenericType 
                && src.GetGenericTypeDefinition() == typeof(ICollection<>)
                && src.GetGenericArguments()[0].IsSubclassOf(typeof(Entity));
        }

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            var val = sp.GetValue(source);
            if (val != null)
            {
                //tp.SetValue(target, (val as IEnumerable<Entity>).Select(o => o.id).ToArray());
                tp.SetValue(target, (val as IEnumerable<Entity>).ToList());
            }
        }
    }
}