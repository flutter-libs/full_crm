import 'package:frontend/models/analytic.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/models/job.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/models/meeting.dart';
import 'package:frontend/models/message_users.dart';
import 'package:frontend/models/tasks.dart';
import 'package:frontend/models/user_meeting.dart';
import 'package:frontend/models/user_roles.dart';

class User {
  String? id;
  String? userName;
  String? email;
  String? name;
  String? address;
  String? city;
  String? state;
  String? zipCode;
  String? description;
  String? password;
  int? messageUserId;
  DateTime? dateOfBirth;
  DateTime? dateCreated;
  List<Campaign>? campaigns;
  List<UserRoles>? userRoles;
  List<Lead>? leads;
  List<Contact>? contacts;
  List<Job>? createdJobs;
  List<Job>? assignedJobs;
  List<Analytic>? analytics;
  List<Tasks>? tasks;
  List<MessageUsers>? messageUsers;
  List<UserMeeting>? userMeetings;
  List<Meeting>? meetings;

  User({
    this.id,
    this.userName,
    this.email,
    this.name,
    this.address,
    this.city,
    this.state,
    this.zipCode,
    this.description,
    this.password,
    this.messageUserId,
    this.dateOfBirth,
    this.dateCreated,
    this.campaigns,
    this.userRoles,
    this.leads,
    this.contacts,
    this.createdJobs,
    this.assignedJobs,
    this.analytics,
    this.tasks,
    this.messageUsers,
    this.userMeetings,
    this.meetings,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'],
      userName: json['userName'],
      email: json['email'],
      name: json['name'],
      address: json['address'],
      city: json['city'],
      state: json['state'],
      zipCode: json['zipCode'],
      description: json['description'],
      password: json['password'],
      messageUserId: json['messageUserId'],
      dateOfBirth: (json['dateOfBirth'] as DateTime),
      dateCreated: (json['dateCreated'] as DateTime),
      campaigns: (json['campaigns'] as List?)?.map((e) => Campaign.fromJson(e)).toList(),
      userRoles: (json['userRoles'] as List?)?.map((e) => UserRoles.fromJson(e)).toList(),
      leads: (json['leads'] as List?)?.map((e) => Lead.fromJson(e)).toList(),
      contacts: (json['contacts'] as List?)?.map((e) => Contact.fromJson(e)).toList(),
      createdJobs: (json['createdJobs'] as List?)?.map((e) => Job.fromJson(e)).toList(),
      assignedJobs: (json['assignedJobs'] as List?)?.map((e) => Job.fromJson(e)).toList(),
      analytics: (json['analytics'] as List?)?.map((e) => Analytic.fromJson(e)).toList(),
      tasks: (json['tasks'] as List?)?.map((e) => Tasks.fromJson(e)).toList(),
      messageUsers: (json['messageUsers'] as List?)?.map((e) => MessageUsers.fromJson(e)).toList(),
      userMeetings: (json['userMeetings'] as List?)?.map((e) => UserMeeting.fromJson(e)).toList(),
      meetings: (json['meetings'] as List?)?.map((e) => Meeting.fromJson(e)).toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'userName': userName,
      'email': email,
      'name': name,
      'address': address,
      'city': city,
      'state': state,
      'zipCode': zipCode,
      'description': description,
      'password': password,
      'messageUserId': messageUserId,
      'dateOfBirth': dateOfBirth!.toIso8601String(),
      'dateCreated': dateCreated!.toIso8601String(),
      'campaigns': campaigns?.map((e) => e.toJson()).toList(),
      'userRoles': userRoles?.map((e) => e.toJson()).toList(),
      'leads': leads?.map((e) => e.toJson()).toList(),
      'contacts': contacts?.map((e) => e.toJson()).toList(),
      'createdJobs': createdJobs?.map((e) => e.toJson()).toList(),
      'assignedJobs': assignedJobs?.map((e) => e.toJson()).toList(),
      'analytics': analytics?.map((e) => e.toJson()).toList(),
      'tasks': tasks?.map((e) => e.toJson()).toList(),
      'messageUsers': messageUsers?.map((e) => e.toJson()).toList(),
      'userMeetings': userMeetings?.map((e) => e.toJson()).toList(),
      'meetings': meetings?.map((e) => e.toJson()).toList(),
    };
  }
}