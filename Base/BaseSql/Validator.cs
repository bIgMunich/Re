using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Base.BaseSql
{
    public class Validator
    {
        public ErrorResult ValidatorData<T>(PropertyInfo pi, T entity)
        {
            ErrorResult result = new ErrorResult();
            var name = pi.Name;
            var value = pi.GetValue(entity, null);
            var type = pi.GetType();
            var ruleArray = pi.GetCustomAttributes(typeof(DBEntity), false);
            if (ruleArray != null && ruleArray.Length > 0)
            {
                DBEntity model = (DBEntity)ruleArray[0];
                if (model._isPrimary)
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                    {
                        result.Flag = false;
                        result.Message = "主键" + name + "的值不能为空，请检查";
                        return result;
                    }
                }
                if (model._isNullOrEmpty)
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                    {
                        result.Flag = false;
                        result.Message = "字段" + name + "的值不能为空，请检查";
                        return result;
                    }
                }
            }
            result.Flag = true;
            return result;
        }
    }


}

