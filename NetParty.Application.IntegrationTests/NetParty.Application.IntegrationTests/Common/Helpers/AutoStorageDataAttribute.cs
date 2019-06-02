﻿#region Using

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;

#endregion

namespace NetParty.Application.IntegrationTests.Common.Helpers
    {
    public class AutoStorageDataAttribute : AutoDataAttribute
        {
        public AutoStorageDataAttribute()
            : base(
                () => new Fixture()
                    .Customize(new AutoMoqCustomization())
                    .Customize(new StorageProviderCustomization())) { }
        }
    }
