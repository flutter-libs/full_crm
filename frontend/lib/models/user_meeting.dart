import 'package:frontend/models/User.dart';
import 'package:frontend/models/meeting.dart';

class UserMeeting {
  int? id;
  String? userId;
  User? user;
  int? meetingId;
  Meeting? meeting;
  UserMeeting({
    this.id,
    this.userId,
    this.user,
    this.meetingId,
    this.meeting
  });

  factory UserMeeting.fromJson(Map<String, dynamic> json) {
    return UserMeeting(
      id: json['id'],
      userId: json['userId'],
      meetingId: json['meetingId'],
      meeting: json['meeting'],
      user: json['user'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'userId': userId,
      'meetingId': meetingId,
      'meeting': meeting,
      'user': user,
    };
  }
}