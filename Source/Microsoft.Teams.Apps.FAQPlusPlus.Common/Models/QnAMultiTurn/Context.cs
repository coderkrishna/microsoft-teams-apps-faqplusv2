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
        /// <summary>
        /// Gets or sets a value indicating whether or not the context only.
        /// </summary>
        [JsonProperty("isContextOnly")]
        public bool IsContextOnly { get; set; }

        /// <summary>
        /// Gets or sets the list of prompts.
        /// </summary>
        [JsonProperty("prompts")]
        public List<Prompt> Prompts { get; set; }
    }
}