﻿using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CuoiKi.HelperClasses
{
    public class FilterChain<T> where T : ModelBase
    {
        private Dictionary<string, Predicate<T>> _predicates;
        public FilterChain()
        {
            _predicates = new();
        }

        public void AddPredicate(string key, Predicate<T> predicate)
        {
            _predicates.Add(key, predicate);
        }

        public void RemovePredicate(string key)
        {
            _predicates.Remove(key);
        }

        public List<T> ApplyAll(List<T> list)
        {
            List<T> result = list;

            foreach (Predicate<T> filter in _predicates.Values)
            {
                result = result.FindAll(filter);
            }
            return result;
        }
        public List<T> ApplyAllOrLogic(List<T> list)
        {
            List<T> result = new List<T>();
            foreach (Predicate<T> filter in _predicates.Values)
            {
                Func<T, bool> filterFunc = new Func<T, bool>(filter);
                result.AddRange(list.Where(filterFunc));
            }
            return result;
        }
    }
}
