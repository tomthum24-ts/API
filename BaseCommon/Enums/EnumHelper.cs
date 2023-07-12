using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Enums
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString() ?? throw new InvalidOperationException());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        #region Get Enum
        public static string GetEnumValueAndText(System.Type typeofEnum)
        {
            var valueAndText = new List<string>();
            foreach (var e in Enum.GetValues(typeofEnum))
            {

                string text = ((Enum)Enum.Parse(typeofEnum, e.ToString())).GetEnumDesciption();
                text = text ?? e.ToString();
                valueAndText.Add(string.Format("{0}:{1}", ((int)e).ToString(), text));
            }
            string enumtext = string.Join(";", valueAndText);
            return enumtext;
        }

        public static string GetEnumText(System.Type typeofEnum, int? valueEnum)
        {
            string text = "";
            if (valueEnum.HasValue)
            {
                foreach (var e in Enum.GetValues(typeofEnum))
                {
                    if (valueEnum == ((int)e))
                    {
                        text = ((Enum)Enum.Parse(typeofEnum, e.ToString())).GetEnumDesciption();
                        text = text ?? e.ToString();
                    }
                }
            }

            return text;
        }

        public static string GetEnumDescription<TEnum>(int value)
        {
            return GetEnumDescription((Enum)(object)((TEnum)(object)value));
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();

        }

        public static string GetEnumDesciption(this Enum val)
        {
            string name = Enum.GetName(val.GetType(), val);
            System.Reflection.FieldInfo obj = val.GetType().GetField(name);
            if (obj != null)
            {
                object[] attributes = obj.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                return attributes.Length > 0 ? ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description : null;
            }
            return null;
        }

        #endregion
    }
}
