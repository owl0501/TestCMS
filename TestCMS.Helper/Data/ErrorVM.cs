using System;
using System.Collections.Generic;
using System.Text;

namespace TestCMS.Helper.Data
{
    public class ErrorVM
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
