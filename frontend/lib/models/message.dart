class Message {
  int? id;
  String? content;
  bool isRead;
  DateTime? sentAt;

  // Optional: list of related MessageUsers (not populated unless needed)
  List<int>? messageUserIds;

  Message({
    this.id,
    this.content,
    this.isRead = false,
    this.sentAt,
    this.messageUserIds,
  });

  factory Message.fromJson(Map<String, dynamic> json) {
    return Message(
      id: json['id'],
      content: json['content'],
      isRead: json['isRead'] ?? false,
      sentAt: json['sentAt'] != null ? DateTime.parse(json['sentAt']) : null,
      messageUserIds: (json['messageUserIds'] as List?)?.map((e) => e as int).toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'content': content,
      'isRead': isRead,
      'sentAt': sentAt?.toIso8601String(),
      'messageUserIds': messageUserIds,
    };
  }
}