using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Helpers
{

    public class BaseEntity
    {
        public int Id { get; set; }
    }

    //Generic Comparer
    public class EntityComparer<T> : IEqualityComparer<T> //, IComparer<T>
    where T : SalesOrderLine
    {
        public enum AscDesc : short
        {
            Asc, Desc
        }

        #region Const
        public string PropertyName { get; set; }
        public AscDesc SortType { get; set; }

        public List<string> Properties { get; set; }
        #endregion

        #region Ctor
        public EntityComparer(List<string> _properties,string _propertyname = "Id", AscDesc _sorttype = AscDesc.Asc)
        {
            this.Properties = _properties;
            this.PropertyName = _propertyname;
            this.SortType = _sorttype;
        }
        #endregion

        #region IComparer
        public bool Compare(T x, T y)
        {
            if (typeof(T).GetProperty(PropertyName) == null)
                throw new ArgumentNullException(string.Format("{0} does not contain a property with the name \"{1}\"", typeof(T).Name, PropertyName));

            foreach (var item in Properties )
            {
                //var xValue = (IComparable)x.GetType().GetProperty(item.Name).GetValue(x, null);
                //var yValue = (IComparable)y.GetType().GetProperty(item.Name).GetValue(y, null);
                var xValue = x.GetType().GetProperty(item).GetValue(x, null);
                var yValue = y.GetType().GetProperty(item).GetValue(y, null);

                if(xValue==null || yValue == null) { if (xValue == null && yValue == null) { } else { return false; }   }
                else if (xValue.ToString().Trim() != yValue.ToString().Trim()) { return false; }
            }


            return true;
            //if (this.SortType == AscDesc.Asc)
            //    return xValue.CompareTo(yValue);

            //return yValue.CompareTo(xValue);
        }
        #endregion

        #region IEqualityComparer
        public bool Equals(T x, T y)
        {
            if (typeof(T).GetProperty(PropertyName) == null)
                throw new InvalidOperationException(string.Format("{0} does not contain a property with the name -> \"{1}\"", typeof(T).Name, PropertyName));

            var valuex = x.GetType().GetProperty(PropertyName).GetValue(x, null);
            var valuey = y.GetType().GetProperty(PropertyName).GetValue(y, null);

            if (valuex == null) return valuey == null;

            return valuex.Equals(valuey);
        }

        public int GetHashCode(T obj)
        {
            var info = obj.GetType().GetProperty(PropertyName);
            object value = null;
            if (info != null)
            {
                value = info.GetValue(obj, null);
            }

            return value == null ? 0 : value.GetHashCode();
        }
        #endregion
    }




}
