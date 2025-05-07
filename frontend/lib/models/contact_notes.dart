import 'package:frontend/models/contact.dart';
import 'package:frontend/models/note.dart';

class ContactNotes {
  int? id;
  Contact? contact;
  Note? note;
  int? contactId;
  int? noteId;

  ContactNotes({
    this.id,
    this.contact,
    this.note,
    this.contactId,
    this.noteId
  });

  factory ContactNotes.fromJson(Map<String, dynamic> json) {
    return ContactNotes(
      id: json['id'],
      contactId: json['contactId'],
      contact: json['contact'] != null ? Contact.fromJson(json['contact']) : null,
      noteId: json['noteId'],
      note: json['note'] != null ? Note.fromJson(json['note']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'contact': contact,
      'note': note,
      'noteId': noteId,
      'contactId': contactId
    };
  }
}