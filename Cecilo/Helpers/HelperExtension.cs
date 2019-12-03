using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cecilo
{
    public enum ManageMessageId
    {
        FormSuccess,
        Error
    }

    public enum LanguageId
    {
        [Display(Name ="TR-tr")]
        Tr,
        [Display(Name = "EN-us")]
        En
    }

}