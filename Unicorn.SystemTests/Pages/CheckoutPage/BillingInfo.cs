// <copyright file="BillingInfo.cs" company="Unicorn Ltd.">
// "Licensed under the Apache License, Version 2.0 (the "License");"
// </copyright>

namespace Unicorn.SystemTests.Pages
{
    public class BillingInfo
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CompanyName { get; set; }

        public string? Country { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Town { get; set; }

        public string? State { get; set; }

        public string? PostCode { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool ShouldCreateAccount { get; set; } = false;

        public bool CheckPayment { get; set; } = false;
    }
}