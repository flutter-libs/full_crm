import 'package:frontend/models/lead.dart';
import 'package:frontend/models/note.dart';

class LeadNotes {
  int? id;
  Lead? lead;
  Note? note;
  int? leadId;
  int? noteId;

  LeadNotes({
    this.id,
    this.lead,
    this.note,
    this.leadId,
    this.noteId
  });

  factory LeadNotes.fromJson(Map<String, dynamic> json) {
    return LeadNotes(
      id: json['id'],
      leadId: json['leadId'],
      lead: json['lead'] != null ? Lead.fromJson(json['lead']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'lead': lead,
      'note': note,
      'noteId': noteId,
      'leadId': leadId
    };
  }
}