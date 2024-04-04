//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using Xeptions;

namespace SimpleProject.Api.Models.Foundations.Products.Exceptions
{
    public class NullProductException:Xeption
    {
        public NullProductException()
            :base(message:"Product is null")

        { }
    }
}
