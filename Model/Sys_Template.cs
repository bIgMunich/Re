using Common;
namespace Models
{
    public class Sys_Template
    {
        [DBEntity(_isPrimary = true)]
        public int Id { get; set; }
        public int Vaild { get; set; }
        public int Lever { get; set; }
        [DBEntity(_isNullOrEmpty = true)]
        public string Template { get; set; }
        [DBEntity(_isNullOrEmpty = true)]
        public string TemplateUrl { get; set; }
        [DBEntity(_isNullOrEmpty = true)]
        public int ParentId { get; set; }

        [DBEntity(_isNullOrEmpty = true, _isRepeter = true)]
        public string TemplateCode { get; set; }
        public int Type { get; set; }//0位菜单项，1位action项
    }
}