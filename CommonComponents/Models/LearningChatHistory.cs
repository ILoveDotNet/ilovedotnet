namespace CommonComponents.Models;

public sealed record LearningChatHistory(int Version, Guid? ActiveId, List<LearningChatConversation> Conversations);

public sealed record LearningChatConversation(Guid Id, string Title, List<LearningChatHistoryMessage> Messages);

public sealed record LearningChatHistoryMessage(string Text, bool IsUser, bool IsGenerated, List<LearningChatHistorySource> Sources);

public sealed record LearningChatHistorySource(string Title, string Section, string Heading, string ContentUrl, string Excerpt);
