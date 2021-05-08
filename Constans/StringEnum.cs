using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Constans
{
    public class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValue[] values = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (values.Length > 0)
            {
                output = values[0].Value;
            }
            return output;
        }
    }
}
