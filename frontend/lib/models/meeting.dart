
import 'package:frontend/models/user_meeting.dart';
import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class Meeting {
  int? id;
  String? title;
  String? description;
  DateTime? startTime;
  DateTime? endTime;
  bool? isOnline;
  String? location;
  String? meetingLink;
  String? organizerId;
  String? dateCreated;
  int? leadId;
  int? contactId;
  List<UserMeeting>? userMeetings;

  Meeting({
    this.id,
    this.title,
    this.description,
    this.startTime,
    this.endTime,
    this.isOnline = true,
    this.location,
    this.meetingLink,
    this.organizerId,
    this.leadId,
    this.contactId,
    this.dateCreated,
    this.userMeetings
  });

  factory Meeting.fromJson(Map<String, dynamic> json) => Meeting(
    id: json['id'],
    title: json['title'],
    description: json['description'],
    startTime: json['startTime'] != null ? DateTime.parse(json['startTime']) : null,
    endTime: json['endTime'] != null ? DateTime.parse(json['endTime']) : null,
    isOnline: json['isOnline'],
    location: json['location'],
    meetingLink: json['meetingLink'],
    organizerId: json['organizerId'],
    leadId: json['leadId'],
    contactId: json['contactId'],
    dateCreated: json['dateCreated'],
    userMeetings: (json['userMeetings'] as List?)?.map((e) => UserMeeting.fromJson(e)).toList(),
  );

  Map<String, dynamic> toJson() => {
    'id': id,
    'title': title,
    'description': description,
    'startTime': startTime?.toIso8601String(),
    'endTime': endTime?.toIso8601String(),
    'isOnline': isOnline,
    'location': location,
    'meetingLink': meetingLink,
    'organizerId': organizerId,
    'leadId': leadId,
    'contactId': contactId,
    'dateCreated': dateCreated,
    'userMeetings': userMeetings?.map((e) => e.toJson()).toList(),
  };
}