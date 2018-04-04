using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;



namespace 反射优化
{
    public class DataTableUtility
    {

        public static IEnumerable<T> Get<T>(DataTable table) where T : new()
        {
            if (table == null)
            {
                yield break;
            }
            if (table.Rows.Count == 0)
            {
                yield break;
            }
            foreach (DataRow row in table.Rows)
            {
                yield return Get<T>(row);
            }
        }

        public static T Get<T>(DataRow row) where T : new()
        {
            return GenericCache<T>.Factory(row);
        }

        public class GenericCache<T> where T : new()
        {
            static GenericCache()
            {
                Factory = GetFactory();
            }
            public static readonly Func<DataRow, T> Factory;

            private static Func<DataRow, T> GetFactory()
            {
                var type = typeof(T);
                var rowType = typeof(DataRow);
                var rowDeclare = Expression.Parameter(rowType, "row");
                var instanceDeclare = Expression.Parameter(type, "instance");
                var newExpression = Expression.New(type);
                var instanceExpression = Expression.Assign(instanceDeclare, newExpression);
                var nullEqualExpression = Expression.Equal(rowDeclare, Expression.Constant(null));
                var containsMethod = typeof(DataColumnCollection).GetMethod("Contains");
                var indexerMethod = rowType.GetMethod("get_Item", BindingFlags.Instance | BindingFlags.Public, null,
                                                      new[] { typeof(string) },
                                                      new[] { new ParameterModifier(1) });
                var setExpressions = new List<Expression>();
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var columns = Expression.Property(Expression.Property(rowDeclare, "Table"), "Columns");
                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.CanWrite)
                    {
                        var propertyName = Expression.Constant(propertyInfo.Name, typeof(string));
                        var checkIfContainsColumn =
                            Expression.Call(columns, containsMethod, propertyName);
                        var propertyExpression = Expression.Property(instanceDeclare, propertyInfo);
                        var value = Expression.Call(rowDeclare, indexerMethod, propertyName);
                        var proertyAssign = Expression.Assign(propertyExpression, Expression.Convert(value, propertyInfo.PropertyType));
                        setExpressions.Add(Expression.IfThen(checkIfContainsColumn, proertyAssign));
                    }
                }
                var checkIfRowIsNull = Expression.IfThenElse(nullEqualExpression, Expression.Empty(), Expression.Block(setExpressions));
                var body = Expression.Block(new[] { instanceDeclare }, newExpression, instanceExpression, checkIfRowIsNull, instanceDeclare);
                return Expression.Lambda<Func<DataRow, T>>(body, rowDeclare).Compile();
            }
        }

        public static T GetByReflection<T>(DataRow dr) where T : new()
        {
            var t = new T();
            if (dr != null)
            {
                foreach (var p in typeof(T).GetProperties())
                {
                    if (!dr.Table.Columns.Contains(p.Name))
                    {
                        continue;
                    }
                    var obj = dr[p.Name];
                    var set = p.GetSetMethod();
                    if (set == null)
                    {
                        continue;
                    }
                    p.SetValue(t, obj, null);
                }
            }
            return t;
        }
    }
}