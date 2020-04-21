// <copyright file="Context.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.FAQPlusPlus.Common.Models.QnAMultiTurn
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the context.
    /// </summary>
    public class Context
    {
        [JsonProperty("isContextOnly")]
        public bool IsContextOnly { get; set; }
    }
}