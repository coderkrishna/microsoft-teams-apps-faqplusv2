// <copyright file="ResponseCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.FAQPlusPlus.Cards
{
    using System.Collections.Generic;
    using AdaptiveCards;
    using Microsoft.Bot.Schema;
    using Microsoft.Teams.Apps.FAQPlusPlus.Bots;
    using Microsoft.Teams.Apps.FAQPlusPlus.Common;
    using Microsoft.Teams.Apps.FAQPlusPlus.Common.Models;
    using Microsoft.Teams.Apps.FAQPlusPlus.Common.Models.QnAMultiTurn;
    using Microsoft.Teams.Apps.FAQPlusPlus.Properties;

    /// <summary>
    ///  This class process Response Card- Response by bot when user asks a question to bot.
    /// </summary>
    public static class ResponseCard
    {
        /// <summary>
        /// Construct the response card - when user asks a question to QnA Maker through bot.
        /// </summary>
        /// <param name="question">Knowledgebase question, from QnA Maker service.</param>
        /// <param name="answer">Knowledgebase answer, from QnA Maker service.</param>
        /// <param name="userQuestion">Actual question asked by the user to the bot.</param>
        /// <returns>Response card.</returns>
        public static Attachment GetCard(string question, string answer, string userQuestion)
        {
            AdaptiveCard responseCard = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Weight = AdaptiveTextWeight.Bolder,
                        Text = Strings.ResponseHeaderText,
                        Wrap = true,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = question,
                        Wrap = true,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = answer,
                        Wrap = true,
                    },
                },
                Actions = new List<AdaptiveAction>
                {
                    new AdaptiveSubmitAction
                    {
                        Title = Strings.AskAnExpertButtonText,
                        Data = new ResponseCardPayload
                        {
                            MsTeams = new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                DisplayText = Strings.AskAnExpertDisplayText,
                                Text = Constants.AskAnExpert,
                            },
                            UserQuestion = userQuestion,
                            KnowledgeBaseAnswer = answer,
                        },
                    },
                    new AdaptiveSubmitAction
                    {
                        Title = Strings.ShareFeedbackButtonText,
                        Data = new ResponseCardPayload
                        {
                            MsTeams = new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                DisplayText = Strings.ShareFeedbackDisplayText,
                                Text = Constants.ShareFeedback,
                            },
                            UserQuestion = userQuestion,
                            KnowledgeBaseAnswer = answer,
                        },
                    },
                },
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = responseCard,
            };
        }

        /// <summary>
        /// This method would be able to get the necessary card.
        /// </summary>
        /// <param name="response">The response model that contains the QnA Response.</param>
        /// <param name="question">The question coming in from the response.</param>
        /// <param name="answer">The QnA answer.</param>
        /// <param name="userQuestion">The question that the user has asked the bot.</param>
        /// <returns>An attachment to append to a message.</returns>
        public static Attachment GetCard(QnaAnswer response, string question, string answer, string userQuestion)
        {
            AdaptiveCard responseCard = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Weight = AdaptiveTextWeight.Bolder,
                        Text = Strings.ResponseHeaderText,
                        Wrap = true,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = question,
                        Wrap = true,
                    },
                    new AdaptiveTextBlock
                    {
                        Text = answer,
                        Wrap = true,
                    },
                },
                Actions = BuildListOfActions(response, userQuestion, answer),
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = responseCard,
            };
        }

        private static List<AdaptiveAction> BuildListOfActions(QnaAnswer response, string userQuestion, string answer)
        {
            List<AdaptiveAction> actionsList = new List<AdaptiveAction>();

            if (response?.Context.Prompts.Count > 0)
            {
                actionsList.Add(new AdaptiveShowCardAction
                {
                    Title = "Follow Ups",
                    Card = new AdaptiveCard("1.0")
                    {
                        Actions = AddMultiTurnOptions(response?.Context.Prompts),
                    },
                });
            }

            actionsList.Add(
                new AdaptiveSubmitAction
                {
                    Title = Strings.ShareFeedbackButtonText,
                    Data = new ResponseCardPayload
                    {
                        MsTeams = new CardAction
                        {
                            Type = ActionTypes.MessageBack,
                            DisplayText = Strings.ShareFeedbackDisplayText,
                            Text = Constants.ShareFeedback,
                        },
                        UserQuestion = userQuestion,
                        KnowledgeBaseAnswer = answer,
                    },
                });

            actionsList.Add(new AdaptiveSubmitAction
            {
                Title = Strings.AskAnExpertButtonText,
                Data = new ResponseCardPayload
                {
                    MsTeams = new CardAction
                    {
                        Type = ActionTypes.MessageBack,
                        DisplayText = Strings.AskAnExpertDisplayText,
                        Text = Constants.AskAnExpert,
                    },
                    UserQuestion = userQuestion,
                    KnowledgeBaseAnswer = answer,
                },
            });

            return actionsList;
        }

        private static List<AdaptiveAction> AddMultiTurnOptions(List<Prompt> prompts)
        {
            var multiTurnActions = new List<AdaptiveAction>();

            foreach (var item in prompts)
            {
                multiTurnActions.Add(new AdaptiveSubmitAction
                {
                    Title = item.DisplayText,
                    Data = new ResponseCardPayload
                    {
                        UserQuestion = string.Empty,
                        KnowledgeBaseAnswer = string.Empty,
                        MsTeams = new CardAction
                        {
                            Type = ActionTypes.MessageBack,
                            DisplayText = item.DisplayText,
                            Text = item.DisplayText,
                        },
                    },
                });
            }

            return multiTurnActions;
        }
    }
}