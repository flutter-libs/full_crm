import 'package:frontend/models/User.dart';
import 'package:frontend/models/contact.dart';
import 'package:frontend/models/lead.dart';
import 'package:frontend/models/tasks.dart';

class Campaign {
  int? id;
  String? name;
  String? description;
  String? type;
  String? status;
  DateTime? startDate;
  DateTime? endDate;
  double? budget;
  double? actualCost;
  int? expectedResponses;
  int? actualResponses;
  double? expectedSales;
  double? actualSales;
  String? notes;
  DateTime? dateCreated;
  DateTime? dateUpdated;
  String? createdByUserId;
  User? createdByUser;
  List<Lead>? leads;
  List<Contact>? contacts;
  List<Tasks>? tasks;

  Campaign({
    this.id,
    this.name,
    this.description,
    this.type,
    this.status,
    this.startDate,
    this.endDate,
    this.budget,
    this.actualCost,
    this.expectedResponses,
    this.actualResponses,
    this.expectedSales,
    this.actualSales,
    this.notes,
    this.dateCreated,
    this.dateUpdated,
    this.createdByUserId,
    this.createdByUser,
    this.leads,
    this.contacts,
    this.tasks,
  });

  factory Campaign.fromJson(Map<String, dynamic> json) {
    return Campaign(
      id: json['id'],
      name: json['name'],
      description: json['description'],
      type: json['type'],
      status: json['status'],
      startDate: DateTime.parse(json['startDate']),
      endDate: json['endDate'] != null ? DateTime.parse(json['endDate']) : null,
      budget: json['budget']?.toDouble(),
      actualCost: json['actualCost']?.toDouble(),
      expectedResponses: json['expectedResponses'],
      actualResponses: json['actualResponses'],
      expectedSales: json['expectedSales']?.toDouble(),
      actualSales: json['actualSales']?.toDouble(),
      notes: json['notes'],
      dateCreated: DateTime.parse(json['dateCreated']),
      dateUpdated: json['dateUpdated'] != null
          ? DateTime.parse(json['dateUpdated'])
          : null,
      createdByUserId: json['createdByUserId'],
      createdByUser: json['createdByUser'] != null
          ? User.fromJson(json['createdByUser'])
          : null,
      leads: (json['leads'] as List?)
          ?.map((item) => Lead.fromJson(item))
          .toList(),
      contacts: (json['contacts'] as List?)
          ?.map((item) => Contact.fromJson(item))
          .toList(),
      tasks: (json['tasks'] as List?)
          ?.map((item) => Tasks.fromJson(item))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'description': description,
      'type': type,
      'status': status,
      'startDate': startDate?.toIso8601String(),
      'endDate': endDate?.toIso8601String(),
      'budget': budget,
      'actualCost': actualCost,
      'expectedResponses': expectedResponses,
      'actualResponses': actualResponses,
      'expectedSales': expectedSales,
      'actualSales': actualSales,
      'notes': notes,
      'dateCreated': dateCreated?.toIso8601String(),
      'dateUpdated': dateUpdated?.toIso8601String(),
      'createdByUserId': createdByUserId,
      'createdByUser': createdByUser?.toJson(),
      'leads': leads?.map((e) => e.toJson()).toList(),
      'contacts': contacts?.map((e) => e.toJson()).toList(),
      'tasks': tasks?.map((e) => e.toJson()).toList(),
    };
  }
}