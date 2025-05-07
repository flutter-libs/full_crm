import 'package:frontend/models/campaign_notes.dart';
import 'package:frontend/models/company_notes.dart';
import 'package:frontend/models/contact_notes.dart';
import 'package:frontend/models/job_notes.dart';
import 'package:frontend/models/lead_notes.dart';
import 'package:frontend/models/task_notes.dart';
import 'package:frontend/models/user_notes.dart';

class Note {
  int? id;
  String title;
  String content;
  DateTime created;
  DateTime updated;

  List<UserNotes>? userNotes;
  List<LeadNotes>? leadNotes;
  List<TaskNotes>? taskNotes;
  List<CompanyNotes>? companyNotes;
  List<ContactNotes>? contactNotes;
  List<CampaignNotes>? campaignNotes;
  List<JobNotes>? jobNotes;

  Note({
    this.id,
    required this.title,
    required this.content,
    required this.created,
    required this.updated,
    this.userNotes,
    this.leadNotes,
    this.taskNotes,
    this.companyNotes,
    this.contactNotes,
    this.campaignNotes,
    this.jobNotes,
  });

  factory Note.fromJson(Map<String, dynamic> json) {
    return Note(
      id: json['id'],
      title: json['title'],
      content: json['content'],
      created: DateTime.parse(json['created']),
      updated: DateTime.parse(json['updated']),
      userNotes: (json['userNotes'] as List<dynamic>?)
          ?.map((e) => UserNotes.fromJson(e))
          .toList(),
      leadNotes: (json['leadNotes'] as List<dynamic>?)
          ?.map((e) => LeadNotes.fromJson(e))
          .toList(),
      taskNotes: (json['taskNotes'] as List<dynamic>?)
          ?.map((e) => TaskNotes.fromJson(e))
          .toList(),
      companyNotes: (json['companyNotes'] as List<dynamic>?)
          ?.map((e) => CompanyNotes.fromJson(e))
          .toList(),
      contactNotes: (json['contactNotes'] as List<dynamic>?)
          ?.map((e) => ContactNotes.fromJson(e))
          .toList(),
      campaignNotes: (json['campaignNotes'] as List<dynamic>?)
          ?.map((e) => CampaignNotes.fromJson(e))
          .toList(),
      jobNotes: (json['jobNotes'] as List<dynamic>?)
          ?.map((e) => JobNotes.fromJson(e))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'title': title,
      'content': content,
      'created': created.toIso8601String(),
      'updated': updated.toIso8601String(),
      'userNotes': userNotes?.map((e) => e.toJson()).toList(),
      'leadNotes': leadNotes?.map((e) => e.toJson()).toList(),
      'taskNotes': taskNotes?.map((e) => e.toJson()).toList(),
      'companyNotes': companyNotes?.map((e) => e.toJson()).toList(),
      'contactNotes': contactNotes?.map((e) => e.toJson()).toList(),
      'campaignNotes': campaignNotes?.map((e) => e.toJson()).toList(),
      'jobNotes': jobNotes?.map((e) => e.toJson()).toList(),
    };
  }
}