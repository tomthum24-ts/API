using System.Collections.Generic;
using System.Linq;

namespace BaseCommon.Common.AttachFile
{
    public class MineTypeModel
    {
        public string ContentType { get; set; }

        public string Extention { get; set; }

        private List<MineTypeModel> List { get; set; }

        private List<MineTypeModel> GetList()
        {
            var list = new List<MineTypeModel>();

            list.Add(new MineTypeModel() { ContentType = "image/jpeg", Extention = "jpg" });
            list.Add(new MineTypeModel() { ContentType = "image/png", Extention = "png" });
            list.Add(new MineTypeModel() { ContentType = "image/bmp", Extention = "bmp" });
            list.Add(new MineTypeModel() { ContentType = "image/gif", Extention = "gif" });
            list.Add(new MineTypeModel() { ContentType = "application/pdf", Extention = "pdf" });

            list.Add(new MineTypeModel() { ContentType = "application/msword", Extention = "doc" });
            list.Add(new MineTypeModel() { ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", Extention = "docx" });

            list.Add(new MineTypeModel() { ContentType = "application/vnd.ms-excel", Extention = "xls" });
            list.Add(new MineTypeModel() { ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Extention = "xlsx" });

            list.Add(new MineTypeModel() { ContentType = "application/vnd.ms-powerpoint", Extention = "ppt" });
            list.Add(new MineTypeModel() { ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation", Extention = "pptx" });

            return list;
        }

        public MineTypeModel IndexOf(string mineType)
        {
            List = GetList();

            var obj = List.FirstOrDefault(p => p.ContentType.Equals(mineType));
            if (obj != null)
            {
                return obj;
            }

            return null;
        }
    }
}