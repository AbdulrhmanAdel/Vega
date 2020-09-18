using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Core.Models;

namespace Vega.Extensions
{
    public static class IQueryableExtension
    {
        
        public static IQueryable<Vehicle> ApplyFiltering(this IQueryable<Vehicle> query,VehicleQuery queryObject)
        {
            if(queryObject.MakeId.HasValue)  
                query =query.Where(v => v.Model.MakeId == queryObject.MakeId);
            if(queryObject.ModelId.HasValue)
                query =query.Where(v => v.ModelId == queryObject.ModelId); 

            return query;   
        }
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query,IQueryObject queryObject,Dictionary<string , Expression<Func<T,object>>> columnMap){
        if(string.IsNullOrWhiteSpace(queryObject.SortBy) || !columnMap.ContainsKey(queryObject.SortBy))
                return query;
                
            if(!queryObject.IsSortAscending)
                query =query.OrderBy(columnMap[queryObject.SortBy]);
            else{
                query =query.OrderByDescending(columnMap[queryObject.SortBy]);
            }
            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query,IQueryObject queryObject){
            if(queryObject.Page <= 0)
                queryObject.Page =1;
            if (queryObject.PageSize <= 0 )
                queryObject.PageSize = 10;

            return query.Skip((queryObject.Page -1)* queryObject.PageSize).Take(queryObject.PageSize);
        }

    }
}