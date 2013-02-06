using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trul.Framework.Rules
{
    [Serializable]
    public class ValidateSettings
    {
        /// <summary>
        /// form id
        /// </summary>
        public string FormID { get; set; }

        /// <summary>
        /// click event'inda <see cref="FormID"/> formunu validate edecek kontrol id
        /// </summary>
        public string SubmitControlID { get; set; }

        /// <summary>
        /// hata mesajlarinin gosterilecegi container
        /// </summary>
        public string ErrorContainer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorLabelContainer { get; set; }

        /// <summary>
        /// error css class
        /// </summary>
        public string ErrorClass { get; set; }

        /// <summary>
        /// hata mesajlari arasinda yer alacak wrapper
        /// </summary>
        public string Wrapper { get; set; }
    }
}
