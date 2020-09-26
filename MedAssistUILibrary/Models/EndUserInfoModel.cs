using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UILibrary.Models
{
    public class EndUserInfoModel
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public BitmapImage ProfileImage { get; set; }
        public string Address { get; set; }
        public string BirthPlace { get; set; }

    }
}
