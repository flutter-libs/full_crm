class MessageUsers {
  int? id;
  int? messageId;
  String? fromId;

  // Optionally, for nested deserialization:
  // Message? message;
  // User? from;
  List<String>? receiverIds;

  MessageUsers({
    this.id,
    this.messageId,
    this.fromId,
    this.receiverIds,
  });

  factory MessageUsers.fromJson(Map<String, dynamic> json) {
    return MessageUsers(
      id: json['id'],
      messageId: json['messageId'],
      fromId: json['fromId'],
      receiverIds: (json['receiverIds'] as List?)?.map((e) => e.toString()).toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'messageId': messageId,
      'fromId': fromId,
      'receiverIds': receiverIds,
    };
  }
}