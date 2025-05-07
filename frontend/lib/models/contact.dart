import 'package:frontend/models/User.dart';
import 'package:frontend/models/campaign.dart';
import 'package:frontend/models/company.dart';
import 'package:frontend/models/contact_notes.dart';
import 'package:frontend/models/job.dart';
import 'package:frontend/models/meeting.dart';
import 'package:frontend/models/tasks.dart';

class Contact {
  int? id;
  String? firstName;
  String? lastName;
  String? companyName;
  String? jobTitle;
  String? email;
  String? phoneNumber;
  String? addressLine1;
  String? addressLine2;
  String? city;
  String? state;
  String? zipCode;
  String? country;
  DateTime? dateCreated;
  DateTime? dateUpdated;
  String? imageUrl;
  String? ownerUserId;
  User? ownerUser;
  int? companyId;
  Company? company;
  List<Job>? jobs;
  List<Campaign>? campaigns;
  List<Tasks>? tasks;
  List<Meeting>? meetings;
  List<ContactNotes>? contactNotes;

  Contact({
    this.id,
    this.firstName,
    this.lastName,
    this.companyName,
    this.jobTitle,
    this.email,
    this.phoneNumber,
    this.addressLine1,
    this.addressLine2,
    this.city,
    this.state,
    this.zipCode,
    this.country,
    this.dateCreated,
    this.dateUpdated,
    this.imageUrl,
    this.ownerUserId,
    this.ownerUser,
    this.companyId,
    this.company,
    this.jobs,
    this.campaigns,
    this.tasks,
    this.meetings,
    this.contactNotes,
  });

  factory Contact.fromJson(Map<String, dynamic> json) {
    return Contact(
      id: json['id'],
      firstName: json['firstName'],
      lastName: json['lastName'],
      companyName: json['companyName'],
      jobTitle: json['jobTitle'],
      email: json['email'],
      phoneNumber: json['phoneNumber'],
      addressLine1: json['addressLine1'],
      addressLine2: json['addressLine2'],
      city: json['city'],
      state: json['state'],
      zipCode: json['zipCode'],
      country: json['country'],
      dateCreated: DateTime.parse(json['dateCreated']),
      dateUpdated: json['dateUpdated'] != null
          ? DateTime.parse(json['dateUpdated'])
          : null,
      imageUrl: json['imageUrl'],
      ownerUserId: json['ownerUserId'],
      ownerUser: json['ownerUser'] != null
          ? User.fromJson(json['ownerUser'])
          : null,
      companyId: json['companyId'],
      company: json['company'] != null
          ? Company.fromJson(json['company'])
          : null,
      jobs: (json['jobs'] as List?)
          ?.map((item) => Job.fromJson(item))
          .toList(),
      contactNotes: (json['contactNotes'] as List?)
          ?.map((item) => ContactNotes.fromJson(item)).toList(),
      campaigns: (json['campaigns'] as List?)
          ?.map((item) => Campaign.fromJson(item))
          .toList(),
      tasks: (json['tasks'] as List?)
          ?.map((item) => Tasks.fromJson(item))
          .toList(),
      meetings: (json['meetings'] as List?)
          ?.map((item) => Meeting.fromJson(item))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'firstName': firstName,
      'lastName': lastName,
      'companyName': companyName,
      'jobTitle': jobTitle,
      'email': email,
      'phoneNumber': phoneNumber,
      'addressLine1': addressLine1,
      'addressLine2': addressLine2,
      'city': city,
      'state': state,
      'zipCode': zipCode,
      'country': country,
      'dateCreated': dateCreated?.toIso8601String(),
      'dateUpdated': dateUpdated?.toIso8601String(),
      'imageUrl': imageUrl,
      'ownerUserId': ownerUserId,
      'ownerUser': ownerUser?.toJson(),
      'companyId': companyId,
      'company': company?.toJson(),
      'jobs': jobs?.map((e) => e.toJson()).toList(),
      'campaigns': campaigns?.map((e) => e.toJson()).toList(),
      'tasks': tasks?.map((e) => e.toJson()).toList(),
      'meetings': meetings?.map((e) => e.toJson()).toList(),
      'contactNotes': contactNotes?.map((e) => e.toJson()).toList(),
    };
  }
}