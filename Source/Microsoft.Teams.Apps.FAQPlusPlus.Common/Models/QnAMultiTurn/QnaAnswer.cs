// <copyright file="QnaAnswer.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.FAQPlusPlus.Common.Models.QnAMultiTurn
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the QnA Answer for the multiturn and normal scenarios as well.
    /// </summary>
    public class QnaAnswer
    {
        /// <summary>
        /// Gets or sets the questions.
        /// </summary>
        [JsonProperty("questions")]
        public string[] Questions { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        [JsonProperty("score")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonProperty("metadata")]
#pragma warning disable CA2227 // Collection properties should be read only
        public List<object> Metadata { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        [JsonProperty("context")]
        public Context Context { get; set; }
    }
}