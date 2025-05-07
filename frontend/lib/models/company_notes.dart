import 'package:frontend/models/company.dart';
import 'package:frontend/models/note.dart';

class CompanyNotes {
  int? id;
  Company? company;
  Note? note;
  int? companyId;
  int? noteId;

  CompanyNotes({
    this.id,
    this.company,
    this.note,
    this.companyId,
    this.noteId
  });

  factory CompanyNotes.fromJson(Map<String, dynamic> json) {
    return CompanyNotes(
      id: json['id'],
      companyId: json['companyId'],
      company: json['company'] != null ? Company.fromJson(json['company']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'task': company,
      'note': note,
      'noteId': noteId,
      'taskId': companyId
    };
  }
}