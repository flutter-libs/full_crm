import 'package:frontend/models/User.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/models/company.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/models/job.dart';

class Tasks {
  int? id;
  String? taskDescription;
  DateTime? dueDate;
  String? status;  // Enum or String
  String? priority; // Enum or String
  String? assignedToUserId;
  User? assignedToUser;
  int? campaignId;
  Campaign? campaign;
  int? jobId;
  Job? job;
  int? contactId;
  Contact? contact;
  int? companyId;
  Company? company;
  DateTime? dateCreated;
  DateTime? dateUpdated;
  DateTime? dateCompleted;

  Tasks({
    this.id,
    this.taskDescription,
    this.dueDate,
    this.status,
    this.priority,
    this.assignedToUserId,
    this.assignedToUser,
    this.campaignId,
    this.campaign,
    this.jobId,
    this.job,
    this.contactId,
    this.contact,
    this.companyId,
    this.company,
    this.dateCreated,
    this.dateUpdated,
    this.dateCompleted,
  });

  factory Tasks.fromJson(Map<String, dynamic> json) {
    return Tasks(
      id: json['id'],
      taskDescription: json['taskDescription'],
      dueDate: DateTime.parse(json['dueDate']),
      status: json['status'],
      priority: json['priority'],
      assignedToUserId: json['assignedToUserId'],
      assignedToUser: json['assignedToUser'] != null
          ? User.fromJson(json['assignedToUser'])
          : null,
      campaignId: json['campaignId'],
      campaign: json['campaign'] != null
          ? Campaign.fromJson(json['campaign'])
          : null,
      jobId: json['jobId'],
      job: json['job'] != null ? Job.fromJson(json['job']) : null,
      contactId: json['contactId'],
      contact: json['contact'] != null ? Contact.fromJson(json['contact']) : null,
      companyId: json['companyId'],
      company: json['company'] != null ? Company.fromJson(json['company']) : null,
      dateCreated: DateTime.parse(json['dateCreated']),
      dateUpdated: json['dateUpdated'] != null
          ? DateTime.parse(json['dateUpdated'])
          : null,
      dateCompleted: json['dateCompleted'] != null
          ? DateTime.parse(json['dateCompleted'])
          : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'taskDescription': taskDescription,
      'dueDate': dueDate?.toIso8601String(),
      'status': status,
      'priority': priority,
      'assignedToUserId': assignedToUserId,
      'assignedToUser': assignedToUser?.toJson(),
      'campaignId': campaignId,
      'campaign': campaign?.toJson(),
      'jobId': jobId,
      'job': job?.toJson(),
      'contactId': contactId,
      'contact': contact?.toJson(),
      'companyId': companyId,
      'company': company?.toJson(),
      'dateCreated': dateCreated?.toIso8601String(),
      'dateUpdated': dateUpdated?.toIso8601String(),
      'dateCompleted': dateCompleted?.toIso8601String(),
    };
  }
}