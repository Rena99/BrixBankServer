﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Services.Models
{
    public class VerificationHelperModel
    {
        public CustomerModel Customer { get; set; }
        public EmailVerificationModel EmailVerification { get; set; }
    }
}
