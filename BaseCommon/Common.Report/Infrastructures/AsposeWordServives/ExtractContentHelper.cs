using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseCommon.Common.Report.Infrastructures.AsposeWordServives
{
    public static class BaseCommon
    {
        public static string Normalization(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text.Normalize(NormalizationForm.FormKC);
            return text;
        }

        public static string GetEnumDesciption(this System.Enum val)
        {
            string name = System.Enum.GetName(val.GetType(), val);
            System.Reflection.FieldInfo obj = val.GetType().GetField(name);
            if (obj != null)
            {
                object[] attributes = obj.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                return attributes.Length > 0 ? ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description : null;
            }
            return null;
        }

        public static string GetEnumCode(this System.Enum val)
        {
            var obj = (object)val;
            return ((int)obj).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Check data empty to new blank 1 record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<T> CheckDataEmpty<T>(IEnumerable<T> data) where T : class, new()
        {
            try
            {
                if (data == null || !data.Any())
                {
                    return new List<T> { new T() };
                }

                foreach (T item in data)
                {
                    foreach (PropertyInfo prop in item.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(string) && prop.CanWrite)
                        {
                            var value = prop.GetValue(item);
                            // format value
                            prop.SetValue(item, Convert.ChangeType(Normalization(value?.ToString()), prop.PropertyType), null);
                        }
                    }
                }
            }
            catch
            {
            }

            return data.ToList();
        }


        /// <summary>
        /// Remove HTML Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<T> ClearHTMLData<T>(IEnumerable<T> data) where T : class
        {
            try
            {
                if (data != null && data.Any())
                {
                    foreach (T item in data)
                    {
                        foreach (PropertyInfo prop in item.GetType().GetProperties())
                        {
                            if (prop.PropertyType == typeof(string) && prop.CanWrite)
                            {
                                var value = prop.GetValue(item);
                                // format value
                                prop.SetValue(item, Convert.ChangeType(Normalization(Regex.Replace(WebUtility.HtmlDecode(value?.ToString() ?? ""), "<.*?>", string.Empty)), prop.PropertyType), null);
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return data.ToList();
        }

        /// <summary>
        /// Get all properties of class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static HashSet<string> GetPropertiesOfClass<T>() where T : class
        {
            return typeof(T).GetProperties().Select(x => x.Name).ToHashSet();
        }

        /// <summary>
        /// Read Byte from file
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetByteFromFileAsync(string url)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = await webClient.DownloadDataTaskAsync(url);
                    return imageBytes;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// List<T>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<ExpandoObject> ConvertDataToExpandoObjectForWord<T>(List<T> data)
        {
            try
            {
                var serializeObject = JsonConvert.SerializeObject(data);
                return JsonConvert.DeserializeObject<List<ExpandoObject>>(serializeObject);
            }
            catch
            {
                return default;
            }
        }

        public static Stream LStream
        {
            get
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(Resource.LicenseAspose ?? ""));
            }
        }
    }
}
