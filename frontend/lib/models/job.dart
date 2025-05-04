import 'package:frontend/models/User.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/models/tasks.dart';

class Job {
  int? id;
  String? title;
  String? description;
  String? status;
  String? priority;
  DateTime? scheduledDate;
  DateTime? completionDate;
  double? estimatedCost;
  double? actualCost;
  String? notes;
  DateTime? dateCreated;
  DateTime? dateUpdated;
  int? contactId;
  Contact? contact;
  String? assignedUserId;
  User? assignedUser;
  String? createdByUserId;
  User? createdByUser;
  List<Tasks>? tasks;

  Job({
    this.id,
    this.title,
    this.description,
    this.status,
    this.priority,
    this.scheduledDate,
    this.completionDate,
    this.estimatedCost,
    this.actualCost,
    this.notes,
    this.dateCreated,
    this.dateUpdated,
    this.contactId,
    this.contact,
    this.assignedUserId,
    this.assignedUser,
    this.createdByUserId,
    this.createdByUser,
    this.tasks,
  });

  factory Job.fromJson(Map<String, dynamic> json) {
    return Job(
      id: json['id'],
      title: json['title'],
      description: json['description'],
      status: json['status'],
      priority: json['priority'],
      scheduledDate: DateTime.parse(json['scheduledDate']),
      completionDate: json['completionDate'] != null
          ? DateTime.parse(json['completionDate'])
          : null,
      estimatedCost: json['estimatedCost'] != null
          ? json['estimatedCost'].toDouble()
          : null,
      actualCost: json['actualCost'] != null
          ? json['actualCost'].toDouble()
          : null,
      notes: json['notes'],
      dateCreated: DateTime.parse(json['dateCreated']),
      dateUpdated: json['dateUpdated'] != null
          ? DateTime.parse(json['dateUpdated'])
          : null,
      contactId: json['contactId'],
      contact: json['contact'] != null ? Contact.fromJson(json['contact']) : null,
      assignedUserId: json['assignedUserId'],
      assignedUser: json['assignedUser'] != null
          ? User.fromJson(json['assignedUser'])
          : null,
      createdByUserId: json['createdByUserId'],
      createdByUser: json['createdByUser'] != null
          ? User.fromJson(json['createdByUser'])
          : null,
      tasks: (json['tasks'] as List?)
          ?.map((item) => Tasks.fromJson(item))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'title': title,
      'description': description,
      'status': status,
      'priority': priority,
      'scheduledDate': scheduledDate?.toIso8601String(),
      'completionDate': completionDate?.toIso8601String(),
      'estimatedCost': estimatedCost,
      'actualCost': actualCost,
      'notes': notes,
      'dateCreated': dateCreated?.toIso8601String(),
      'dateUpdated': dateUpdated?.toIso8601String(),
      'contactId': contactId,
      'contact': contact?.toJson(),
      'assignedUserId': assignedUserId,
      'assignedUser': assignedUser?.toJson(),
      'createdByUserId': createdByUserId,
      'createdByUser': createdByUser?.toJson(),
      'tasks': tasks?.map((e) => e.toJson()).toList(),
    };
  }
}